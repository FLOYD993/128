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
public class ChaseState : IState
{
    protected FSM manager;
    protected Parameter parameter;

    protected TrackEntry trackEntry;

    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public virtual void OnEnter()
    {
        Debug.Log("Chase");

        // parameter.animator.Play("BoarChase");
        // trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.chase, true, -1F);
        trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.animationDic[StateType.Chase], true, -1F);
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}
