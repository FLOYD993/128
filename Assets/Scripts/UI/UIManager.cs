using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIManager : MonoBehaviour
{
    public PlayerStateBar PlayerStateBar;
    [Header("ÊÂ¼þ¼àÌý")]
    public CharacterEventSO healthEvent;
    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
       
    }

    private void OnHealthEvent(Character character)
    {
        var per=character.currentHealth / character.maxHealth;
        PlayerStateBar.OnHealthChange(per);
        PlayerStateBar.OnPowerChange(character);
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

}
