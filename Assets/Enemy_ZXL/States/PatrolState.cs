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
public class PatrolState : IState
{
    private FSM manager;
    private Parameter parameter;

    /// <summary>
    /// ���Ҳ��л�Ѳ�ߵ�
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

        // ���Ŷ���
        // parameter.skeletonAnimation.Play("BoarWalk");
        // trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.patrol, true, -1F);
        trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.animationDic[StateType.Patrol], true, -1F);

       

    }

    public void OnUpdate()
    {


        /*-------------------- 1. �л���Hit --------------------*/
        // �ܵ��˺�
        if (parameter.isHit)
        {
            manager.TransitionState(StateType.Hit);
        }

        /*-------------------- 2. �л���Idle --------------------*/
        // Ѳ�ߵ���Ѳ�ߵ��㹻����0.1F��
        manager.transform.position = Vector2.MoveTowards(manager.transform.position, parameter.patrolPoints[patrolPosition].position, parameter.moveSpeed * Time.deltaTime);

        if(Vector2.Distance(manager.transform.position, parameter.patrolPoints[patrolPosition].position) < 0.1F)
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

    public void OnExit()
    {
        patrolPosition++;

        if(patrolPosition >= parameter.patrolPoints.Length)
        {
            patrolPosition = 0;
        }
    }


}
