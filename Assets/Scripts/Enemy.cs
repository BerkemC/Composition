using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health;

    public int Health { get => health; set => health = value; }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Mathf.Clamp(health, 0, 999);

        if(health == 0) { Destroy(gameObject); }
    }
}
