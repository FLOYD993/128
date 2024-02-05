using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DeathState: 死亡状态
/// 
/// </summary>
public class DeathState_l : DeathState
{
    public DeathState_l(FSM manager) : base(manager)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        // TODO: 在一定时间后销毁物体
        GameObject.Destroy(manager.gameObject.transform.parent.gameObject, 1F);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}
