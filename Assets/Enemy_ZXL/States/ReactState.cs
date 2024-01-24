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


        /*-------------------- 2. 切换到Chase --------------------*/
        // React动画播放完
        trackEntry.Complete += OnSpineAnimationComplete;
    }

    public void OnUpdate()
    {
        /*-------------------- 1. 切换到Hit --------------------*/
        // 受到伤害
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
