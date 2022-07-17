using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnIndicator : MonoBehaviour
{
    public void Spawn() {
        RoundLogic.rl.SpawnEnemy(transform.position);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            RoundLogic.rl.ChangeEnemyCount(-1);
            Destroy(gameObject);
        }
    }
    public void Die() {
        Destroy(gameObject);
    }
}
