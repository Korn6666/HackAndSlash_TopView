using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Health : MonoBehaviour
{

    public float maxHealth;
    public float health;

    public HealthBar healthBar;
    public GameObject spawnEnemy;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        spawnEnemy = GameObject.FindGameObjectWithTag("Spawn");
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
        if (gameObject.layer == 9)
        {
            StartCoroutine(TakeKnockBackCoroutine());
        }
        Vector3 dir = Vector3.ProjectOnPlane(target - transform.position, new Vector3(0.0f, 1.0f, 0.0f));
        GetComponent<Rigidbody>().AddForce(dir.normalized * knockbackPower, ForceMode.Impulse);
    }

    public void TakeKnockUp(float knockupPower)
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * knockupPower, ForceMode.Impulse);
    }

    IEnumerator TakeKnockBackCoroutine()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<NavMeshAgent>().enabled =  true;
        yield return null;
    }
}
