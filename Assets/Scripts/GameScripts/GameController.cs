using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int roundCount;

    private void Start() {
        RoundLogic.rl.startRound(roundCount); //starts round, will have trigger in future
    }
}
