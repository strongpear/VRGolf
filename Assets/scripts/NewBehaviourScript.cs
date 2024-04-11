using UnityEngine;
using System.Collections;

public class BallSpeedController : MonoBehaviour
{
    public float speedIncreaseAmount = 5f; // Amount by which to increase the speed
    public float duration = 5f; // Duration of the speed boost
    public GameObject ball; // Reference to the ball GameObject

    private bool isBoostActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball && !isBoostActive)
        {
            isBoostActive = true;
            StartCoroutine(ActivateSpeedBoost());
            gameObject.SetActive(false); // Deactivate the power-up object
        }
    }

    IEnumerator ActivateSpeedBoost()
    {
        // Increase the speed of the ball
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        float originalSpeed = ballRigidbody.velocity.magnitude;
        ballRigidbody.velocity = ballRigidbody.velocity.normalized * (originalSpeed + speedIncreaseAmount);

        // Wait for the duration of the speed boost
        yield return new WaitForSeconds(duration);

        // Reset the speed of the ball after the duration
        ballRigidbody.velocity = ballRigidbody.velocity.normalized * originalSpeed;

        // Reset flag
        isBoostActive = false;
    }
}