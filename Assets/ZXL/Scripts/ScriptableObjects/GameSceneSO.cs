using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "GameScenes/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    public SceneType sceneType;
    public AssetReference sceneReference;
}