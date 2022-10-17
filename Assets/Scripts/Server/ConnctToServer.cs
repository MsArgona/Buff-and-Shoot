using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnctToServer : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.SendRate = 100; //30
        PhotonNetwork.SerializationRate = 60; //10

        PhotonNetwork.ConnectUsingSettings();
    }

    //Подключились к серверу
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Menu");
    }
}
