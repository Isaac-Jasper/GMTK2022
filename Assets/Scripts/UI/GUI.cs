using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GUI : MonoBehaviour
{
    public static GUI gui { get; private set; } //sets this script as a singleton, only one of this script must exist and it can be accessed anywere

    [SerializeField]
    private TMP_Text round, wave, money, health,
        roundShop, waveShop, moneyShop, healthShop;
    [SerializeField]
    private int moneyAmount;
    private void Start() {
        if (gui != null && gui != this) Destroy(this); //makes sure only one isntance of this script exists
        else gui = this;
    }

    //setter methods
    public void SetRound(int round) {
        this.round.text = "Round " + round;
        roundShop.text = this.round.text;
    }
    public void SetWave(int wave, int maxWave) {
        this.wave.text = wave + " / " + maxWave;
        waveShop.text = this.wave.text;
    }
    public void AddMoney(int money) {
        moneyAmount += money;
        this.money.text = "$" + moneyAmount;
        moneyShop.text = this.money.text;
    }
    public void SetHealth(int health) {
        this.health.text = "Round " + health;
        healthShop.text = this.health.text;
    }
    //getter methods
    public int GetMoney() {
        return moneyAmount;
    }
}
