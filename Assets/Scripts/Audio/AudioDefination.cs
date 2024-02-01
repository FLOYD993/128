using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    public PlayAudioEventSO playAudioEvent;
    public AudioClip audioClip;
    //判断是否在一开始就播放
    public bool playOnEnable;
    public void OnEnable()
    {
        if (playOnEnable)
        {
            PlayAudioClip();
        }
    }
    public void PlayAudioClip()
    {
        playAudioEvent.RaiseEvent(audioClip);
    }
}
