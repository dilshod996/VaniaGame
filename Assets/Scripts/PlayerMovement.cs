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
    CapsuleCollider2D mycBodyCollider;
    BoxCollider2D myFootCollider;
    SpriteRenderer mysprite;
    float startgravityScale;
    bool isAlive = true;
  

    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float playerSpeed = 2f;
    [SerializeField] float climbingSpeed = 2f;
    

    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mycBodyCollider = GetComponent<CapsuleCollider2D>();
        myFootCollider = GetComponent<BoxCollider2D>();
        startgravityScale = myrigidbody.gravityScale;
        mysprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipingSprite();
        ClimbLadder();
        Die();
        /*MushroomEffect();*/
    }



    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        
    }
    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!myFootCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            Debug.Log("jump");
            return; 
            
        }/*The function prevent over jumping when it touches to ground after that player can jump*/
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
            transform.localScale = new Vector2(Mathf.Sign(myrigidbody.velocity.x), 1f);
           
        }
        

    }
    void ClimbLadder()
    {
        if (!myFootCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) )
        {
            myAnimator.SetBool("isClimbing", false);
            myrigidbody.gravityScale = startgravityScale;
            return;
        }

        Vector2 upVelocity = new Vector2(myrigidbody.velocity.x, moveInput.y * climbingSpeed);
        myrigidbody.velocity = upVelocity;
        myrigidbody.gravityScale = 0f;
        bool Playerhasverticalmove = Math.Abs(myrigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", Playerhasverticalmove);

    }
    void Die()
    {
        if (mycBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            /*float newX = moveInput.x + 10f;*/
            float newY = moveInput.y + 15f;
            myrigidbody.velocity = new Vector2(myrigidbody.velocity.x, newY);
            mycBodyCollider.enabled = !mycBodyCollider.enabled;
            
        }
    }
   
   
}
