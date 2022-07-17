using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnIndicator : MonoBehaviour
{
    public void Spawn() {
        RoundLogic.rl.SpawnEnemy(transform.position);
    }
    public void Die() {
        Destroy(gameObject);
    }
}
