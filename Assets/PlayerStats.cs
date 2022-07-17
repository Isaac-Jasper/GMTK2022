using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    [Header("Health Stats")]
    [SerializeField] int maxHealth;

    [Header("References")]
    [SerializeField] LayerMask Enemy;

    [Header("Knockback")]
    [SerializeField] float knockBack;

    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize the health counter to the max health
        currentHealth = maxHealth;
    }

    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if collision is with an enemy
        if(collision.collider.CompareTag("Enemy"))
        {
            //Get an array of all objects within a radius of twice the player scale on the enemy layer
            RaycastHit2D[] hit = Physics2D.CircleCastAll(new Vector2(transform.position.x,transform.position.y), transform.localScale.x * 2f, new Vector2(0, 0), 0, Enemy);
            
            //For every enemy within the apply a force in the direction away from the player
            for (int i = 0; i < hit.Length; i++)
            {
                hit[i].rigidbody.AddForce((hit[i].transform.position - transform.position).normalized*knockBack, ForceMode2D.Impulse) ;
            }

            //reduce the Hp counter by 1
            currentHealth--;
        }
        
    }


}
