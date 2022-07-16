using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundLogic : MonoBehaviour
{
    public static RoundLogic rl { get; private set; } //sets this script as a singleton, only one of this script must exist and it can be accessed anywere

    [SerializeReference]
    private GameObject[] enemies;
    [SerializeField]
    private float waveDelay, enemyDelay;
    [SerializeField]
    private Vector2 topLeftBound, bottomRightBound;

    private int waveCount, currentWave, enemiesAlive, waveEnemies, roundCount;

    private void Start() {
        if (rl != null && rl != this) { //makes sure only one of this script exists and sets defins singleton
            Destroy(this);
        } else {
            rl = this;
        }
    }
    public void startRound(int roundCount) {
        Debug.Log("round start");
        currentWave = 0;
        this.roundCount = roundCount;

        if (roundCount > 2) waveCount = (int)Mathf.Log(roundCount) * 6; //equation calculates how many waves there will be this round
        else waveCount = roundCount;

        StartCoroutine(StartWave());
    }
    private IEnumerator StartWave() {
        Debug.Log("wave start");
        currentWave++;

        if (currentWave > waveCount) { //on last wave ends waves
            currentWave = waveCount; //sets waves to last wave for visual effect
            //end round
            Debug.Log("round end");
            yield break;
        }
        int lowerRange = (int)(Mathf.Sqrt(roundCount) + 5 * Mathf.Log(currentWave) + 2); //equation to calculate lower range of enemies
        int upperRange = (int)(3 * Mathf.Sqrt(roundCount) + 7 * Mathf.Log(currentWave) + 2); //equation to calculate higher range of enemies
        waveEnemies = Random.Range(lowerRange, upperRange);

        yield return new WaitForSeconds(waveDelay); 
        StartCoroutine(SpawnEnemies());
    }
    private IEnumerator SpawnEnemies() { //spanws all enemies this wave with a delay of enemyDelay
        Debug.Log("spawn enemies");
        enemiesAlive = waveEnemies;

        for (int i = 0; i < waveEnemies; i++) { 
            StartCoroutine(Spawn());
            yield return new WaitForSeconds(enemyDelay);
        }
        yield return new WaitWhile(() => enemiesAlive > 0); //wont continues corutine until all enemies are dead
        StartCoroutine(StartWave());
    }
    private IEnumerator Spawn() { //spawns a specific random enemy from enemies
        Debug.Log("spawned");
        GameObject enemy = enemies[Random.Range(0, enemies.Length)];
        Vector2 coordinates = new Vector2(Random.Range(topLeftBound.x, bottomRightBound.x), Random.Range(topLeftBound.y, bottomRightBound.y)); //randomises spawn location within bounds set
        //spawn animation
        yield return new WaitForSeconds(1);
        Instantiate(enemy, coordinates, Quaternion.identity);
    }

    //setter methods
    public void ChangeEnemyCount(int set) {
        enemiesAlive += set;
    }
}
