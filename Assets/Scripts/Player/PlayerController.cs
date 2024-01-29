using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    public PlayerParameter parameter;

    // Start is called before the first frame update
    public Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    public Yongshi InputControl;
    public Vector2 inputDirection;
    public PlayerAnimation playerAnimation;
    public Character character;
    [Header("精力限制")]
    public float currentPower;
    public float maxPower;
    [Header("基本参数")]
    public float speed;
    public float jumpForce;
    public float hurtForce;
    public float walljumpforce;
    public float horizontal;
    public float dashdis;
    public float dashspeed;

    //public int combo;
    [Header("状态")]
    public bool isMove;
    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    public bool walljump;
    public bool IsSlide;
    public int SlideCost;

    public static PlayerController Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        character = GetComponent<Character>();
        InputControl = new Yongshi();
        InputControl.Gameplay.Jump.started += Jump;//+=注册
        InputControl.Gameplay.Attack.started += PlayerAttack;
        InputControl.Gameplay.Dash.started += Dash;

        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Dash(InputAction.CallbackContext context)
    {

        if (!IsSlide && (character.currentPower > character.cost))
        {
            IsSlide = true;
            var targetPos = new Vector3(transform.position.x + dashdis * transform.localScale.x, transform.position.y);
            gameObject.layer = LayerMask.NameToLayer("Enemy");
            StartCoroutine(TriggerDash(targetPos));
            GetComponent<Character>().OnDash(SlideCost);
        }


    }
    private IEnumerator TriggerDash(Vector3 target)
    {
        InputControl.Gameplay.Disable();
        do
        {
            yield return null;
            //if(!physicsCheck.isGround)
            //    break;
            if (physicsCheck.touchLwall && transform.localScale.x < 0f || physicsCheck.touchRwall && transform.localScale.x > 0f)
            {
                IsSlide = false;
                break;
            }
            rb.MovePosition(new Vector2(transform.position.x + transform.localScale.x * dashspeed, transform.position.y));
        } while (Mathf.Abs(target.x - transform.position.x) > 0.1f);
        IsSlide = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
        InputControl.Gameplay.Enable();
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayerAttack();
        isAttack = true;
        //combo++;
        //if (combo >= 3)
        //    combo = 0;
    }

    private void OnEnable()
    {
        InputControl.Enable();
    }
    private void OnDisable()
    {
        InputControl.Disable();
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        inputDirection = InputControl.Gameplay.Move.ReadValue<Vector2>();

        if (!isHurt && !isAttack)
            Move();
        CheckState();
    }

    private void Move()
    {
        if (!walljump)
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        int faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1; //整数化 手柄遥感力度为小数
        transform.localScale = new Vector3(faceDir, 1, 1);//翻转人物

    }
    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("J");
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            IsSlide = false;
            StopAllCoroutines();
        }
        else if (physicsCheck.IsWall)
        {
            rb.AddForce(new Vector2(-inputDirection.x, 2f) * walljumpforce, ForceMode2D.Impulse);
            walljump = true;
        }
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }
    public void PlayerDead()
    {
        isDead = true;
        InputControl.Gameplay.Disable();
    }
    private void CheckState()
    {
        if (physicsCheck.IsWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2f);

        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        if (walljump && rb.velocity.y < 0)
            walljump = false;
        if (isDead || IsSlide)
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        else
            gameObject.layer = LayerMask.NameToLayer("Player");
    }
}

