using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "DataSO/Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> LoadReuestEvent;
     
    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="locationToLoad">���س���</param>
    /// <param name="positionToGo">Player����λ��</param>
    /// <param name="fadeScreen">�Ƿ��뽥��</param>
    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad, Vector3 positionToGo, bool fadeScreen)
    {
        LoadReuestEvent?.Invoke(locationToLoad, positionToGo, fadeScreen);
    }
}