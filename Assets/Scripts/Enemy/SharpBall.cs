using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SharpBall : MonoBehaviourPun
{
    [SerializeField] private float damage = 5f;

    [SerializeField] private Transform[] poses;

    [SerializeField] private float speed = 2f;

    private bool onPos;
    private Transform randPos;

    private void Start()
    {
        randPos = GetRandomPos();
    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        if (!onPos) //не дошел до точки
        {
            Move();
        }
        else //ƒошел до точки - выбираем новую
        {
            randPos = GetRandomPos();
            onPos = false;
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, randPos.position, speed * Time.deltaTime);

        onPos = (transform.position == randPos.position) ? true : false;
    }

    private Transform GetRandomPos()
    {
        var index = Random.Range(0, poses.Length - 1);

        return poses[index];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damageable"))
        {
            collision.GetComponent<Damageable>().TakeDamage(damage);
        }
    }
}
