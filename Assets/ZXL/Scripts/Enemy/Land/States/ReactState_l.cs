using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ReactState: 反应状态
/// 1. ->HitState: 受到伤害
/// 2. ->ChaseState: React动画播放完(>0.95F)
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


        /*-------------------- 2. 切换到Chase --------------------*/
        // React动画播放完
        trackEntry.Complete += OnSpineAnimationComplete;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        /*-------------------- 1. 切换到Hit --------------------*/
        // 受到伤害
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
