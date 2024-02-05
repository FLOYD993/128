using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ReactState: ��Ӧ״̬
/// 1. ->HitState: �ܵ��˺�
/// 2. ->ChaseState: React����������(>0.95F)
/// </summary>
public class ReactState_l : ReactState
{
    public ReactState_l(FSM manager) : base(manager)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        manager.FlipTo(parameter.target);


        /*-------------------- 2. �л���Chase --------------------*/
        // React����������
        trackEntry.Complete += OnSpineAnimationComplete;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        /*-------------------- 1. �л���Hit --------------------*/
        // �ܵ��˺�
        if (parameter.isHit)
        {
            manager.TransitionState(StateType.Hit);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    private void OnSpineAnimationComplete(TrackEntry trackEntry)
    {
        manager.TransitionState(StateType.Chase);
    }
}
