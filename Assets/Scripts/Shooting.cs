using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //References the main camera
    private Camera mainCam;
    //References gun Sprite
    private SpriteRenderer gunSprite;
   
    //Stores mouse Position
    private Vector3 mousePos;
    //Holds timer value
    private float coolDown;
    

    [Header("References")]
    [SerializeField] GameObject hand;
    [SerializeField] ParticleSystem tracer;
    [SerializeField] ParticleSystem muzzle;
    [SerializeField] GameObject FlashPoint;
    [SerializeField] GameObject Gun;

    [Header("Shooting Settings")]
    [SerializeField] float firerate;

    
    private void Start()
    {
        //Sets mainCam to reference the main camera, reset the cooldown to 0 and sets gunSprite to the sprite Renderer on the gun 
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        coolDown = 0f;
        gunSprite = Gun.GetComponent<SpriteRenderer>();
       
    }
    private void Update()
    {
        //Gets the mouse position on the screen, and passes them into functions to Rotate the gun and calculate Shooting trajectory, and then flip the weapon sprite when appropriate
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        RotateHand(mousePos);
        Shoot(mousePos);
        FlipWeapon();
        
    }

    private void FlipWeapon()
    {
        //Check the angle of the z axis of the gun, then flip the sprite when necessary
        if(hand.transform.rotation.z >-0.7f && hand.transform.rotation.z < 0.7f)
        {
            gunSprite.flipY = false;
        }
        else
        {
            gunSprite.flipY = true;
        }

        
    }


    private void Shoot(Vector3 mousePosition)
    {
        //If cooldown greater than the set firerate and the mouse is left clicked, create both a bullet tracer and muzzle flash and then raycast for hits(TO BE IMPLEMENTED)
        if(Input.GetButton("Fire1") && coolDown > firerate)
        {
            Instantiate(tracer, FlashPoint.transform.position, FlashPoint.transform.rotation);
            Instantiate(muzzle, FlashPoint.transform.position, FlashPoint.transform.rotation);
            coolDown = 0;
        }
        else
        {//else, do nothing and increment the cooldown
            coolDown++;
        }
    }

    private void RotateHand(Vector3 mousePosition)
    {
        
        //Take the difference between mouse position and the hand position and calculate the angle required for hand to look at mouse and rotate
        Vector3 rotation = mousePosition - hand.transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        hand.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
