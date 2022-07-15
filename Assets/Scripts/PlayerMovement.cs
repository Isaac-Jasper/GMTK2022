using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    //Variables holding value that determine the magnitude of movement
    [Header("Movement Modifiers")]
    [SerializeField] float minSpeed= 10f;
    [SerializeField] float maxSpeed = 15f;
    [SerializeField] float jumpForce = 50f;

    //Variables that reference other attributes and GameObjects
    [Header("References")]
    public TextMeshProUGUI xOutput;
    public Rigidbody2D rb;

    //Stores magnitude of the player speed based on whether the accelerate key(Fire3 or Lshift) is pressed or not
    private float speedModifier;

    private void Start()
    {
        //Sets the players speed to the base value
        speedModifier = minSpeed;
    }

    private void Update()
    {
        //Gets input from the horizontal joystick/Left and Right, as well as the state of LShift
        float accelerate = Input.GetAxisRaw("Fire3");
        float currentXInput = Input.GetAxisRaw("Horizontal");


        xOutput.text = "" + speedModifier;

        
        //Check if the player is moving and if the Lshift key is down, then lerps the speed modifier of the player to its max, otherwise, lerp the speed modifier to its min.
        if( accelerate > 0 && currentXInput != 0){
            speedModifier = Mathf.Lerp(speedModifier, maxSpeed, 1000);
            print(speedModifier);
        }
        else{
            speedModifier = Mathf.Lerp(speedModifier, minSpeed, 1000);
        }

        //Make sure the speed to be applied to the player does not exceed the max speed.
        float movement = Mathf.Clamp(speedModifier * currentXInput, -1f * maxSpeed, maxSpeed);

        //Apply the speed to the x component of the player, preserving the y velocity
        rb.velocity = new Vector2(movement, rb.velocity.y);
    }
}
