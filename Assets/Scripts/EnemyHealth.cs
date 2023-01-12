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



<<<<<<< HEAD

=======
    public void TakeKnockBack(Vector3 target, float knockbackPower)
    {
        Vector3 dir = Vector3.ProjectOnPlane(target - transform.position, new Vector3(0.0f, 5.0f, 0.0f));
        GetComponent<Rigidbody>().AddForce(dir.normalized * knockbackPower, ForceMode.Impulse);
    }
>>>>>>> 57e9f8eb795dc406c4d893aa80d6cc369a2921c3
}
