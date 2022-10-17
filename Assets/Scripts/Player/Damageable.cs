using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float health = 10f;
    private float curHealth;

    [HideInInspector] public bool IsDead { get; private set; }
    [HideInInspector] public Action onDead;

    private void Start()
    {
        IsDead = false;
        curHealth = health;
    }

    public void TakeDamage(float damage)
    {
       curHealth -= damage;

        if (curHealth <= 0)
        {
            IsDead = true;
            onDead();
        }
    }
}
