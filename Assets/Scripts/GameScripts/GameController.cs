using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<int> numbers;
    public GameObject enemy;
    public GameObject dice;
    public Camera Cam3D;
    private void Start() {
        Cam3D.gameObject.SetActive(true);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) enemy.GetComponent<GeneralAI>().Hit(dice); //for testing
        if (Input.GetKey(KeyCode.X)) average(); //for testing
    }
    public void average() {//for testing
        int total = 0;
        for (int i = 0; i < numbers.Count; i++) {
            total += numbers[i];
        }
        Debug.Log("average roll is: " + (float) total / numbers.Count);
    }
}
