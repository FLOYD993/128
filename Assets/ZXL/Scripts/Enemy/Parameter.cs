using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public enum StateType
{
    Null, Idle, Patrol, Chase, React, Attack, Hit, Death
}

[Serializable]
public struct AnimationType
{
    public StateType type;
    [SpineAnimation]
    public string animationName;
}



[Serializable]
public class Parameter
{
    [Header("基础参数")]
    public int maxHealth;
    public float walkSpeed;
    public float runSpeed;
    public float idleTime;

    [Header("活动范围")]
    public Transform[] patrolPoints;
    public Transform[] chasePoints;

    [Header("攻击")]
    public LayerMask targetLayer;
    public Transform attackPoint;
    public float attackArea;

    [Header("状态 & 动画")]
    public AnimationType[] animationTypes;
    public Dictionary<StateType, string> animationDic = new Dictionary<StateType, string>();

    [Header("状态 - 无需设置")]
    public int currHealth;
    public bool isHit;
    public bool isDead;

    [Header("其他 - 无需设置")]
    public string enemyType;
    public Transform target;
    public SkeletonAnimation skeletonAnimation;

    public void OnStart()
    {
        currHealth = maxHealth;
    }


}
