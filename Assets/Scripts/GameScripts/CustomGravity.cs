using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour { //the dice gravity, makes it horizontal instead of vertical and gives control over gravity scale
    public float gravityScale = 1.0f;
    public static float Gravity = -9.81f;
    Rigidbody rb;

    void OnEnable() { //normal gravity needs to be off
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
    public void setGravity(float set) { 
        gravityScale = set;
    }
    void FixedUpdate() {
        Vector3 gravity = Gravity * gravityScale * Vector3.back;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}