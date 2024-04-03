using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CollisionDetection : MonoBehaviour
{
    public bool collided = false;
    TextMeshProUGUI myStrokesTotalText;
    private int currentStrokes;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            collided = true;
            // currentStrokes++;
            // strokesText += currentStrokes.ToString();
        }
        
    }
    
}

