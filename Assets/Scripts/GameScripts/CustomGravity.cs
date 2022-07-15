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
    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.CompareTag("Ground")) {
            grounded = true;
            rb.useGravity = true;
        }
    }
    void FixedUpdate() {
        if (!grounded) {
            Vector3 gravity = Gravity * gravityScale * Vector3.up;
            rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }
}