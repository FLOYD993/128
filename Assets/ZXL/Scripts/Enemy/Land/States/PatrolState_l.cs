using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// PatrolState: 巡逻状态
/// 1. ->HitState: 受到伤害
/// 2. ->IdleState: 巡逻到离巡逻点足够近（0.1F）
/// 3. ->ReactState: 发现玩家，并且玩家处于追击范围
/// </summary>
public class PatrolState_l : PatrolState
{
    /// <summary>
    /// 查找并切换巡逻点
    /// </summary>
    private int patrolPosition;

    public PatrolState_l(FSM manager) : base(manager)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        manager.FlipTo(parameter.patrolPoints[patrolPosition]);
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

        /*-------------------- 2. 切换到Idle --------------------*/
        // 巡逻到离巡逻点足够近（0.1F）
        manager.transform.position = Vector2.MoveTowards(manager.transform.position, parameter.patrolPoints[patrolPosition].position, parameter.walkSpeed * Time.deltaTime);

        if (Vector2.Distance(manager.transform.position, parameter.patrolPoints[patrolPosition].position) < 0.5F)
        {
            manager.TransitionState(StateType.Idle);
        }

        /*-------------------- 3. 切换到React --------------------*/
        // 发现玩家，并且玩家处于追击范围
        if (parameter.target != null)
        {
            if (parameter.target.position.x >= parameter.chasePoints[0].position.x &&
                parameter.target.position.x <= parameter.chasePoints[1].position.x)
            {
                manager.TransitionState(StateType.React);
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        patrolPosition++;

        if (patrolPosition >= parameter.patrolPoints.Length)
        {
            patrolPosition = 0;
        }
    }
}
