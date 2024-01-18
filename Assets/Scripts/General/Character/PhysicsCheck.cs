using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;
    private PlayerController playerController;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    public bool manual;
    public bool IsPlayer;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public bool IsWall;
    public bool isGround;
    public bool touchRwall;
    public bool touchLwall;
    public float CheckRaduis;
    public LayerMask groundLayer;
    // Update is called once per frame
    private void Update()
    {
        Check();
    }
    public void Check() 
    {
        if(IsWall)
            isGround=Physics2D.OverlapCircle((Vector2)transform.position+new Vector2(bottomOffset.x*transform.localScale.x,bottomOffset.y), CheckRaduis, groundLayer);
        else
            isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, 0), CheckRaduis, groundLayer);
        touchLwall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, CheckRaduis, groundLayer);
        touchRwall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, CheckRaduis, groundLayer);
        if(IsPlayer) {
            IsWall = (touchLwall&&playerController.inputDirection.x<0f || touchRwall && playerController.inputDirection.x > 0f) && rb.velocity.y<0f;
        }
            
    }
    private void Awake()
    {
        coll=GetComponent<CapsuleCollider2D>();
        rb=GetComponent<Rigidbody2D>();
        if(!manual)
        {
            rightOffset = new Vector2((coll.bounds.size.x + coll.offset.x) / 2, coll.bounds.size.y / 2);
            leftOffset = new Vector2(-rightOffset.x,rightOffset.y);

        }
        if (IsPlayer)
        {
            playerController = GetComponent<PlayerController>();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, CheckRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, CheckRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, CheckRaduis);
    }
}
