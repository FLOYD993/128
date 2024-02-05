using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// PatrolState: Ѳ��״̬
/// 1. ->HitState: �ܵ��˺�
/// 2. ->IdleState: Ѳ�ߵ���Ѳ�ߵ��㹻����0.1F��
/// 3. ->ReactState: ������ң�������Ҵ���׷����Χ
/// </summary>
public class PatrolState_l : PatrolState
{
    /// <summary>
    /// ���Ҳ��л�Ѳ�ߵ�
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

        /*-------------------- 1. �л���Hit --------------------*/
        // �ܵ��˺�
        if (parameter.isHit)
        {
            manager.TransitionState(StateType.Hit);
        }

        /*-------------------- 2. �л���Idle --------------------*/
        // Ѳ�ߵ���Ѳ�ߵ��㹻����0.1F��
        manager.transform.position = Vector2.MoveTowards(manager.transform.position, parameter.patrolPoints[patrolPosition].position, parameter.walkSpeed * Time.deltaTime);

        if (Vector2.Distance(manager.transform.position, parameter.patrolPoints[patrolPosition].position) < 0.5F)
        {
            manager.TransitionState(StateType.Idle);
        }

        /*-------------------- 3. �л���React --------------------*/
        // ������ң�������Ҵ���׷����Χ
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
