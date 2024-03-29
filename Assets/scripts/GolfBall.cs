using UnityEngine;

public class GolfBallController : MonoBehaviour
{
    public float ballSpeedMultiplier = 1.0f; // Adjust this to control the speed of the ball

    private Rigidbody rb;
    private bool ballInPlay = false;
    private Vector3 prevPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && !ballInPlay)
        {
            // Start the ball's movement
            rb.isKinematic = false;
            ballInPlay = true;

            // Calculate force based on the distance between current and previous position
            Vector3 force = (prevPosition - transform.position) * ballSpeedMultiplier;
            rb.AddForce(force, ForceMode.Impulse);
        }

        prevPosition = transform.position;
    }
}
