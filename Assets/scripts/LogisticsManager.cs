using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogisticsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myStrokesTotalText;

    [SerializeField] private int currentStrokes = 0;
    private string strokesText = "Strokes: ";
    private CollisionDetection myColDec;
    // Start is called before the first frame update
    void Start()
    {
        myColDec = GameObject.Find("Putter").GetComponent<CollisionDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myColDec.collided)
        {
            currentStrokes++;
            myColDec.collided = false;
        }
        myStrokesTotalText.text = strokesText + currentStrokes.ToString();
    }   
}
