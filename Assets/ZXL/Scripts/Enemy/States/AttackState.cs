using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

/// <summary>
/// AttackState: ¹¥»÷×´Ì¬
/// 1. ->HitState: ÊÜµ½ÉËº¦
/// 2. ->ChaseState: Attack¶¯»­²¥·ÅÍê(>0.95F)
/// </summary>
public class AttackState : IState
{
    protected FSM manager;
    protected Parameter parameter;

    protected TrackEntry trackEntry;

    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public virtual void OnEnter()
    {
        Debug.Log("Attack");

        // trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.attack, true, -1F);
        trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.animationDic[StateType.Attack], true, -1F);
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}
