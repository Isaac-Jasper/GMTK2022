using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    //Variables holding value that determine the magintude of movment
    [Header("Movement Modifiers")]
    [SerializeField] float minSpeed= 10f;
    [SerializeField] float maxSpeed = 15f;
    [SerializeField] float jumpForce = 50f;

    //Variables that reference other attributes and GameObjects
    [Header("References")]
    public TextMeshProUGUI xOutput;
    public Rigidbody2D rb;

  

    private float speedModifier;

    private void Start()
    {
        speedModifier = minSpeed;
    }

    private void Update()
    {
        float accelerate = Input.GetAxisRaw("Fire3");
        float currentXInput = Input.GetAxisRaw("Horizontal");
        xOutput.text = "" + speedModifier;

        

        if( accelerate > 0 && currentXInput != 0)
        {
            
            speedModifier = Mathf.Lerp(speedModifier, maxSpeed, 1000);
            print(speedModifier);
        }
        else
        {
            speedModifier = Mathf.Lerp(speedModifier, minSpeed, 1000);
        }

        float movement = Mathf.Clamp(speedModifier * currentXInput, -1f * maxSpeed, maxSpeed);

        rb.velocity = new Vector2(movement, rb.velocity.y);
    }
}
