using UnityEngine;

public class NewBehaviorScript : MonoBehaviour
{
    public float initialSpeed = 5f; // Initial speed of the ball
    public float speedIncreaseFactor = 1.5f; // Factor by which speed increases upon contact with the certain object

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * initialSpeed; // Set initial velocity
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedIncreaseTrigger")) // Check if the object is the one that increases speed
        {
            rb.velocity *= speedIncreaseFactor; // Increase speed
        }
    }
}