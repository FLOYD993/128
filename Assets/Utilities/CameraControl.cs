using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D confiner;
    public CinemachineImpulseSource impulseSource;
    public VoidEventSO cameraShakeEvent;
    private void Awake()
    {
        confiner=GetComponent<CinemachineConfiner2D>();
    }
    private void Start()
    {
        GetNewCameraBounds();
    }
    private void OnEnable()
    {
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
    }

    private void OnCameraShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }

    private void OnDisable()
    {
        cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
    }
    private void GetNewCameraBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj == null)
            return;
        confiner.m_BoundingShape2D=obj.GetComponent<Collider2D>();
        confiner.InvalidateCache();
    }
}
