using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<int> numbers;
    public GameObject dice;
    public Camera Cam3D;
    private void Start() {
        Cam3D.gameObject.SetActive(true);
    }
    private void Update() {
        if (Input.GetKey(KeyCode.Space)) Instantiate(dice, transform.position, transform.rotation); //for testing
        if (Input.GetKey(KeyCode.X)) average();//for testing
    }
    public void average() {//for testing
        int total = 0;
        for (int i = 0; i < numbers.Count; i++) {
            total += numbers[i];
        }
        Debug.Log("average roll is: " + (float) total / numbers.Count);
    }
}
