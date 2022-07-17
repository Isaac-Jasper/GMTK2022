using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float upForce, torque, direction;
    [SerializeField]
    Transform[] diceSides;
    [SerializeField]
    private int side = 1;
    [SerializeField]
    private CustomGravity cg;
    [SerializeField]
    private float fallGravity, maxHeight;
    [SerializeField]
    private bool isGold;

    private bool endOnce;
    private void Start() {
        rollDice();
    }
    private void Update() {
        if (Mathf.Abs(transform.position.z) > maxHeight) cg.setGravity(fallGravity); //increases gravity if position is above (or below) a certain value
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.CompareTag("Ground") && !endOnce) { //when it hits ground it decreases the gravity again, this avoids the dice sinking into the ground
            StartCoroutine(EndRoll());
            cg.setGravity(5);
        }
    }
    IEnumerator EndRoll() {//stops the die from moving and then destroyes it, will also play any exit animation
        endOnce = true;
        yield return new WaitForSeconds(2);
        rb.isKinematic = true;
        if (isGold) addMoney();
        yield return new WaitForSeconds(2);
        //exit animation
        Destroy(gameObject);
    }
    private void rollDice() { //applies initial transformations to die to rotate and send it in the air
        SetStartRotation();

        float xT = Random.Range(-torque, torque);
        float yT = Random.Range(-torque, torque);
        float zT = Random.Range(-torque, torque);
        float xD = Random.Range(-direction, direction);
        float zD = Random.Range(-direction, direction);

        rb.AddForce(new Vector3(xD, zD, -upForce), ForceMode.VelocityChange);
        rb.AddTorque(xT, yT, zT, ForceMode.VelocityChange);
    }
    private void SetStartRotation() { //randomises the upwards facing face on die to maintain randomness
        switch (Random.Range(1, 7)) {
            case 1:
                transform.Rotate(new Vector3(90,0,0));
                break;
            case 2:
                transform.Rotate(new Vector3(-90,0,0));
                break;
            case 3:
                transform.Rotate(new Vector3(0,90,0));
                break;
            case 4:
                transform.Rotate(new Vector3(0,-90,0));
                break;
            case 5:
                transform.Rotate(new Vector3(0,0,90));
                break;
            case 6:
                transform.Rotate(new Vector3(0,0,-90));
                break;
        }
    }
    public int getSide() { //returns the upmost side
        for (int i = 1; i < diceSides.Length; i++) {
            if (diceSides[i].position.z < diceSides[side - 1].position.z) {
                side = i + 1;
            }
        }
        return side;
    }
    public void addMoney() {
        GUI.gui.AddMoney(getSide());
    }
}
