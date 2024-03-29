using UnityEngine;
using UnityEngine.UI;

public class YourOtherScript : MonoBehaviour
{
    private GolfGameManager golfGameManager; // Reference to the GolfGameManager script

    private void Start()
    {
        // Find the GolfGameManager script in the scene
        golfGameManager = FindObjectOfType<GolfGameManager>();

        if (golfGameManager == null)
        {
            Debug.LogError("GolfGameManager script not found in the scene!");
            return;
        }

        // Now you can access the strokesText field of the GolfGameManager script
        Text strokesText = golfGameManager.strokesText;

        if (strokesText == null)
        {
            Debug.LogError("Strokes text UI element not assigned in GolfGameManager!");
            return;
        }

        // Do something with the strokesText UI element
        strokesText.text = "Strokes: " + golfGameManager.Strokes.ToString();
    }
}