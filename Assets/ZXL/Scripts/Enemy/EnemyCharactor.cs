using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCharactor : MonoBehaviour, ISaveable_Tutorial
{
    [Header("ÊÂ¼þ¼àÌý")]
    public VoidEventSO newGameEvent;

    private void OnEnable()
    {
        newGameEvent.OnEventRaised += NewGame;

        ISaveable_Tutorial saveable = this;
        saveable.RegisterSaveData();
    }
    private void OnDisable()
    {
        newGameEvent.OnEventRaised -= NewGame;

        ISaveable_Tutorial savebale = this;
        savebale.UnregisterSaveData();
    }

    private void NewGame()
    {
        GetComponent<FSM>().OnStart();
    }

    public DataDefination_Tutorial GetDataID()
    {
        return GetComponent<DataDefination_Tutorial>();
    }

    public void GetSaveData(Data_Tutorial data)
    {
        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            data.characterPosDict[GetDataID().ID] = new SerializeVector3(transform.position);
        }
        else
        {
            data.characterPosDict.Add(GetDataID().ID, new SerializeVector3(transform.position));
        }
    }

    public void LoadData(Data_Tutorial data)
    {
        if (!data.characterPosDict.ContainsKey(GetDataID().ID)) { return; }

        transform.position = data.characterPosDict[GetDataID().ID].ToVector3();
    }
}
