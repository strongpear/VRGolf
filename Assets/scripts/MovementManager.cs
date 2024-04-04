using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;


public class MovementManager : MonoBehaviour
{ 

    public int playerID; 
    
    private PhotonView myView;
    private GameObject myChild;

    private float xInput;
    private float yInput;

    public float speed = 5f;


    private InputData inputData;
    private Transform myXrRig;

    private Transform xrCamera;
    private Vector3 headsetForward;
    private Vector3 forwardDirection;
    private Transform rightControllerTransform;

    private LogisticsManager myLogistics;

    private Transform playerSpawn1;
    private Transform playerSpawn2;
    private Transform playerSpawn3;

    private GameObject putter;

    void Start()
    {
        myView = GetComponent<PhotonView>();

        myChild = transform.GetChild(0).gameObject;
        
        putter = GameObject.Find("Putter");

        rightControllerTransform = GameObject.Find("Right Controller").transform;
        GameObject myXrOrigin = GameObject.Find("XR Origin"); 
        myXrRig = myXrOrigin.transform;

        GameObject logistics = GameObject.Find("LogisticsManager");
        myLogistics = logistics.GetComponent<LogisticsManager>();

        inputData = myXrOrigin.GetComponent<InputData>();  

        playerSpawn1 = GameObject.Find("Spawn 1").transform;
        playerSpawn2 = GameObject.Find("Spawn 2").transform;
        playerSpawn3 = GameObject.Find("Spawn 3").transform; 
    }


    void Update()
    {
        if (myView.IsMine)
        {
            myXrRig.position = myChild.transform.position;
            
            if (inputData.leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 movement))
            {
                xInput = movement.x;
                yInput = movement.y;

                // Handle player movement
                float moveHorizontal = xInput;
                float moveVertical = yInput;

                // Calculate movement direction based on player orientation
                Vector3 moveDirection = Camera.main.transform.right * moveHorizontal + forwardDirection * moveVertical;
                // moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
                moveDirection.y = 0f;
                // Move the player
                transform.position += moveDirection * speed * Time.deltaTime;
                Debug.Log(moveDirection);
                // Recalculate forward direction
                forwardDirection = Vector3.ProjectOnPlane(Camera.main.transform.forward, Camera.main.transform.position.normalized).normalized;

            }
            // Teleportation? (WIP)
            if (inputData.rightController.TryGetFeatureValue(CommonUsages.trigger, out float triggered))
            {
                if (triggered > 0.3)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(rightControllerTransform.position, rightControllerTransform.forward, out hit, Mathf.Infinity))
                    {
                        if (hit.collider.tag == "teleportObjectTag")
                        {
                            if (hit.collider.name == "Banner 1")
                            {
                                transform.position = playerSpawn1.position;
                                transform.rotation = playerSpawn1.rotation;
                            }
                            else if (hit.collider.name == "Banner 2")
                            {
                                transform.position = playerSpawn2.position;
                                transform.rotation = playerSpawn3.rotation;
                            }
                            else if (hit.collider.name == "Banner 3")
                            {
                                transform.position = playerSpawn3.position;
                                transform.rotation = playerSpawn3.rotation;
                            }
                        }
                    }
                }
            }


            // Putter Respawn
            if (inputData.rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool pressed))
            {
                if (pressed)
                {
                    putter.transform.position = transform.position + new Vector3(0,2,0);
                }
            }
            if (myLogistics.gameOver)
            {
                myView.RPC("communicateScore", RpcTarget.Others, myLogistics.currentStrokes);
                // Game Over, communicate score
                myLogistics.resetGameOver();
            }
        }
    }

    [PunRPC]
    void communicateScore(int score)
    {
        myLogistics.opponentStrokes = score;
    }
}




// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR;
// using Photon.Pun;

// public class MovementManager : MonoBehaviour
// {
//     private PhotonView myView;
//     private GameObject myChild;

//     private float xInput;
//     private float yInput;
//     private float movementSpeed = 10.0f;

//     private InputData inputData;
//     //[SerializeField] private GameObject myObjectToMove;
//     private Rigidbody myRB;
//     private Transform myXrRig;

//     // Start is called before the first frame update
//     void Start()
//     {
//         myView = GetComponent<PhotonView>();

//         myChild = transform.GetChild(0).gameObject;
//         myRB = myChild.GetComponent<Rigidbody>();
        
//         GameObject myXrOrigin = GameObject.Find("XR Origin"); 
//         myXrRig = myXrOrigin.transform;
//         inputData = myXrOrigin.GetComponent<InputData>();   
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//         if (myView.IsMine)
//         {
//             myXrRig.position = myChild.transform.position;
//             if (inputData.rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 movement))
//             {
//                 xInput = movement.x;
//                 yInput = movement.y;
//             }
//             else
//             {
//                 xInput = Input.GetAxis("Horizontal");
//                 yInput = Input.GetAxis("Vertical");
//             }    
//         }

//     }

//     private void FixedUpdate()
//     {
//         myRB.AddForce(xInput * movementSpeed, 0, yInput * movementSpeed);
//     }
// }
