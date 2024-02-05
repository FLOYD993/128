using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HitState: �ܵ��˺�״̬
/// 1. ->DeathState: ����ֵ <= 0
/// 2. ->ChaseState: �ܵ��˺���ֱ�ӿ�ʼ׷�����
/// </summary>
public class HitState_l : HitState
{
    public HitState_l(FSM manager) : base(manager)
    {
    }

    public override void OnEnter()
    {
        base.OnExit();

        trackEntry.Complete += OnSpineAnimationComplete;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public void OnSpineAnimationComplete(TrackEntry trackEntry)
    {
        /*-------------------- 1. �л���Death --------------------*/
        // ����ֵ <= 0
        if (parameter.currHealth <= 0)
        {
            manager.TransitionState(StateType.Death);
        }

        /*-------------------- 2. �л���Chase --------------------*/
        // �ܵ��˺���ֱ�ӿ�ʼ׷�����
        else
        {
            parameter.target = GameObject.FindWithTag("Player").transform;

            manager.TransitionState(StateType.Chase);
        }
    }
}
