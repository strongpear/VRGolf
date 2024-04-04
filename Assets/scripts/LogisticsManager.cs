using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogisticsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myStrokesTotalText;
    [SerializeField] TextMeshProUGUI myCourseStrokeText;
    [SerializeField] TextMeshProUGUI myFinishText;
    [SerializeField] TextMeshProUGUI myOpponentText;

    [SerializeField] public int currentStrokes = 0;

    [SerializeField] public GameObject hole1;
    [SerializeField] public GameObject hole2;
    [SerializeField] public GameObject hole3;

    private List<bool> finishedList = new List<bool>() {false, false, false};
    private bool finished = false;
    private string gameOverText = "Game Over";
    public bool gameOver = false;

    private List<int> strokesList = new List<int>(){0, 0, 0};
    private string totalStrokesText = "Total Strokes: ";
    private string courseStrokesText = "Course Strokes: ";
    private string opponentText = "";
    
    private CollisionDetection myColDec;
    private CollisionDetection hole1ColDec;
    private CollisionDetection hole2ColDec;
    private CollisionDetection hole3ColDec;

    public int opponentStrokes = -1;
    // Start is called before the first frame update
    void Start()
    {
        myColDec = GameObject.Find("Putter").GetComponent<CollisionDetection>();
        hole1ColDec = hole1.GetComponent<CollisionDetection>();
        hole2ColDec = hole2.GetComponent<CollisionDetection>();
        hole3ColDec = hole3.GetComponent<CollisionDetection>();
        // Populate finished list
    }

    // Update is called once per frame
    void Update()
    {
        // Handle collisions with ball
        if (myColDec.collided)
        {
            currentStrokes++;
            myColDec.collided = false;

            if (myColDec.ballName == "Ball 1")
            {
                strokesList[0]++;
                Debug.Log(strokesList);
            }
            else if (myColDec.ballName == "Ball 2")
            {
                strokesList[1]++;
                Debug.Log(strokesList);
            }
            else if (myColDec.ballName == "Ball 3")
            {
                strokesList[2]++;
                Debug.Log(strokesList);
            }
        }
        myStrokesTotalText.text = totalStrokesText + currentStrokes.ToString();

        // Handle collision with hole
        if (hole1ColDec.collided)
        {
            Debug.Log("Hole 1 Finished");
            finishedList[0] = true;
        }
        if (hole2ColDec.collided)
        {
            Debug.Log("Hole 2 Finished");
            finishedList[1] = true;
        }
        if (hole3ColDec.collided)
        {
            Debug.Log("Hole 3 Finished");
            finishedList[2] = true;
        }

        myCourseStrokeText.text = courseStrokesText + strokesList[0].ToString() + " " + strokesList[1].ToString() + " " + strokesList[2].ToString();
        // Check if game is finished
        if (CheckEndGame())
        {
            HandleEndGame();
            if (opponentStrokes > 0)
            {
                if (currentStrokes > opponentStrokes)
                {
                    myOpponentText.text = "You Lost!";
                }
                else if (currentStrokes < opponentStrokes)
                {
                    myOpponentText.text = "You Won!";
                }
                else
                {
                    myOpponentText.text = "It's a tie!";
                }
            }
            else
            {
                myOpponentText.text = "Waiting for Opponent to Finish";
            }
        }
    }   

    // Helper Functions
    private bool CheckEndGame()
    {
        foreach (bool finished in finishedList)
        {
            if (!finished)
            {
                return false;
            }
        }

        return true;
    }

    // Todo
    private void HandleEndGame()
    {
        gameOver = true;
        // Need to display who wins and how to figure this out
        myFinishText.text = gameOverText;
    }

    public void resetGameOver()
    {
        gameOver = false;
    }
}
