using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// ChaseState: ×·×Ù×´Ì¬
/// 1. ->HitState: ÊÜµ½ÉËº¦
/// 2. ->IdleState: Ä¿±ê£¨Target£ºÍæ¼Ò£©Àë¿ªÊÓÒ°·¶Î§ || µÐÈË£¨self£©³¬³ö×·×Ù·¶Î§
/// 3. ->AttackState: ¹¥»÷·¶Î§¼ì²âµ½Íæ¼Ò
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
