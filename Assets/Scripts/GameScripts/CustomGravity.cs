using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour {
    public float gravityScale = 1.0f;
    public static float Gravity = -9.81f;
    bool grounded = false;
    Rigidbody rb;

    void OnEnable() {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
    public void setGravity(float set) {
        gravityScale = set;
    }
    void FixedUpdate() {
        if (!grounded) {
            Vector3 gravity = Gravity * gravityScale * Vector3.back;
            rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }
}