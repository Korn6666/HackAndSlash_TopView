using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float swordAttackCoolDown = 1;
    [SerializeField] private float swordAttackAnimationTime = 5; //Temps d'animation    
    [SerializeField] private float swordAttackDamages = 5;    

    private float TimeBetweenAttacks;    
    public bool isAttacking = false;
    public bool canAttack = false;
    private GameObject player;



    void Start()
    {
       StartCoroutine(WaitAndAttack(swordAttackCoolDown, swordAttackAnimationTime));
               
        // Ce if/else est une sécurité pour le temps d'animation, s'il est plus grand que le cool down.
        if (swordAttackCoolDown > swordAttackAnimationTime)       
        {
            TimeBetweenAttacks = swordAttackCoolDown;
        }else { TimeBetweenAttacks = swordAttackAnimationTime; }; 
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            canAttack = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canAttack = false;
        }
    }
    public void SwordAttack()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(swordAttackDamages);
    }

    private IEnumerator WaitAndAttack(float swordAttackCoolDown, float swordAttackAnimationTime)
    {
        while (true)
        {
            if (canAttack)
            {
                yield return new WaitForSeconds(TimeBetweenAttacks);
            }
            if (canAttack)
            {
                SwordAttack();
                canAttack = false;
            }
            yield return null;
        }
    }
}
