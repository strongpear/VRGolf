// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR;
// public class TeleportationManager : MonoBehaviour
// {
    
//     public Transform controllerTransform;
//     public InputData inputData;
//     // Update is called once per frame
//     void Start()
//     {
//         GameObject myXrOrigin = GameObject.Find("XR Origin"); 
//         inputData = myXrOrigin.GetComponent<InputData>();   
//     }
//     void Update()
//     {
//         if (inputData.rightController.TryGetFeatureValue(CommonUsages.trigger, out bool triggered))
//         {
//             RaycastHit hit;
//             if (Physics.Raycast(controllerTransform.position, controllerTransform.forward, out hit, Mathf.Infinity))
//             {
//                 if (hit.collider)
//                 {
//                     Debug.Log("Target Found Raycast");
//                 }
//                 if (hit.collider.tag == "teleportObjectTag")
//                 {
//                     Debug.Log("Teleport Object Tag Found");
//                 }
//             }
//         }
//     }
// }
