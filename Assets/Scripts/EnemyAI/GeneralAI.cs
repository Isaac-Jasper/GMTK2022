using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAI : MonoBehaviour
{
    [SerializeField]
    private int health = 20;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject Player;

    public GameObject temp; //for testing, holds the dice prefab. in the future will be done through player
    private void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Hit(GameObject hitDie, GameObject goldDie, float knockback) { //hit die is the dice that wil be rolled to deal damage
        GameObject dieAmmo = Instantiate(hitDie, transform.position + Vector3.back + Vector3.back, Quaternion.identity);
        StartCoroutine(HitWait(dieAmmo, goldDie, knockback));
    }
    IEnumerator HitWait(GameObject die, GameObject dieGold, float knockback) { //deals the damage after dice has rolled
        rb.AddForce((transform.position - Player.transform.position).normalized * knockback, ForceMode2D.Impulse);
        yield return new WaitForSeconds(2);
        int damage = die.GetComponent<DiceRoll>().getSide();
        health -= damage;
        if (health <= 0) Death(dieGold);
    }
    private void Death(GameObject dieGold) {
        RoundLogic.rl.ChangeEnemyCount(-1); //decreases enemy count
        Instantiate(dieGold, transform.position, Quaternion.identity);
        Instantiate(dieGold, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
