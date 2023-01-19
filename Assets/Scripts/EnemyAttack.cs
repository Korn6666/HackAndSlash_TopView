using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float swordAttackCoolDown = 1;
    public float swordAttackAnimationTime = 1.3f; //Temps d'animation    
    public float swordAttackDamages = 5;

    public bool canAttack;
    public GameObject player;
    public Animator Animator;
    public float knockbackPower = 0;

    //private bool firstAttack = true;




    protected void Start()
    {
        StartCoroutine(WaitAndAttack());
        player = GameObject.FindGameObjectWithTag("Player");
        Animator = gameObject.GetComponent<Animator>();
    }
    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        //firstAttack = true;
    //        //canAttack = true;
    //        //player = collision.gameObject;
    //        //Animator.SetBool("onPlayerContact", true);
    //    }
    //}

    //void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        firstAttack = false;
    //        canAttack = false;
    //        Animator.SetBool("onPlayerContact", false);
    //    }
    //}

    public void SwordAttack()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(swordAttackDamages);
        player.GetComponent<PlayerHealth>().TakeKnockBack(transform.position, knockbackPower);
    }

    private IEnumerator WaitAndAttack()
    {
        while (true)
        {
            //if (firstAttack) { yield return new WaitForSeconds(swordAttackCoolDown - swordAttackAnimationTime); }
            canAttack = gameObject.GetComponent<EnemyMovement>().canAttack;
            if (canAttack)
            {
                Animator.SetTrigger("Attack");
                yield return new WaitForSeconds(swordAttackAnimationTime); //Histoire d'attendre de faire les degats quand l'épée touche le joueur
                canAttack = gameObject.GetComponent<EnemyMovement>().canAttack;
                if (canAttack)
                {
                    SwordAttack();
                    yield return new WaitForSeconds(swordAttackCoolDown - swordAttackAnimationTime);
                }
            }
            yield return null;
        }
    }
}
