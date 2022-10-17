using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] spawnPoses;

    private void Start()
    {
        if (spawnPoses.Length == 0) 
            return;

        int index = Random.Range(0, spawnPoses.Length - 1);

        var tmpVec2 = new Vector2()
        {
            x = spawnPoses[index].position.x,
            y = spawnPoses[index].position.y
        };

        PhotonNetwork.Instantiate(player.name, tmpVec2, Quaternion.identity);
    }
}
