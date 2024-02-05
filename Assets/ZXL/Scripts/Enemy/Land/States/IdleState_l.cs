using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// IdleState: 闲置状态。
/// 1. ->HitState: 受到伤害
/// 2. ->PartolState: IdleTime结束
/// 3. ->ReactState: 发现玩家，并且玩家处于追击范围
/// </summary>

public class IdleState_l : IdleState
{
    private float timer;            // 计时器

    public IdleState_l(FSM manager) : base(manager)
    {

    }

    public override void OnEnter()
    {

        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        timer += Time.deltaTime;

        /*-------------------- 1. 切换到Hit --------------------*/
        // 受到伤害
        if (parameter.isHit)
        {
            manager.TransitionState(StateType.Hit);
        }

        /*-------------------- 2. 切换到Patrol --------------------*/
        // idleTime结束
        if (timer >= parameter.idleTime)
        {
            manager.TransitionState(StateType.Patrol);
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
        timer = 0f;
    }
}
