using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Balance : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private float targetRotation;
    [SerializeField] private float force;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!photonView.IsMine) // ����� �� ������ �� ������ �������
            return;

        Move();
    }

    private void Move()
    {
         rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetRotation, force * Time.deltaTime));
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //���������� �������� ����� ���������� - ��� ����� ���������
        //����� ���������� �������� ��������� �� ������� ���� ��������� (����� ��������� � �.�.)
    }
}
