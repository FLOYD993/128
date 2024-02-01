using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("事件监听")]
    public PlayAudioEventSO FXEvent;
    public PlayAudioEventSO BGMEvent;
    [Header("组件")]
    public AudioSource BGMSource;
    public AudioSource FXSource; //特效音效

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
        FXSource.Play(); //单词播放
    }
    private void OnBGMEvent(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }
    //音量调节
    public void SetMasterVolume(float volume)    // 控制主音量的函数
    {
        audioMixer.SetFloat("Master", volume);
        // MasterVolume为我们暴露出来的Master的参数
    }

    public void SetBGMVolume(float volume)    // 控制背景音乐音量的函数
    {
        audioMixer.SetFloat("BGM", volume);
    }

    public void SetFXVolume(float volume)    // 控制音效音量的函数
    {
        audioMixer.SetFloat("FX", volume);
    }
}
