﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float playerMaxHealth = 100f;
    public float playerHealth;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(20f);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeHeal(20f);
        }
    }

    void TakeDamage(float damage)
    {
        playerHealth -= damage;
        healthBar.SetHealth(playerHealth);
    }

    void TakeHeal(float heal)
    {
        playerHealth += heal;
        if(playerHealth > 100f) { playerHealth = 100f; }

        healthBar.SetHealth(playerHealth);
    }
}
