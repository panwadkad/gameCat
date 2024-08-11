using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{

    public float speed = 5f;
    public float jumpForce = 4f;
    public float direction = 1;
    public GatherInput gatherInput;
    public new Rigidbody2D rigidbody2D;
    
    public Animator animator;
    public float reyLength = 1;
    public LayerMask groundLayer;
    public Transform leftPoint;
    public Transform RightPoint;
    private bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        gatherInput = GetComponent<GatherInput>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void SetAnimatorValue(){
        animator.SetFloat("Speed", Mathf.Abs(rigidbody2D.velocity.x));
        animator.SetFloat("vSpeed", rigidbody2D.velocity.y);
        animator.SetBool("Grounded", grounded);
    }

    private void CheckStatus(){
        RaycastHit2D leftCheckHit =  Physics2D.Raycast(leftPoint.position , 
        Vector2.down , reyLength , groundLayer);
        grounded = leftCheckHit;

        RaycastHit2D RightCheckHit =  Physics2D.Raycast(RightPoint.position , 
        Vector2.down , reyLength , groundLayer);
        grounded = RightCheckHit;
       
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimatorValue();
        rigidbody2D.velocity = new Vector2(
            speed * gatherInput.valueX,
            rigidbody2D.velocity.y
        );
        CheckStatus();

    }

    private void FixedUpdate() 
    {
        flip();
        Move();
        JumpPlay();

    }

    private void Move(){
        flip();
        rigidbody2D.velocity = new Vector2(
                    speed * gatherInput.valueX, rigidbody2D.velocity.y
                );
    }

    private void flip(){
        if(gatherInput.valueX * direction < 0){
            transform.localScale =  new Vector3(
                -transform.localScale.x,1,1
            );
            direction *= -1;
        }
    }

    private void JumpPlay()
    {
        if(gatherInput.jumpInput && grounded){
            rigidbody2D.velocity = new Vector2(
                gatherInput.valueX * speed, jumpForce
                
            );

            gatherInput.jumpInput = false;
        }
    }
}
