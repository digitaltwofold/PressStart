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

    public static float speed = 5;

    public float currentPosition;

    public static float maxSpeed = 10;
    public static float acceleration = 2;
    
    public static float jumpTakeOff = 7f;

    public static Vector2 velocity = Vector2.zero;


    public static float gravityModifier = 1f;


    private void Start()
    {
        //only collisioncheck with platform mask
        platformLM = LayerMask.GetMask("Platform");
        //get components
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //Get input from user and check for collsisions
        InputCheck(); 
        currentPosition = rb.position.x;
    }

    private void FixedUpdate()
    {
        //movementsystem based on acceleration
        Move();
        //gravity
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        
        if (GroundCheck() && velocity.y <= 0)
        {
            velocity.y = 0;
        }
    }

    //Check player input
    private void InputCheck()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
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
    
}
