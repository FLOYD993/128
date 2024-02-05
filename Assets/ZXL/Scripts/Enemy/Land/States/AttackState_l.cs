using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

/// <summary>
/// AttackState: ����״̬
/// 1. ->HitState: �ܵ��˺�
/// 2. ->ChaseState: Attack����������(>0.95F)
/// </summary>
public class AttackState_l : AttackState
{
    public AttackState_l(FSM manager) : base(manager)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();


        // parameter.animator.Play("BoarAttack");
        manager.FlipTo(parameter.target);

        /*-------------------- 2. �л���Chase --------------------*/
        // Attack����������
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
