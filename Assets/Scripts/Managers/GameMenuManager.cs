using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class GameMenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI pingText;

     private PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Update()
    {
        pingText.text = "Ping: " + PhotonNetwork.GetPing();
    }

    public void Exit()
    {
        // Application.Quit();
         PhotonNetwork.LeaveRoom();
    }

    public void StartLevel()
    {
        PV.RPC("RPC_StartLevel", RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    private void RPC_StartLevel()
    {
        /*�������� ������ ����.
         �.�. ��������� ������� �� �������. ������� ������ ���� 3,2,1.. ������!
        ���������� ������� � ���*/
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene("Menu");
    }
}
