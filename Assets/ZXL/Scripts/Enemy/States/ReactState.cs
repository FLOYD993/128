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
public class ReactState : IState
{
    private FSM manager;
    private Parameter parameter;

    private TrackEntry trackEntry;

    public ReactState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Debug.Log("React");

        manager.FlipTo(parameter.target);

        // parameter.animator.Play("React");
        // trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.react, true, -1F);
        trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.animationDic[StateType.React], false, -1F);


        /*-------------------- 2. �л���Chase --------------------*/
        // React����������
        trackEntry.Complete += OnSpineAnimationComplete;
    }

    public void OnUpdate()
    {
        /*-------------------- 1. �л���Hit --------------------*/
        // �ܵ��˺�
        if (parameter.isHit)
        {
            manager.TransitionState(StateType.Hit);
        }
    }

    public void OnExit()
    {
        
    }

    private void OnSpineAnimationComplete(TrackEntry trackEntry)
    {
        manager.TransitionState(StateType.Chase);
    }
}
