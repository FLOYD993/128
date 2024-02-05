using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DeathState: 死亡状态
/// 
/// </summary>
public class DeathState : IState
{
    protected FSM manager;
    protected Parameter parameter;

    protected TrackEntry trackEntry;

    public DeathState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public virtual void OnEnter()
    {
        Debug.Log("Death");

        // parameter.animator.Play("BoarDeath");
        // trackEntry = parameter.skeletonAnimation.AnimationState.SetAnimation(0, parameter.death, false);
        trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.animationDic[StateType.Death], true, -1F);

        // TODO: 在一定时间后销毁物体
        GameObject.Destroy(manager.gameObject.transform.parent.gameObject, 1F);


    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}
