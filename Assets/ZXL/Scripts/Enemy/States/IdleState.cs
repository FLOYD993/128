using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class IdleState : IState
{
    protected FSM manager;
    protected Parameter parameter;

    protected TrackEntry trackEntry;

    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public virtual void OnEnter()
    {
        Debug.Log("Idle");
        // parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.idle, true, -1F);
        trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.animationDic[StateType.Idle], true, -1F);
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}
