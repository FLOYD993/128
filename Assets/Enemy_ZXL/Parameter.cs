using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
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
    public float moveSpeed;
    public float chaseSpeed;
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
    //public AnimationReferenceAsset idle;
    //public AnimationReferenceAsset patrol;
    //public AnimationReferenceAsset chase;
    //public AnimationReferenceAsset react;
    //public AnimationReferenceAsset attack;
    //public AnimationReferenceAsset hit;
    //public AnimationReferenceAsset death;

    [Header("��������")]
    public Transform target;
    public SkeletonAnimation skeletonAnimation;

    public bool isHit;
    public int currentHealth;


    public void OnStart()
    {
        currentHealth = maxHealth;
    }
}
