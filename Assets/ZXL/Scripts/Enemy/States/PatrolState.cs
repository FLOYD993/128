using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 
/// </summary>
public class PatrolState : IState
{
    protected FSM manager;
    protected Parameter parameter;

    protected TrackEntry trackEntry;

    public PatrolState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public virtual void OnEnter()
    {
        Debug.Log("Patrol");

        // ²¥·Å¶¯»­
        // parameter.skeletonAnimation.Play("BoarWalk");
        // trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.patrol, true, -1F);
        trackEntry = parameter.skeletonAnimation.AnimationState.AddAnimation(0, parameter.animationDic[StateType.Patrol], true, -1F);
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}
