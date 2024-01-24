using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

/// <summary>
/// AttackState: 攻击状态
/// 1. ->HitState: 受到伤害
/// 2. ->ChaseState: Attack动画播放完(>0.95F)
/// </summary>
public class AttackState : IState
{
    private FSM manager;
    private Parameter parameter;

    private TrackEntry trackEntry;

    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Debug.Log("Attack");

        // parameter.animator.Play("BoarAttack");
        manager.FlipTo(parameter.target);
        // trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.attack, true, -1F);
        trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.animationDic[StateType.Attack], true, -1F);


        /*-------------------- 2. 切换到Chase --------------------*/
        // Attack动画播放完
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
