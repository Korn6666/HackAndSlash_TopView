using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : Health
{
    public GameObject lootXp; //Notre object pour donner 1 d'xp
    [SerializeField] private GameObject damageText;

     new public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if(damage > 0)
        {
            GameObject text = Instantiate(damageText, transform.position + Vector3.up * 2 + Vector3.forward, Quaternion.identity);
            text.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().SetText(damage.ToString("F0"));
        }

        if (health <= 0)
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
