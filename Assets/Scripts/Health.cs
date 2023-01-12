using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float maxHealth;
    public float health;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame


    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if (health <= 0f) //condition de destruction 
        {
            Destroy(gameObject); //Détruis l'objet
        }

        if(health > maxHealth) //Empêcher 
        {
            health = maxHealth;
        }
    }


    public void TakeKnockBack(Vector3 target, float knockbackPower)
    {
        Vector3 dir = Vector3.ProjectOnPlane(target - transform.position, new Vector3(0.0f, 1.0f, 0.0f));
        GetComponent<Rigidbody>().AddForce(dir.normalized * knockbackPower, ForceMode.Impulse);
    }
}
