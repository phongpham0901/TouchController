using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool moveLeft;
    private bool moveRight;
    private bool moveUp;
    private bool moveDown;
    private float horizontalMove;
    private float VerticalMove;
    public float speed = 15;
    public float jumpSpeed = 5;
    bool canDoubleJump;
    public float delayBeforeDoubleJump;
    bool facingRight = true;
    CapsuleCollider2D myCapsule;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCapsule = GetComponent<CapsuleCollider2D>();
        moveLeft = false;
        moveRight = false;
        moveUp = false;
        moveDown = false;
    }

    //I am pressing the left button
    public void PointerDownLeft()
    {
        
        moveLeft = true;
    }

    //I am not pressing the left button
    public void PointerUpLeft()
    {
        
        moveLeft = false;
    }

    //Same thing with the right button
    public void PointerDownRight()
    {
       
        moveRight = true;
    }

    public void PointerUpRight()
    {
        
        moveRight = false;
    }

    public void PointerDownUp()
    {

        moveUp = true;
    }

    public void PointerUpUp()
    {

        moveUp = false;
    }

    public void PointerDownDown()
    {

        moveDown = true;
    }

    public void PointerUpDown()
    {

        moveDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        MovementPlayer();
        Climb();
    }

    //Now let's add the code for moving
    private void MovementPlayer()
    {
        //If I press the left button

        if (moveLeft)
        {

            horizontalMove = -speed;
        }

        //if i press the right button
        else if (moveRight)
        {

            horizontalMove = speed;
        }

        //if I am not pressing any button
        else
        {
            horizontalMove = 0;
        }

        if (horizontalMove < 0 && facingRight)
        {
            flip();
        }

        if (horizontalMove > 0 && !facingRight)
        {
            flip();
        }
    }

    void Climb()
    {
        if (moveUp)
        {
            if (myCapsule.IsTouchingLayers(LayerMask.GetMask("Ladder")))
            {
                rb.gravityScale = 0;
                VerticalMove = speed;
                rb.velocity = new Vector2(rb.velocity.x, VerticalMove);
            }
        }

        else if (moveDown && myCapsule.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb.gravityScale = 0;
            VerticalMove = -speed;
            rb.velocity = new Vector2(rb.velocity.x, VerticalMove);
        }

        else if (myCapsule.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb.gravityScale = 0;
            VerticalMove = 0;
            rb.velocity = new Vector2(rb.velocity.x, VerticalMove);
        }

        else if (!myCapsule.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb.gravityScale = 2;
        }
    }

    public void jumpButton()
    {
        
        if (myCapsule.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            canDoubleJump = false;
            rb.velocity = Vector2.up * jumpSpeed;
            Invoke("EnableDoubleJump",0);
        }
        if (canDoubleJump)
        {
            rb.velocity = Vector2.up * jumpSpeed;
            canDoubleJump = false;
        }

    }

    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }

    //add the movement force to the player
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
}
