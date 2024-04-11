using UnityEngine;

public class BallTeleport : MonoBehaviour
{
    public Transform teleportTarget; // Target position to teleport the ball to

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) // Assuming the ball has a tag "Ball"
        {
            // Teleport the ball to the teleport target position
            other.transform.position = teleportTarget.position;
        }
    }
}