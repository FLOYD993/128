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
public class PatrolState : IState
{
    private FSM manager;
    private Parameter parameter;

    /// <summary>
    /// 查找并切换巡逻点
    /// </summary>
    private int patrolPosition;

    private TrackEntry trackEntry;

    public PatrolState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Debug.Log("Patrol");

        manager.FlipTo(parameter.patrolPoints[patrolPosition]);

        // 播放动画
        // parameter.skeletonAnimation.Play("BoarWalk");
        // trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.patrol, true, -1F);
        trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.animationDic[StateType.Patrol], true, -1F);

       

    }

    public void OnUpdate()
    {


        /*-------------------- 1. 切换到Hit --------------------*/
        // 受到伤害
        if (parameter.isHit)
        {
            manager.TransitionState(StateType.Hit);
        }

        /*-------------------- 2. 切换到Idle --------------------*/
        // 巡逻到离巡逻点足够近（0.1F）
        manager.transform.position = Vector2.MoveTowards(manager.transform.position, parameter.patrolPoints[patrolPosition].position, parameter.moveSpeed * Time.deltaTime);

        if(Vector2.Distance(manager.transform.position, parameter.patrolPoints[patrolPosition].position) < 0.1F)
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

    public void OnExit()
    {
        patrolPosition++;

        if(patrolPosition >= parameter.patrolPoints.Length)
        {
            patrolPosition = 0;
        }
    }


}
