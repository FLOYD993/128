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
    [Header("��������")]
    public int maxHealth;
    public float walkSpeed;
    public float runSpeed;
    public float idleTime;

    [Header("���Χ")]
    public Transform[] patrolPoints;
    public Transform[] chasePoints;

    [Header("����")]
    public LayerMask targetLayer;
    public Transform attackPoint;
    public float attackArea;

    [Header("״̬ & ����")]
    public AnimationType[] animationTypes;
    public Dictionary<StateType, string> animationDic = new Dictionary<StateType, string>();

    [Header("״̬ - ��������")]
    public int currHealth;
    public bool isHit;
    public bool isDead;

    [Header("���� - ��������")]
    public string enemyType;
    public Transform target;
    public SkeletonAnimation skeletonAnimation;

    public void OnStart()
    {
        currHealth = maxHealth;
    }


}
