using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{

    public static float playerMaxHealth;

    private void Start()
    {
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

<<<<<<< HEAD
=======
    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        healthBar.SetHealth(playerHealth);
    }

    public void TakeHeal(float heal)
    {
        playerHealth += heal;
        if(playerHealth > 100f) { playerHealth = 100f; }

        healthBar.SetHealth(playerHealth);
    }
>>>>>>> 57e9f8eb795dc406c4d893aa80d6cc369a2921c3

    public void SetMaxHealthUpgrade() //Fonction à appeler lors d'une upgrade de point de vie
    {
        health = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
    }

}
