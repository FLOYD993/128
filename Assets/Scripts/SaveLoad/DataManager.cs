using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    [Header("ÊÂ¼þ¼àÌý")]
    public VoidEventSO savaDataEvent;

    private List<ISaveable> saveableList = new List<ISaveable>();
    private Data saveData;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        saveData = new Data();
    }
    private void OnEnable()
    {
        saveDataEvent.OnEventRaised += Save;
    }
    private void OnDisable()
    {
        saveDataEvent.OnEventRaised -= Save;
    }
    public void RegisterSaveData(ISaveable saveable)
    {
        if (!saveableList.Contains(saveable))
        {
            saveableList.Add(saveable);
        }
    }
    public void UnRegisterSaveData(ISaveable saveable)
    {
        saveableList.Remove(saveable);
    }
    private void Update()
    {
        if(Keyboard.current.eKey.wasPressedThisFrame)
        {
            Load();
        }
    }
    public void Save()
    {
        foreach(var saveable in saveableList)
        {
            saveable.GetSaveDate(saveData);
        }
        foreach(var item in saveData.characterPosDict)
        {
            Debug.Log(item.Key + " " + item.Value);
        }
    }
    public void Load()
    {
        foreach (var saveable in saveableList)
        {
            saveable.GetSaveDate(saveData);
        }
    }
}
