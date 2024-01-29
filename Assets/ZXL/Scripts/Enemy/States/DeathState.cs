using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DeathState: ����״̬
/// 
/// </summary>
public class DeathState : IState
{
    private FSM manager;
    private Parameter parameter;

    private TrackEntry trackEntry;

    public DeathState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Debug.Log("Death");

        // parameter.animator.Play("BoarDeath");
        // trackEntry = parameter.skeletonAnimation.AnimationState.SetAnimation(0, parameter.death, false);
        trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.animationDic[StateType.Death], true, -1F);

        // TODO: ��һ��ʱ�����������
        GameObject.Destroy(manager.gameObject.transform.parent.gameObject, 1F);
    }

    public void OnUpdate()
    {


    }
    public void OnExit()
    {

    }


}
