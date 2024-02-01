using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    public PlayAudioEventSO playAudioEvent;
    public AudioClip audioClip;
    //�ж��Ƿ���һ��ʼ�Ͳ���
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
