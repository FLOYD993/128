using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;


public class FSM : MonoBehaviour
{
    public Parameter parameter;

    private IState currentState;

    public InfoBarController barController;

    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();

    // Start is called before the first frame update
    void Start()
    {
        // 基础参数设置
        parameter.OnStart();

        // parameter.animator = GetComponent<Animator>();
        parameter.skeletonAnimation = GetComponent<SkeletonAnimation>();
        barController = GetComponentInChildren<InfoBarController>();

        // 注册状态及动画
        RegisterState();

        // 起始状态为Idle
        TransitionState(StateType.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="type">目标状态</param>
    public void TransitionState(StateType type)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = states[type];

        currentState.OnEnter();
    }

    /// <summary>
    /// 反转朝向
    /// </summary>
    /// <param name="target">朝向目标</param>
    public void FlipTo(Transform target)
    {
        if (target == null)         // 没有目标，返回
        {
            return;
        }
        if (IsLookAt(target))       // 目标在面前，不需要转向， 返回
        {
            return;
        }

        if (transform.position.x > target.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (transform.position.x < target.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        barController.OnCharactorFlip();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(parameter.attackPoint.position, parameter.attackArea);
    }

    private void RegisterState()
    {
        foreach (AnimationType state in parameter.animationTypes)
        {
            switch (state.type)
            {
                case StateType.Idle:
                    states.Add(StateType.Idle, new IdleState(this));
                    parameter.animationDic.Add(StateType.Idle, state.animationName);
                    break;
                case StateType.Patrol:
                    states.Add(StateType.Patrol, new PatrolState(this));
                    parameter.animationDic.Add(StateType.Patrol, state.animationName);
                    break;
                case StateType.Chase:
                    states.Add(StateType.Chase, new ChaseState(this));
                    parameter.animationDic.Add(StateType.Chase, state.animationName);
                    break;
                case StateType.React:
                    states.Add(StateType.React, new ReactState(this));
                    parameter.animationDic.Add(StateType.React, state.animationName);
                    break;
                case StateType.Attack:
                    states.Add(StateType.Attack, new AttackState(this));
                    parameter.animationDic.Add(StateType.Attack, state.animationName);
                    break;
                case StateType.Hit:
                    states.Add(StateType.Hit, new HitState(this));
                    parameter.animationDic.Add(StateType.Hit, state.animationName);
                    break;
                case StateType.Death:
                    states.Add(StateType.Death, new DeathState(this));
                    parameter.animationDic.Add(StateType.Death, state.animationName);
                    break;
                default:
                    states.Add(StateType.Null, null);
                    parameter.animationDic.Add(StateType.Null, state.animationName);
                    break;
            }
        }
    }

    public bool IsLookAt(Transform target)
    {
        if (transform.localScale.x * (target.transform.position.x - transform.position.x) > 0)
        {
            return true;
        }

        return false;
    }
}
