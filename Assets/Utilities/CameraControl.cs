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

    //位移视差
    public Transform target; //玩家位置
    public Transform farBackground, midBackground; //远景和中景的位置
    private Vector2 lastPos; //最后一次相机的位置

    private void Awake()
    {
        confiner=GetComponent<CinemachineConfiner2D>();
    }
    private void Start()
    {
        GetNewCameraBounds();
        lastPos = transform.position; //记录相机的初始位置
    }
    private void Update()
    {
        //计算相机在上一帧和当前帧之间的移动距离
        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        //根据相机移动的距离，移动远景和中景的位置
        farBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
        midBackground.position += new Vector3(amountToMove.x * 0.5f, amountToMove.y * 0.5f, 0f);

        lastPos = transform.position;
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
