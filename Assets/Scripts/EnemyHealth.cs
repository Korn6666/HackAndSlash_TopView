using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
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
    }

    public void TakeHeal(float heal)
    {
        enemyHealth += heal;
        if (enemyHealth > 100f) { enemyHealth = 100f; }

        healthBar.SetHealth(enemyHealth);
    }

    public void TakeKnockBack(Vector3 target, float knockbackPower)
    {
        Vector3 dir = target - transform.position;
        transform.Translate(dir.normalized * knockbackPower); // !!!on utilisera un addforce des que les rigied body seront ready!!!
    }
}
