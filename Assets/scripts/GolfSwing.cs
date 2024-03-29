using UnityEngine;

public class GolfSwing : MonoBehaviour
{
    public float minForce = 500f; // Minimum force to apply to the ball
    public float maxForce = 2000f; // Maximum force to apply to the ball
    public float forceMultiplier = 10f; // Multiplier to adjust force
    public Rigidbody ball; // Reference to the golf ball's Rigidbody

    private Vector3 prevPos; // Previous position of the club controller

    void Start()
    {
        prevPos = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Assuming mouse click or VR controller trigger is used to swing
        {
            Vector3 currentPos = transform.position;
            float velocity = (currentPos - prevPos).magnitude / Time.deltaTime;
            float force = Mathf.Clamp(velocity * forceMultiplier, minForce, maxForce);
            HitBall(force);
            prevPos = currentPos;
        }
    }

    void HitBall(float force)
    {
        Vector3 direction = transform.forward; // Assuming the forward direction of the club is the direction of the swing
        ball.AddForce(direction * force);
    }
}