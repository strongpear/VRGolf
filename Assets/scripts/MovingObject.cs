using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the object
    public float boundary = 5f; // Boundary to change direction
    public GameObject ballEffectPrefab; // Effect to be applied on the ball

    private bool movingRight = true;

    void Update()
    {
        // Move the object left or right based on direction
        if (movingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        // Change direction when reaching boundary
        if (transform.position.x >= boundary)
        {
            movingRight = false;
        }
        else if (transform.position.x <= -boundary)
        {
            movingRight = true;
        }
    }

    // Detect collision with objects
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // Apply effect to the ball
            Instantiate(ballEffectPrefab, other.transform.position, Quaternion.identity);
            // Prevent the ball from passing through
            other.gameObject.SetActive(false);
        }
    }
}