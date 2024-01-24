using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// ChaseState: 追踪状态
/// 1. ->HitState: 受到伤害
/// 2. ->IdleState: 目标（Target：玩家）离开视野范围 || 敌人（self）超出追踪范围
/// 3. ->AttackState: 攻击范围检测到玩家
/// </summary>
public class ChaseState : IState
{
    private FSM manager;
    private Parameter parameter;

    private TrackEntry trackEntry;

    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Debug.Log("Chase");

        // parameter.animator.Play("BoarChase");
        // trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.chase, true, -1F);
        trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.animationDic[StateType.Chase], true, -1F);

    }

    public void OnUpdate()
    {
        manager.FlipTo(parameter.target);

        /*-------------------- 1. 切换到Hit --------------------*/
        // 受到伤害
        if (parameter.isHit)
        {
            manager.TransitionState(StateType.Hit);         // ->HitState
        }

        /*-------------------- 2. 切换到Idle --------------------*/
        // 目标（Target：玩家）离开视野范围 || 敌人（self）超出追踪范围
        if (parameter.target == null ||
            manager.transform.position.x < parameter.chasePoints[0].position.x ||
            manager.transform.position.x > parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.Idle);    // ->IdleState
        }

        /*-------------------- 3. 切换到Attack --------------------*/
        // 攻击范围检测到玩家，切换到攻击状态
        if (Physics2D.OverlapCircle(parameter.attackPoint.position, parameter.attackArea, parameter.targetLayer))
        {
            manager.TransitionState(StateType.Attack);      // ->Attacktate
        }

        /*-------------------- **继续追击** --------------------*/
        // 目标仍在视野范围，则以追击速度(chaseSpeed)移向目标
        if (parameter.target)
        {
            manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                                                                parameter.target.position,
                                                                parameter.chaseSpeed * Time.deltaTime);
        }
    }

    public void OnExit()
    {

    }


}
