using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ControllerInput : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool moveLeft;
    private bool moveRight;
    private float horizontalMove;
    public float speed = 15;
    public float jumpSpeed = 5;
    bool isGrounded;
    bool canDoubleJump;
    public float delayBeforeDoubleJump;
    bool facingRight = true;
    public Animator animator;
    [SerializeField] private AudioSource JumpAudio;
    [SerializeField] private AudioSource EatAudio;
    [SerializeField] private AudioSource RunAudio;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        moveLeft = false;
        moveRight = false;
    }

    //I am pressing the left button
    public void PointerDownLeft()
    {
        RunAudio.Play();
        moveLeft = true;
    }

    //I am not pressing the left button
    public void PointerUpLeft()
    {
        RunAudio.Stop();
        moveLeft = false;
    }

    //Same thing with the right button
    public void PointerDownRight()
    {
        RunAudio.Play();
        moveRight = true;
    }

    public void PointerUpRight()
    {
        RunAudio.Stop();
        moveRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));
        MovementPlayer();
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
           
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            canDoubleJump = false;
        }

        else if (other.gameObject.CompareTag("Finish"))
        {
            
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            canDoubleJump = false;
        }

        else if (other.gameObject.CompareTag("clound"))
        {

            isGrounded = true;
            animator.SetBool("IsJumping", false);
            canDoubleJump = false;
        }

        else if (other.gameObject.tag == "Quad")
        {

            isGrounded = true;
            animator.SetBool("IsJumping", false);
            canDoubleJump = false;
        }
    }
    public void jumpButton()
    {
        animator.SetBool("IsJumping", true);
        if (isGrounded)
        {
            
            isGrounded = false;
            rb.velocity = Vector2.up * jumpSpeed;
            Invoke("EnableDoubleJump", delayBeforeDoubleJump);
            JumpAudio.Play();
        }
        if (canDoubleJump)
        {
            
            rb.velocity = Vector2.up * jumpSpeed;
            canDoubleJump = false;
            JumpAudio.Play();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Star")
        {
            GameManager.instance.increasePoint();
            Destroy(collision.gameObject);
            EatAudio.Play();
        }
    }
}

