using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject lootXp; //Notre object pour donner 1 d'xp

    public float enemyMaxHealth = 100f;
    public float enemyHealth;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = enemyMaxHealth;
        healthBar.SetMaxHealth(enemyMaxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        healthBar.SetHealth(enemyHealth);

        if(enemyHealth <= 0)
        {
            Instantiate(lootXp, transform.position, transform.rotation); //fait spawn le loot xp
            Destroy(gameObject); //Détruis l'ennemie
        }
    }

    public void TakeHeal(float heal)
    {
        enemyHealth += heal;
        if (enemyHealth > 100f) { enemyHealth = 100f; }

        healthBar.SetHealth(enemyHealth);
    }

    public void TakeKnockBack(Vector3 target, float knockbackPower)
    {
        Vector3 dir = Vector3.ProjectOnPlane(target - transform.position, new Vector3(0.0f, 1.0f, 0.0f));
        GetComponent<Rigidbody>().AddForce(dir.normalized * knockbackPower, ForceMode.Impulse);
    }
}
