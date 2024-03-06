using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PhysicsCheck PhysicsCheck;
    private PlayerController playerController;

    public GameObject myBag;
    bool isOpen;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
        PhysicsCheck = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        SetAnimation();
        OpenMyBag();
    }
    public void SetAnimation()
    {
        anim.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));
        //anim.SetFloat("velocityX", 100*Mathf.Abs(rb.velocity.x));
        //Debug.Log();
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("IsGround", PhysicsCheck.isGround);
        anim.SetBool("IsDead", playerController.isDead);
        anim.SetBool("IsAttack",playerController.isAttack);
        anim.SetBool("IsWall", PhysicsCheck.IsWall);
        anim.SetBool("IsSlide", playerController.IsSlide);
        //anim.SetBool("IsTalk", playerController.isTalk);
        //anim.SetInteger("combo", playerController.combo);
    }
    public void PlayHurt()
    {
        anim.SetTrigger("Hurt");
    }
    public void PlayerAttack()
    {
        anim.SetTrigger("Attack");
    }

    void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.B))//B¼ü´ò¿ª±³°ü
        {
            isOpen = !isOpen;
            myBag.SetActive(isOpen);
        }
    }
}
