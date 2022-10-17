using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerController : MonoBehaviourPun
{
    [SerializeField] private float speed;
    [SerializeField] private TextMeshProUGUI nameText;

    private new PhotonView photonView;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        if (photonView.IsMine)
        {
            nameText.text = photonView.Owner.NickName;
            nameText.color = Color.white;
        }
        else
        {
            nameText.text = photonView.Owner.NickName;
            nameText.color = Color.red;
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) // чтобы не влиять на других игроков
            return;

        playerMovement.Move();
        playerMovement.Jump();
    }

    private void OnDestroy()
    {
        FunnyNames.RemoveName(nameText.text);
    }
}
