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
        /*-------------------- 1. 切换到Death --------------------*/
        // 生命值 <= 0
        if (parameter.currHealth <= 0)
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
