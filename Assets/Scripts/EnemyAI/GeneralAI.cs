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
        health = 1 + (int)Mathf.Sqrt(RoundLogic.rl.GetRound());
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Hit(GameObject hitDie, GameObject goldDie, float knockback, int diceCount) { //hit die is the dice that wil be rolled to deal damage
        SFXControlleer.sfx.PlaySound("EnemyHit");

        GameObject[] dieAmmo = new GameObject[diceCount];
        for (int i = 0; i < diceCount; i++) {
            dieAmmo[i] = Instantiate(hitDie, transform.position + Vector3.back + Vector3.back, Quaternion.identity);
            dieAmmo[i].SetActive(true);
        }
        StartCoroutine(HitWait(dieAmmo, goldDie, knockback, diceCount));
    }
    IEnumerator HitWait(GameObject[] die, GameObject dieGold, float knockback, int diceCount) { //deals the damage after dice has rolled
        rb.AddForce((transform.position - Player.transform.position).normalized * knockback, ForceMode2D.Impulse);
        yield return new WaitForSeconds(2);
        int damage = 0;
        for (int i = 0; i < diceCount; i++) {
            int add = die[i].GetComponent<DiceRoll>().getSide();
            if (add == -1) add = 999999;
            damage += add;
        }
        health -= damage;
        if (health <= 0) Death(dieGold, diceCount);
    }
    private void Death(GameObject dieGold, int diceCount) {
        RoundLogic.rl.ChangeEnemyCount(-1); //decreases enemy count
        for (int i = 0; i < diceCount; i++) {
            Instantiate(dieGold, transform.position, Quaternion.identity).SetActive(true);
        }
        SFXControlleer.sfx.PlaySound("Splat");

        Destroy(gameObject);
    }
}
