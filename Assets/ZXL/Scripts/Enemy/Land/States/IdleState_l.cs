using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// IdleState: ����״̬��
/// 1. ->HitState: �ܵ��˺�
/// 2. ->PartolState: IdleTime����
/// 3. ->ReactState: ������ң�������Ҵ���׷����Χ
/// </summary>

public class IdleState_l : IdleState
{
    private float timer;            // ��ʱ��

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

        /*-------------------- 1. �л���Hit --------------------*/
        // �ܵ��˺�
        if (parameter.isHit)
        {
            manager.TransitionState(StateType.Hit);
        }

        /*-------------------- 2. �л���Patrol --------------------*/
        // idleTime����
        if (timer >= parameter.idleTime)
        {
            manager.TransitionState(StateType.Patrol);
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
        timer = 0f;
    }
}
