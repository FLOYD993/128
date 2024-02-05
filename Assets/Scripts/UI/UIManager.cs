using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UIManager : MonoBehaviour
{
    public PlayerStateBar PlayerStateBar;
    [Header("事件监听")]
    public CharacterEventSO healthEvent;
    public SceneLoadEventSO unLoadSceneEvent;
    public VoidEventSO loadDataEvent;
    public VoidEventSO gameOverEvent;
    public VoidEventSO backToMenuEvent;

    [Header("组件")]
    public GameObject gameOverPanel;
    public GameObject restartBtn;

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        unLoadSceneEvent.LoadReuestEvent += OnUnLoadedSeceneEvent;
        loadDataEvent.OnEventRaised += OnLoadDataEvent;
        gameOverEvent.OnEventRaised += OnGameOverEvent;
        backToMenuEvent.OnEventRaised += OnLoadDataEvent;
    }


    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        unLoadSceneEvent.LoadReuestEvent -= OnUnLoadedSeceneEvent;
        loadDataEvent.OnEventRaised -= OnLoadDataEvent;
        gameOverEvent.OnEventRaised -= OnGameOverEvent;
        backToMenuEvent.OnEventRaised -= OnLoadDataEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var per=character.currentHealth / character.maxHealth;
        PlayerStateBar.OnHealthChange(per);
        PlayerStateBar.OnPowerChange(character);
    }

    private void OnUnLoadedSeceneEvent(GameSceneSO sceneToLoad, Vector3 arg1, bool arg2)
    {

        var isMenu = sceneToLoad.sceneType == SceneType.Menu;
        PlayerStateBar.gameObject.SetActive(!isMenu);
    }

    private void OnLoadDataEvent()
    {
        gameOverPanel.SetActive(false);
    }

    private void OnGameOverEvent()
    {
        gameOverPanel.SetActive(true );
        EventSystem.current.SetSelectedGameObject(restartBtn);
    }
}
