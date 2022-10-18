using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class GameMenuManager : MonoBehaviourPunCallbacks
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI pingText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private GameObject startButton;
    private float currCountdownValue;

    [Header("Audio")]
    [SerializeField] private AudioClip countdownClip;
    [SerializeField] private AudioClip goClip;

    [HideInInspector] public static Action onGameStarted;
    [HideInInspector] public static Action onGameEnd;

    private static int playerCount = 0;

    private PhotonView PV;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
        PhotonNetwork.LeaveRoom();
    }

    public void StartLevel()
    {
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (playerCount < 2) //должно быть минимум два игрока
            return;

        PV.RPC("RPC_StartLevel", RpcTarget.AllBufferedViaServer);
        PhotonNetwork.CurrentRoom.IsOpen = false;
    }

    [PunRPC]
    private void RPC_StartLevel()
    {
        StartCoroutine(StartCountdown());
        startButton.SetActive(false);
        onGameStarted?.Invoke();
    }

    private IEnumerator StartCountdown(float countdownValue = 5)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue >= 0)
        {
            if (currCountdownValue != 0)
            {
                countdownText.text = currCountdownValue.ToString();
                audioSource.PlayOneShot(countdownClip);
            }
            else
            {
                countdownText.text = "GO!";
                audioSource.PlayOneShot(goClip);
            }
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        countdownText.text = "";
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene("Menu");
    }

    //переделать
    public void PlayerDead()
    {
        playerCount--;

        if (playerCount == 1)
        {
            PV.RPC("RPC_Win", RpcTarget.AllBufferedViaServer);
        }
    }

    [PunRPC]
    private void RPC_Win()
    {
        countdownText.text = "We have a winner!";
        onGameEnd?.Invoke();
    }
}
