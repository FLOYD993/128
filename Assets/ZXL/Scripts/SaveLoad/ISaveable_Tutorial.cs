public interface ISaveable_Tutorial
{
    DataDefination_Tutorial GetDataID();

    void RegisterSaveData() => DataManager_Tutorial.Instance.ResigterSaveData(this);

    void UnregisterSaveData() => DataManager_Tutorial.Instance.UnRegisterSaveData(this);

    void GetSaveData(Data_Tutorial data);
    void LoadData(Data_Tutorial data);
}
