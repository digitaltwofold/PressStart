using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static Rigidbody2D rb;
    private BoxCollider2D bc;
    public float horizontalMovement;
    private LayerMask platformLM;

    //movement acceleration
    private float initialVelocity = 4f;
    private float maxVelocity = 6f;
    private float currentVelocity = 0;
    private float accRate = 1.5f;
    private float decRate = 2.5f;

    private bool isWallsliding;

    public static float speed = 5;

    public int facingDir = 1;
    public float currentPosition;


    public static float maxSpeed = 10;
    public static float acceleration = 2;
    
    public static float jumpTakeOff = 7f;

    private Vector2 slideJump;

    public static Vector2 velocity = Vector2.zero;

    private bool facingRight = true;

    public static float gravityModifier = 1f;
    public static float slideSpeed = 2;


    private void Start()
    {
        //only collisioncheck with platform mask
        platformLM = LayerMask.GetMask("Platform");
        //get components
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        //Player ist facing right at the beginning of the scene
        facingRight = true;
    }

    void Update()
    {
        //Get input from user and check for collsisions
        InputCheck();
        flipPlayer(horizontalMovement);
        WallSlideCheck();
        slideJump = new Vector2(3 * -facingDir, 7);
        
        currentPosition = rb.position.x;


    }

    private void FixedUpdate()
    {
        //movementsystem based on acceleration
        Move();
        //gravity
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        //what to do if wallsliding
        if (isWallsliding && (velocity.y <= slideSpeed))
        {
            velocity.y = -slideSpeed;
            if(Input.GetAxisRaw("Vertical") < 0)
            {
                velocity.y = -slideSpeed*3;
            }
        }
        if (GroundCheck() && velocity.y <= 0)
        {
            velocity.y = 0;
        }
    }

    //Check player input
    private void InputCheck()
    {
        //don't let player move on wallslide
        if (!isWallsliding)
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
        }

        Jump();
    }
    // Player movement
    private void Move()
    {
        if(horizontalMovement != 0)
        {
            currentVelocity = currentVelocity + (accRate * Time.deltaTime);
        }
        else
        {
            currentVelocity = currentVelocity - (decRate * Time.deltaTime);
        }
        currentVelocity = Mathf.Clamp(currentVelocity, initialVelocity, maxVelocity);
        velocity.x = currentVelocity * horizontalMovement;
        rb.velocity = velocity;
    }
    //jump function
    private void Jump()
    { 
        if (Input.GetButtonDown("Jump") && GroundCheck())
        {
            velocity.y = jumpTakeOff;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }
        if (Input.GetButtonDown("Jump") && isWallsliding && (Input.GetAxisRaw("Horizontal") != facingDir))
        {
            velocity.y = 6.5f;
            velocity.x = 3;
            horizontalMovement = -facingDir;
            if(rb.velocity.y > 0)
            {
                Invoke("SetWalljumpBool",1f);
            }
        }
    }
        //flip player sprite
       private void flipPlayer(float move)
    {
        if (move > 0 && !facingRight || move < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

            facingDir *= -1;
        }
    }

    //Raycast and Groundcheck function
    private bool GroundCheck()
    {
        float safeSpace = 0.02f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, safeSpace, platformLM);
        Color rayColor;
        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(bc.bounds.center, Vector2.down * (bc.bounds.extents.y + safeSpace), rayColor);
        return raycastHit.collider != null;
    }
    private bool WallcheckRight()
    {
        float safeSpace = 0.01f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.right, safeSpace, platformLM);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(bc.bounds.center, Vector2.right * (bc.bounds.extents.y + safeSpace), rayColor);
        return raycastHit.collider != null;
    }
    private bool WallcheckLeft()
    {
        float safeSpace = 0.01f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.left, safeSpace, platformLM);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(bc.bounds.center, Vector2.left * (bc.bounds.extents.y + safeSpace), rayColor);
        return raycastHit.collider != null;
    }
    private bool Wallcheck()
    {
        bool isTrue = false;
        if(!facingRight)
        {
            isTrue = WallcheckLeft();
        }
        else if(facingRight)
        {
            isTrue = WallcheckRight();
        }
        return isTrue;
    }
    //Wallslide
    public void WallSlideCheck()
    {
        if(Wallcheck() && !GroundCheck() && (velocity.y < 0))
        {
            isWallsliding = true;
        }
        else
        {
            isWallsliding = false;
        }
    }
}
