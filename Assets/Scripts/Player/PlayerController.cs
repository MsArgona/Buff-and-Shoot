using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerController : MonoBehaviourPun
{
    [SerializeField] private TextMeshProUGUI nameText;

    private new PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
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

    private void OnDestroy()
    {
        FunnyNames.RemoveName(nameText.text);
    }
}
