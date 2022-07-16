using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //Variables holding value that determine the magnitude of movement
    [Header("Movement Modifiers")]
    [SerializeField] float minSpeed= 10f;
    [SerializeField] float maxSpeed = 15f;
    [SerializeField] float jumpForce = 50f;
    [SerializeField] float fallMulitplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;
    
    //Variables that reference other attributes and GameObjects
    [Header("References")]
    [SerializeField] LayerMask groundLayer;
    

    //Stores magnitude of the player speed based on whether the accelerate key(Fire3 or Lshift) is pressed or not
    private float speedModifier;
    
    Rigidbody2D rb;

    private void Start()
    {
        //Set rb to reference this objects rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        //Sets the players speed to the base value
        speedModifier = minSpeed;
    }

    private void Update()
    {
        //Apply both horizontal movement and vertical movement every frame
        HorizontalMovement();
        VerticalMovement(); 
    }

    private void HorizontalMovement()
    {
        //Gets input from the horizontal joystick/Left and Right, as well as the state of LShift
        float accelerate = Input.GetAxisRaw("Fire3");
        float currentXInput = Input.GetAxisRaw("Horizontal");

        //Check if the player is moving and if the Lshift key is down, then lerps the speed modifier of the player to its max, otherwise, lerp the speed modifier to its min.
        if (accelerate > 0 && currentXInput != 0)
        {
            speedModifier = Mathf.Lerp(speedModifier, maxSpeed, 1000);
            
        }
        else
        {
            speedModifier = Mathf.Lerp(speedModifier, minSpeed, 1000);
        }

        //Make sure the speed to be applied to the player does not exceed the max speed.
        float movement = Mathf.Clamp(speedModifier * currentXInput, -1f * maxSpeed, maxSpeed);

        //Apply the speed to the x component of the player, preserving the y velocity
        rb.velocity = new Vector2(movement, rb.velocity.y);
    }

    //Controls Vertical Translations
    private void VerticalMovement()
    {
        
        //If the Player is grounded and the jump button is pressed, then execute a jump by applying vertical velocity
        if (Grounded() && Input.GetButtonDown("Jump"))
        {
            print("Jumping");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        //Apply the smooth jump logic
        ApplyGravity();
    }

    //Check whether the player is grounded by raycast
    private bool Grounded()
    {
        //Store the result of a 2d raycast, which only hits things on the environment layer
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, transform.localScale.y * 0.7f,groundLayer); //ISAAC ADDITION - added transform.localscale.x so the size of the isgrounded check scales linearly with cube size
        //If the raycast hits something, return true, else return false


        return hit.collider != null;
    }

    //Apply smooth jump logic
    public void ApplyGravity()
    {
        //If the player is falling, increase gravity during their descent, else if the player releases early, apply a similar magnitude of gravity
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMulitplier - 1) * Time.deltaTime;
        }else if(rb.velocity.y >0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
