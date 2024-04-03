using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CollisionDetection : MonoBehaviour
{
    public bool collided = false;
    public string ballName;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            collided = true;
            ballName = collision.gameObject.name;
        }
        
    }
    
}

