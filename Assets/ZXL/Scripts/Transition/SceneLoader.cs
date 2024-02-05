using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, ISaveable_Tutorial
{
    [Header("事件监听")]
    public SceneLoadEventSO loadEventSO;
    public VoidEventSO newGameEventSO;
    public VoidEventSO backToMenuEvent;

    [Header("广播")]
    public VoidEventSO afterSceneLoadedEvent;
    public FadeEventSO fadeEventSO;
    public SceneLoadEventSO unloadedSceneEvent;

    [Header("参数")]
    public Transform playerTrans;
    public Vector3 menuPosition;
    public Vector3 firstPosition;

    [Header("场景")]
    public GameSceneSO menuScene;
    public GameSceneSO firstLoadScene;
    private GameSceneSO currentLoadedScene;
    private GameSceneSO sceneToLoad;


    private Vector3 positionToGo;
    private bool isFade;
    private bool isLoading;
    public float fadeDuration;

    private void Awake()
    {
        //currentLoadedScene = firstLoadScene;
        //currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void Start()
    {
        // MainMenu制作后会修改
        // NewGame();

        loadEventSO.RaiseLoadRequestEvent(menuScene, menuPosition, true);
    }

    private void OnEnable()
    {
        loadEventSO.LoadReuestEvent += OnLoadRequestEvent;
        newGameEventSO.OnEventRaised += NewGame;
        backToMenuEvent.OnEventRaised += OnBackToMenuEvent;


        ISaveable_Tutorial saveable = this;
        saveable.RegisterSaveData();

    }

    private void OnDisable()
    {
        loadEventSO.LoadReuestEvent -= OnLoadRequestEvent;
        newGameEventSO.OnEventRaised -= NewGame;
        backToMenuEvent.OnEventRaised -= OnBackToMenuEvent;

        ISaveable_Tutorial saveable = this;
        saveable.UnregisterSaveData();
    }



    private void NewGame()
    {
        sceneToLoad = firstLoadScene;

        loadEventSO.RaiseLoadRequestEvent(sceneToLoad, firstPosition, true);
    }

    private void OnLoadRequestEvent(GameSceneSO loactionToLoad, Vector3 PositionToGo, bool isFadeScreen)
    {
        if(isLoading) { return; }

        isLoading = true;
        sceneToLoad = loactionToLoad;
        positionToGo = PositionToGo;
        isFade = isFadeScreen;

        if(currentLoadedScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }
        else
        {
            LoadNewScene();
        }
    }

    private IEnumerator UnLoadPreviousScene()
    {
        if (isFade)
        {
            fadeEventSO.FadeIn(fadeDuration);
        }

        yield return new WaitForSeconds(fadeDuration);

        unloadedSceneEvent.RaiseLoadRequestEvent(sceneToLoad,positionToGo, true);

        yield return currentLoadedScene.sceneReference.UnLoadScene();

        // 关闭人物
        playerTrans.gameObject.SetActive(false);
        
        //加载新场景
        LoadNewScene();
    }

    private void LoadNewScene()
    {
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);

        loadingOption.Completed += OnLoadCompleted;
    }

    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        currentLoadedScene = sceneToLoad;

        playerTrans.position = positionToGo;
        playerTrans.gameObject.SetActive(true);

        if(isFade)
        {
            fadeEventSO.FadeOut(fadeDuration);
        }

        isLoading = false;

        if(currentLoadedScene.sceneType == SceneType.Location)
        {
            // 场景加载完后事件
            afterSceneLoadedEvent.RaiseEvent();
        }


    }

    public DataDefination_Tutorial GetDataID()
    {
        return GetComponent<DataDefination_Tutorial>();
    }

    public void GetSaveData(Data_Tutorial data)
    {
        data.SaveGameScene(currentLoadedScene);
    }

    public void LoadData(Data_Tutorial data)
    {
        var playerID = playerTrans.GetComponent<DataDefination_Tutorial>().ID;

        Debug.Log(playerID);

        if(data.characterPosDict.ContainsKey(playerID))
        {
            positionToGo = data.characterPosDict[playerID].ToVector3();

            sceneToLoad = data.GetSavedScene();

            Debug.Log(sceneToLoad);

            OnLoadRequestEvent(sceneToLoad, positionToGo, true);
        }
    }

    private void OnBackToMenuEvent()
    {
        sceneToLoad = menuScene;
        loadEventSO.RaiseLoadRequestEvent(sceneToLoad, menuPosition, true);
    }
}
