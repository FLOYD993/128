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

    //λ���Ӳ�
    public Transform target; //���λ��
    public Transform farBackground, midBackground; //Զ�����о���λ��
    private Vector2 lastPos; //���һ�������λ��

    private void Awake()
    {
        confiner=GetComponent<CinemachineConfiner2D>();
    }
    private void Start()
    {
        GetNewCameraBounds();
        lastPos = transform.position; //��¼����ĳ�ʼλ��
    }
    private void Update()
    {
        //�����������һ֡�͵�ǰ֮֡����ƶ�����
        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        //��������ƶ��ľ��룬�ƶ�Զ�����о���λ��
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
