using UnityEngine;

public class MovingObject2 : MonoBehaviour
{
    public float speed = 2f; // Speed of the object's movement
    public float maxHeight = 5f; // Maximum height the object will move to
    public float minHeight = 0f; // Minimum height the object will move to

    private bool movingUp = true; // Flag to track if the object is moving up or down

    private void Update()
    {
        // Move the object up or down based on the movingUp flag
        if (movingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if (transform.position.y >= maxHeight)
                movingUp = false;
        }
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (transform.position.y <= minHeight)
                movingUp = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the object collides with a ball, stop the ball's movement
        if (other.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = other.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballRigidbody.velocity = Vector3.zero;
                ballRigidbody.angularVelocity = Vector3.zero;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the ball exits collision with the object, allow it to move freely
        if (other.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = other.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                // You can add code here to apply some force or impulse to the ball if desired
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player is colliding with the object, and if so, allow the player to walk through it
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}