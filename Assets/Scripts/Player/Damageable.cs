using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float health = 10f;
    private float curHealth;

    private void Start()
    {
        curHealth = health;
    }

    public void TakeDamage(float damage)
    {
       curHealth -= damage;

        if (curHealth <= 0)
        {
            Debug.Log("You died");
        }
    }
}
