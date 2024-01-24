using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HitState: 受到伤害状态
/// 1. ->DeathState: 生命值 <= 0
/// 2. ->ChaseState: 受到伤害，直接开始追击玩家
/// </summary>
public class HitState : IState
{
    private FSM manager;
    private Parameter parameter;

    private TrackEntry trackEntry;

    public HitState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        Debug.Log("Hit");

        // parameter.animator.Play("BoarHit");
        // trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.hit, true, -1F);
        trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.animationDic[StateType.Hit], false, -1F);

        parameter.currentHealth--;
        manager.barController.GetHit();


        trackEntry.Complete += AfterHit;

    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {
        parameter.isHit = false;
    }

    public void AfterHit(TrackEntry trackEntry)
    {
        /*-------------------- 1. 切换到Death --------------------*/
        // 生命值 <= 0
        if (parameter.currentHealth <= 0)
        {
            manager.TransitionState(StateType.Death);
        }

        /*-------------------- 2. 切换到Chase --------------------*/
        // 受到伤害，直接开始追击玩家
        else
        {
            parameter.target = GameObject.FindWithTag("Player").transform;

            manager.TransitionState(StateType.Chase);
        }
    }
}
