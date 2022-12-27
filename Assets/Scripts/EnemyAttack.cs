using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float swordAttackCoolDown = 1;
    [SerializeField] private float swordAttackAnimationTime; //Temps d'animation    
    [SerializeField] private float swordAttackDamages = 5;    

    private float TimeBetweenAttacks;    
    public bool isAttacking = false;
    public bool canAttack = false;
    private GameObject player;



    void Start()
    {
       StartCoroutine(WaitAndAttack(swordAttackCoolDown, swordAttackAnimationTime));
    }

    void Update()
    {

    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            canAttack = true;
        }
    }
    public void SwordAttack()
    {
        isAttacking = true; //nous attaquons

        // Ce if/else est une sécurité pour le temps d'animation, s'il est plus grand que le cool down.
        if (swordAttackCoolDown > swordAttackAnimationTime)       
        {
            TimeBetweenAttacks = swordAttackCoolDown;
        }else { TimeBetweenAttacks = swordAttackAnimationTime; }; 

        player.GetComponent<PlayerHealth>().TakeDamage(swordAttackDamages);
    }

    private IEnumerator WaitAndAttack(float swordAttackCoolDown, float swordAttackAnimationTime)
    {
        while (true)
        {
            if (canAttack)
            {
                SwordAttack();
                isAttacking = true;
            }

            if (isAttacking)
            {
                // oui en effet, canAttack et isAttacking ont les mêmes roles la. Mais dans la symbolique non donc on verra comment je peux améliorer ca. 
                yield return new WaitForSeconds(TimeBetweenAttacks);
                canAttack = false;    
                isAttacking = false;
            }
            yield return null;
        }
    }
}
