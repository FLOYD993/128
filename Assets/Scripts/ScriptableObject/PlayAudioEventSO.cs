using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Event/PlayAudioEventSO")]
//传递不同物体间的音频文件
public class PlayAudioEventSO : ScriptableObject
{
    public UnityAction<AudioClip> OnEventRaised;
    public void RaiseEvent(AudioClip audioClip)
    {
        OnEventRaised?.Invoke(audioClip);
    }
}
