using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{

    public static float playerMaxHealth;

    private void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("Player HealthBar").GetComponent<HealthBar>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerMaxHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        maxHealth = playerMaxHealth;

        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20f);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(-20f);
        }
    }

    public void SetMaxHealthUpgrade() //Fonction à appeler lors d'une upgrade de point de vie
    {
        health = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
    }

}
