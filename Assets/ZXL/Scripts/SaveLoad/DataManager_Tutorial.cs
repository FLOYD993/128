using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json;
using System.IO;


[DefaultExecutionOrder(-100)]
public class DataManager_Tutorial : MonoBehaviour
{
    [Header("事件监听")]
    public VoidEventSO saveDataEvent;
    public VoidEventSO loadDataEvent;


    [Header("Others")]
    private static DataManager_Tutorial instance;

    public static DataManager_Tutorial Instance { get => instance; set => instance = value; }

    private List<ISaveable_Tutorial> savebaleList = new List<ISaveable_Tutorial>();

    private Data_Tutorial saveData ;

    private string jsonFolder;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        saveData = new Data_Tutorial();

        jsonFolder = Application.persistentDataPath + "\\SAVEDATA\\";

        ReadSaveData();
    }

    private void Update()
    {
        if(Keyboard.current.lKey.wasPressedThisFrame)
        {
            Load();
        }
    }

    private void OnEnable()
    {
        saveDataEvent.OnEventRaised += Save;
        loadDataEvent.OnEventRaised += Load;
    }

    private void OnDisable()
    {
        saveDataEvent.OnEventRaised -= Save;
        loadDataEvent.OnEventRaised -= Load; ;
    }

    public void ResigterSaveData(ISaveable_Tutorial saveable )
    {
        // 不希望频繁加载
        if(savebaleList.Contains(saveable)) { return; }

        savebaleList.Add(saveable);
    }

    public void UnRegisterSaveData(ISaveable_Tutorial saveable)
    {
        savebaleList.Remove(saveable);
    }

    public void Save()
    {
        foreach (var saveable in savebaleList)
        {
            saveable.GetSaveData(saveData);
        }

        var resultPath = jsonFolder + "data.sav";

        var jsonData = JsonConvert.SerializeObject(saveData); 
        
        if(!File.Exists(resultPath))
        {
            Directory.CreateDirectory(jsonFolder);
        }

        File.WriteAllText(resultPath, jsonData);
    }

    public void Load()
    {
        foreach (var saveable in savebaleList)
        {
            saveable.LoadData(saveData);
        }
    }

    private void ReadSaveData()
    {
        var resultPath = jsonFolder + "data.sav";

        if (File.Exists(resultPath))
        {
            var stringData = File.ReadAllText(resultPath);

            var jsonData = JsonConvert.DeserializeObject<Data_Tutorial> (stringData);

            saveData = jsonData;
        }
    }
}
