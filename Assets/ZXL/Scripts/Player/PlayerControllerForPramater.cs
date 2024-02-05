using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerForPramater : MonoBehaviour, ISaveable_Tutorial
{
    [Header("¼àÌýÊÂ¼þ")]
    public SceneLoadEventSO sceneLoadEvent;
    public VoidEventSO afterSceneLoadedEventSO;
    public VoidEventSO loadDataEvent;
    public VoidEventSO backToMenuEvent;


    private Character playerCharacter;
    private PlayerController playerController; 

    private void Awake()
    {
        playerCharacter = GetComponent<Character>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        sceneLoadEvent.LoadReuestEvent += OnLoadEvent;
        afterSceneLoadedEventSO.OnEventRaised += OnAfterSceneLoadedEvent;
        loadDataEvent.OnEventRaised += OnLoadDataEvent;
        backToMenuEvent.OnEventRaised += OnLoadDataEvent;


        ISaveable_Tutorial saveable = this;
        saveable.RegisterSaveData();

    }

    private void OnDisable()
    {
        sceneLoadEvent.LoadReuestEvent -= OnLoadEvent;
        afterSceneLoadedEventSO.OnEventRaised -= OnAfterSceneLoadedEvent;
        loadDataEvent.OnEventRaised -= OnLoadDataEvent;
        backToMenuEvent.OnEventRaised -= OnLoadDataEvent;


        ISaveable_Tutorial savebale = this;
        savebale.UnregisterSaveData();

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Water"))
        {
            playerCharacter.currentHealth = 0;
            playerCharacter.OnHealthChange?.Invoke(playerCharacter);
            playerCharacter.OnDie?.Invoke();
        }
    }

    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        GetComponent<PlayerController>().InputControl.Disable();
    }

    private void OnAfterSceneLoadedEvent()
    {
        GetComponent<PlayerController>().InputControl.Enable();
    }

    public DataDefination_Tutorial GetDataID()
    {
        return GetComponent<DataDefination_Tutorial>();
    }

    public void GetSaveData(Data_Tutorial data)
    {
        Debug.Log(GetDataID().ID);

        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            data.characterPosDict[GetDataID().ID] = new SerializeVector3(transform.position);
            data.floatSavedData[GetDataID().ID + "Health"] = playerCharacter.currentHealth;
            data.floatSavedData[GetDataID().ID + "Power"] = playerCharacter.currentPower;
        }
        else
        {
            data.characterPosDict.Add(GetDataID().ID, new SerializeVector3(transform.position));
            data.floatSavedData.Add(GetDataID().ID + "Health", playerCharacter.currentHealth);
            data.floatSavedData.Add(GetDataID().ID + "Power", playerCharacter.currentPower);

            ;
        }
    }

    public void LoadData(Data_Tutorial data)
    {
        if (!data.characterPosDict.ContainsKey(GetDataID().ID)) { return; }

        transform.position = data.characterPosDict[GetDataID().ID].ToVector3();
        playerCharacter.currentHealth = data.floatSavedData[GetDataID().ID + "Health"];
        playerCharacter.currentPower = data.floatSavedData[GetDataID().ID + "Power"];

        playerCharacter.OnHealthChange?.Invoke(playerCharacter);
    }

    private void OnLoadDataEvent()
    {
        playerController.isDead = false;
    }
}
