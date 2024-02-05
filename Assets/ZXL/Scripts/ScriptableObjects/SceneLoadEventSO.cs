using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "DataSO/Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> LoadReuestEvent;
     
    /// <summary>
    /// 场景加载请求
    /// </summary>
    /// <param name="locationToLoad">加载场景</param>
    /// <param name="positionToGo">Player加载位置</param>
    /// <param name="fadeScreen">是否渐入渐出</param>
    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad, Vector3 positionToGo, bool fadeScreen)
    {
        LoadReuestEvent?.Invoke(locationToLoad, positionToGo, fadeScreen);
    }
}