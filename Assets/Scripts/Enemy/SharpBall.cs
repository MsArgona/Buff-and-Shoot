using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

public class SharpBall : MonoBehaviourPun
{
    [SerializeField] private float damage = 5f;

    [SerializeField] private Transform[] poses;

    [SerializeField] private float speed = 2f;

    private bool onPos;
    private Transform randPos;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        randPos = GetRandomPos();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        Move();
    }

    private void Move()
    {
        onPos = (transform.position.x == randPos.position.x) ? true : false;

        if (!onPos) //не дошел до точки
        {
            agent.SetDestination(randPos.position);
        }
        else //ƒошел до точки - выбираем новую
        {
            randPos = GetRandomPos();
            onPos = false;
        }
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
