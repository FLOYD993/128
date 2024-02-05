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
    protected FSM manager;
    protected Parameter parameter;

    protected TrackEntry trackEntry;

    public ReactState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public virtual void OnEnter()
    {
        Debug.Log("React");

        // parameter.animator.Play("React");
        // trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.react, true, -1F);
        trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.animationDic[StateType.React], false, -1F);
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {
        
    }
}
