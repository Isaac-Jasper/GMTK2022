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
    private void Update() {
        rb.velocity = playerTransform.position - transform.position;
        if (rb.velocity.x < 0) enemySprite.eulerAngles = new Vector3(0, 0, 0);
        else enemySprite.eulerAngles = new Vector3(0, 180, 0);
    }

}
