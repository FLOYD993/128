using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class FSM_l : FSM
{
    protected override void RegisterState()
    {
        foreach (AnimationType state in parameter.animationTypes)
        {
            switch (state.type)
            {
                case StateType.Idle:
                    states.Add(StateType.Idle, new IdleState_l(this));
                    parameter.animationDic.Add(StateType.Idle, state.animationName);
                    break;
                case StateType.Patrol:
                    states.Add(StateType.Patrol, new PatrolState_l(this));
                    parameter.animationDic.Add(StateType.Patrol, state.animationName);
                    break;
                case StateType.Chase:
                    states.Add(StateType.Chase, new ChaseState_l(this));
                    parameter.animationDic.Add(StateType.Chase, state.animationName);
                    break;
                case StateType.React:
                    states.Add(StateType.React, new ReactState_l(this));
                    parameter.animationDic.Add(StateType.React, state.animationName);
                    break;
                case StateType.Attack:
                    states.Add(StateType.Attack, new AttackState_l(this));
                    parameter.animationDic.Add(StateType.Attack, state.animationName);
                    break;
                case StateType.Hit:
                    states.Add(StateType.Hit, new HitState_l(this));
                    parameter.animationDic.Add(StateType.Hit, state.animationName);
                    break;
                case StateType.Death:
                    states.Add(StateType.Death, new DeathState_l(this));
                    parameter.animationDic.Add(StateType.Death, state.animationName);
                    break;
                default:
                    states.Add(StateType.Null, null);
                    parameter.animationDic.Add(StateType.Null, state.animationName);
                    break;
            }
        }
    }
}
