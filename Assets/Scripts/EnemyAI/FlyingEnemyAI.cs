using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAI : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform enemySprite;
    [SerializeReference]
    private float speed;
    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update() {
        AI();
    }
    private void AI() { //moves the fly towards the player and switches the flies direction to face player
        rb.AddForce((playerTransform.position - transform.position).normalized * speed);
        if (rb.velocity.x < 0) enemySprite.eulerAngles = new Vector3(0, 0, 0);
        else enemySprite.eulerAngles = new Vector3(0, 180, 0);
    }
}
