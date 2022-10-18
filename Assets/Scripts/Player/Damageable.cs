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

    private GameMenuManager gameMenuManager;

    private void Start()
    {
        IsDead = false;
        curHealth = health;

        gameMenuManager = FindObjectOfType<GameMenuManager>();
    }

    public void TakeDamage(float damage)
    {
       curHealth -= damage;

        if (curHealth <= 0 && !IsDead)
        {
            IsDead = true;
            onDead?.Invoke();
            gameMenuManager.PlayerDead();
        }
    }
}
