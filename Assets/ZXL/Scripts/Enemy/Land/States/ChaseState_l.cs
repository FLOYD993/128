using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// ChaseState: ׷��״̬
/// 1. ->HitState: �ܵ��˺�
/// 2. ->IdleState: Ŀ�꣨Target����ң��뿪��Ұ��Χ || ���ˣ�self������׷�ٷ�Χ
/// 3. ->AttackState: ������Χ��⵽���
/// </summary>
public class ChaseState_l : ChaseState
{
    public ChaseState_l(FSM manager) : base(manager)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        manager.FlipTo(parameter.target);

        /*-------------------- 1. �л���Hit --------------------*/
        // �ܵ��˺�
        if (parameter.isHit)
        {
            manager.TransitionState(StateType.Hit);         // ->HitState
        }

        /*-------------------- 2. �л���Idle --------------------*/
        // Ŀ�꣨Target����ң��뿪��Ұ��Χ || ���ˣ�self������׷�ٷ�Χ
        if (parameter.target == null ||
            manager.transform.position.x < parameter.chasePoints[0].position.x ||
            manager.transform.position.x > parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.Idle);    // ->IdleState
        }

        /*-------------------- 3. �л���Attack --------------------*/
        // ������Χ��⵽��ң��л�������״̬
        if (Physics2D.OverlapCircle(parameter.attackPoint.position, parameter.attackArea, parameter.targetLayer))
        {
            manager.TransitionState(StateType.Attack);      // ->Attacktate
        }

        /*-------------------- **����׷��** --------------------*/
        // Ŀ��������Ұ��Χ������׷���ٶ�(chaseSpeed)����Ŀ��
        if (parameter.target)
        {
            manager.transform.position = Vector2.MoveTowards(manager.transform.position,
                                                                parameter.target.position,
                                                                parameter.runSpeed * Time.deltaTime);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }


}
