using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int roundCount;
    [SerializeField]
    GameObject camera3D;
    private void Start() {
        camera3D.SetActive(true);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.X)) RoundLogic.rl.startRound(roundCount);
    }
}
