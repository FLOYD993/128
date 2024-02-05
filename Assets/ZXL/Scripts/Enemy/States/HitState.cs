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
public class HitState : IState
{
    protected FSM manager;
    protected Parameter parameter;

    protected TrackEntry trackEntry;

    public HitState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public virtual void OnEnter()
    {
        Debug.Log("Hit");

        // parameter.animator.Play("BoarHit");
        // trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.hit, true, -1F);
        trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.animationDic[StateType.Hit], false, -1F);

        parameter.currHealth--;
        manager.barController.GetHit();
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {
        parameter.isHit = false;
    }
}
