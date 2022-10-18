using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SharpballSpawner : MonoBehaviourPun
{
    [SerializeField] private GameObject[] sharpballs;
    [SerializeField] private float activateRate = 20f;
    //[SerializeField] private Transform[] spawnPoses;

    private int index = 0;

    //private PhotonView PV;

    private void Start()
    {
       // PV = GetComponent<PhotonView>();

        Deactivate();
        GameMenuManager.onGameStarted += StartActivate;
    }

    private void StartActivate()
    {
        InvokeRepeating("Activate", 6f, activateRate);
    }

    private void Deactivate()
    {
        for (int i = 0; i < sharpballs.Length; i++)
        {
            sharpballs[i].SetActive(false);
        }
    }

    private void Activate()
    {
        if (index >= sharpballs.Length)
            return;

        sharpballs[index].SetActive(true);
        index++;

        //PV.RPC("RPC_Spawn", RpcTarget.AllBufferedViaServer);
    }

    private void OnDisable()
    {
        GameMenuManager.onGameStarted -= StartActivate;
    }

    //[PunRPC]
    //private void RPC_Spawn()
    //{
    //    if (spawnPoses.Length == 0)
    //        return;

    //    int index = Random.Range(0, spawnPoses.Length - 1);

    //    var tmpVec2 = new Vector2()
    //    {
    //        x = spawnPoses[index].position.x,
    //        y = spawnPoses[index].position.y
    //    };

    //    PhotonNetwork.Instantiate(sharpballPref.name, tmpVec2, Quaternion.identity);
    //}
}
