using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDetection : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    public HealthBar healthBar;

    
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy destroyed by bullet");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            currentHealth--;
            if (healthBar != null)
            {
                healthBar.SetHealth(currentHealth);
            }
        }
    }
}

