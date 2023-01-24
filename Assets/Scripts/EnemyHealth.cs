using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class EnemyHealth : Health
{
    public GameObject lootXp; //Notre object pour donner 1 d'xp
    [SerializeField] private GameObject damageText;
    private Animator enemyAnimator;
    [SerializeField] private AudioSource CoupRecu;

    new public void TakeDamage(float damage)
    {
        if (damage>0)
        {
            CoupRecu.Play();
        }
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
            spawnEnemy.GetComponentInParent<WaveManager>().OnDestroy();
        }

        if (health > maxHealth) //Empêcher 
        {
            health = maxHealth;
        }
    }

    public void TakeStun(float timer)
    {
        StartCoroutine(TakeStunCoroutine(timer));
    }

    IEnumerator TakeStunCoroutine(float timer)
    {
        enemyAnimator = gameObject.GetComponent<Animator>();
        enemyAnimator.enabled = false;
        gameObject.GetComponent<EnemyAttack>().enabled = false;
        gameObject.GetComponent<EnemyMovement>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;

        yield return new WaitForSeconds(timer);

        enemyAnimator.enabled = true;
        gameObject.GetComponent<EnemyAttack>().enabled = true;
        gameObject.GetComponent<EnemyMovement>().enabled = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
    }
}
