using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //References the main camera
    
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
    [SerializeField] GameObject dice; //ISAAC ADDITION - temp dice variable, will eventually have a proper way to store dice prefabs, with potential for multiple dice, probably an array
    [SerializeField] GameObject diceGold;
    [SerializeField] Camera mainCam;
    [SerializeField] LayerMask Environment;
    [SerializeField] int diceMulltiplier;

    [Header("Shooting Settings")]
    [SerializeField] float firerate;
    [SerializeField] float knockback; //ISAAC ADDITION - knockback stat

    private void Start()
    {
        //Sets mainCam to reference the main camera, reset the cooldown to 0 and sets gunSprite to the sprite Renderer on the gun 
     
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
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, mousePos - transform.position, 1000, Environment); //ISAAC ADDITION - added raycast to mouse position, reverted to old 3d camera overlay to get the aim correct, so the 2d camera is the main camera now, the distance needs to be a big number otherwise an error is thrown if you click outside distance

           
            
            if ( hit.Length != 0 && hit[0].collider.CompareTag("Enemy")) { //ISAAC ADDITION - if ray hits an enemy it hits
                 hit[0].collider.GetComponent<GeneralAI>().Hit(dice, diceGold, knockback, 1);
                for(int i = 1; i < hit.Length; i++ )
                {
                    if(hit[i].collider.CompareTag("Enemy"))
                    {
                        hit[i].collider.GetComponent<GeneralAI>().Hit(dice, diceGold, knockback,diceMulltiplier);
                    }
                    else
                    {
                        break;
                    }
                }
             }

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
