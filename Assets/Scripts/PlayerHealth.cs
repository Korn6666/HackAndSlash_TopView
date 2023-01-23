using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{

    //public float playerMaxHealth;

    private void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("Player HealthBar").GetComponent<HealthBar>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //maxHealth = playerMaxHealth;

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
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

}
