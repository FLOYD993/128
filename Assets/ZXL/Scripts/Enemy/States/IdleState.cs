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

public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;

    private float timer;            // ��ʱ��

    private TrackEntry trackEntry;

    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Debug.Log("Idle");
        // parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.idle, true, -1F);
        trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.animationDic[StateType.Idle], true, -1F);
    }

    public void OnUpdate()
    {
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
            if( parameter.target.position.x >= parameter.chasePoints[0].position.x &&
                parameter.target.position.x <= parameter.chasePoints[1].position.x)
            {
                manager.TransitionState(StateType.React);
            }
        }
    }

    public void OnExit()
    {
        timer = 0f; ;
    }
}
