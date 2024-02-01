using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("�¼�����")]
    public PlayAudioEventSO FXEvent;
    public PlayAudioEventSO BGMEvent;
    [Header("���")]
    public AudioSource BGMSource;
    public AudioSource FXSource; //��Ч��Ч

    public AudioMixer audioMixer;

    private void OnEnable()
    {
        FXEvent.OnEventRaised += OnFXEvent;
        BGMEvent.OnEventRaised += OnBGMEvent;
    }

    private void OnDisable()
    {
        FXEvent.OnEventRaised -= OnFXEvent;
        BGMEvent.OnEventRaised -= OnBGMEvent;
    }

    private void OnFXEvent(AudioClip clip)
    {
        FXSource.clip = clip;
        FXSource.Play(); //���ʲ���
    }
    private void OnBGMEvent(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }
    //��������
    public void SetMasterVolume(float volume)    // �����������ĺ���
    {
        audioMixer.SetFloat("Master", volume);
        // MasterVolumeΪ���Ǳ�¶������Master�Ĳ���
    }

    public void SetBGMVolume(float volume)    // ���Ʊ������������ĺ���
    {
        audioMixer.SetFloat("BGM", volume);
    }

    public void SetFXVolume(float volume)    // ������Ч�����ĺ���
    {
        audioMixer.SetFloat("FX", volume);
    }
}
