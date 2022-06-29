using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myrigidbody;
    Animator myAnimator;
    CapsuleCollider2D mycapsuleCollider;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float playerSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mycapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipingSprite();
        
    }



    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        
    }
    void OnJump(InputValue value)
    {
        if (!mycapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }/*The function prevent over jumping when it touches to ground after that player can jump*/
        if (value.isPressed)
        {
            myrigidbody.velocity = new Vector2(0f, jumpSpeed);
        }
    }
    void Run()
    {
        float newSpeed = moveInput.x * playerSpeed;
        Vector2 playerVelocity = new Vector2(newSpeed, myrigidbody.velocity.y);
        myrigidbody.velocity = playerVelocity;
        bool Playerhashorizontalmove = Math.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", Playerhashorizontalmove);
    }
    void FlipingSprite()
    {
        bool Playerhashorizontalmove = Math.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        if (Playerhashorizontalmove)
        {
            transform.localScale = new Vector2(Math.Sign(myrigidbody.velocity.x), 1f);
           
        }
        

    }
   
}
