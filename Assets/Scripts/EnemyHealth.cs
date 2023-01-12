using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public GameObject lootXp; //Notre object pour donner 1 d'xp

     new public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if(health <= 0)
        {
            Instantiate(lootXp, transform.position + Vector3.up, transform.rotation); //fait spawn le loot xp
            Destroy(gameObject); //Détruis l'ennemie
        }

        if (health > maxHealth) //Empêcher 
        {
            health = maxHealth;
        }
    }




}
