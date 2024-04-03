using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerObject;
    // public Transform putterSpawn1;
    // public Transform putterSpawn2;
    // public Transform putterSpawn3;
    public Transform ballSpawn1;
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.Instantiate("NetworkPlayer", transform.position, transform.rotation);
        // PhotonNetwork.Instantiate("Putter", putterSpawn1.position, putterSpawn1.rotation);
        // PhotonNetwork.Instantiate("Ball", ballSpawn1.position, ballSpawn1.rotation);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerObject);
    }
}
