using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class GolfGameManager : MonoBehaviour
{
    public Transform hole; // Reference to the hole's transform
    public Transform teleportTarget; // Reference to the teleport target for the next course
    public GameObject player; // Reference to the player GameObject
    public GameObject putter; // Reference to the putter GameObject
    public Text strokesText; // Reference to the UI text for strokes

    // Public property to access stroke count from other scripts
    public int Strokes
    {
        get { return strokes; }
        set { strokes = value; UpdateStrokesUI(); } // Update UI when setting strokes
    }

    private int strokes = 0; // Variable to keep track of strokes
    private bool ballInHole = false; // Variable to track if the ball is in the hole
    private bool lastFramePutterSwing = false; // Variable to track the state of the putter swing in the last frame

    private void Awake()
    {
        // Initialize the strokes count to 0
        strokes = 0;
        UpdateStrokesUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) // Check if the collider is the golf ball
        {
            ballInHole = true;
            Debug.Log("Ball in hole!");
        }
        else if (other.CompareTag("TeleportObject")) // Check if the collider is the teleport object
        {
            // Hide or deactivate the ball
            other.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the Oculus controllers are connected
        if (!XRSettings.isDeviceActive)
            return;

        // Check if the putter has been swung
        bool currentPutterSwing = CheckPutterSwing();

        // If the putter has been swung in this frame and it wasn't swung in the last frame, count a stroke
        if (currentPutterSwing && !lastFramePutterSwing)
        {
            strokes++;
            Debug.Log("Stroke count: " + strokes);
        }

        lastFramePutterSwing = currentPutterSwing;

        if (ballInHole)
        {
            // Allow the player to look at a certain object (e.g., flag) and press the left trigger to teleport
            InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            if (leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonPressed) && primaryButtonPressed)
            {
                // Calculate the distance between the player and the teleport target
                float distanceToTeleport = Vector3.Distance(player.transform.position, teleportTarget.position);

                if (distanceToTeleport < 2f) // Check if player is close enough to teleport
                {
                    // Teleport the player to the next course
                    player.transform.position = teleportTarget.position;
                    Debug.Log("Teleported to next course!");

                    // Reset ballInHole flag and stroke count
                    ballInHole = false;
                    strokes = 0;
                }
            }
        }
    }

    // Check if the putter has been swung
    private bool CheckPutterSwing()
    {
        // Modify this method according to how you detect a putter swing in your game
        // This example assumes the putter has a Rigidbody and a collider, and we're checking if it's moving fast enough
        Rigidbody putterRigidbody = putter.GetComponent<Rigidbody>();
        if (putterRigidbody != null && putterRigidbody.velocity.magnitude > 2f)
        {
            return true;
        }
        return false;
    }

    // Update UI to display current strokes
    private void UpdateStrokesUI()
    {
        if (strokesText != null) // Ensure strokesText is not null before accessing it
            strokesText.text = "Strokes: " + strokes.ToString();
    }
}