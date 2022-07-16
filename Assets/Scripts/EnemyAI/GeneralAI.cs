using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAI : MonoBehaviour
{
    [SerializeField]
    private int health = 20;
    [SerializeField]
    private Rigidbody2D rb;
    public void Hit(GameObject hitDie) {
        GameObject die = Instantiate(hitDie, transform.position + Vector3.back + Vector3.back, Quaternion.identity);
        StartCoroutine(HitWait(die));
    }
    IEnumerator HitWait(GameObject die) {
        yield return new WaitForSeconds(2);
        int damage = die.GetComponent<DiceRoll>().getSide();
        health -= damage;
        if (health <= 0) Death();
    }
    private void Death() {
        Destroy(gameObject);
    }
}
