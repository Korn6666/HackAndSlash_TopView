using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Orbe : Projectiles
{
    private GameObject playerPivot;
    private float orbeHitCount;
    private List<Collider> alreadyHit; //list pour stocker les enemy deja touché

    // Start is called before the first frame update
    void Start()
    {
        //playerMageAttack = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerMageAttack>();
        playerPivot = playerMageAttack.gameObject;
        orbeHitCount = playerMageAttack.spell2HitCount;
        StartCoroutine(OrbeLifeTime());
        alreadyHit = new List<Collider>(); //list pour stocker les enemy deja touché
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(playerPivot.transform.position, Vector3.up, speed * Time.deltaTime);
        transform.position = playerPivot.transform.position + (transform.position - playerPivot.transform.position).normalized * 2;
        //transform.position = playerPivot.transform.TransformPoint(playerPivot.transform.position);


        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, playerMageAttack.spell2Range, enemyLayers); //infliger les degats aux ennemies

        foreach (Collider enemy in hitEnemies) //infliger les degats aux ennemies
        {
            if(!alreadyHit.Contains(enemy)) // ici on check si l'ennemi n'a pas déjà été touché
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(playerMageAttack.spell2Damage);
                enemy.GetComponent<EnemyHealth>().TakeKnockBack(playerPivot.transform.position, playerMageAttack.spell2Knockback);
                OrbeTakeHit(1);
                alreadyHit.Add(enemy);
            }
        }
    }

    private void OrbeTakeHit(float hit) //nombre de hit avant 
    {
        orbeHitCount -= hit;

        if(orbeHitCount <= 0)
        {
            OrbeDestroy();
        }
    }

    IEnumerator OrbeLifeTime() // temps de vie de 5 sec
    {
        yield return new WaitForSeconds(5f);
        OrbeDestroy();
    }

    private void OrbeDestroy()
    {
        Destroy(gameObject);
    }
}
