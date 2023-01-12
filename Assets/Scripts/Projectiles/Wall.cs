using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Projectiles
{
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>(); //peut etre inutile
        gameObject.transform.localScale += new Vector3(playerMageAttack.spell3WallScale, 0f, 0f);
        WallDamage(playerMageAttack.spell3Damage);
        StartCoroutine(LifeTime());
    }


    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void WallDamage(float damage)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, playerMageAttack.spell3Range, enemyLayers); //infliger les degats aux ennemies

        foreach (Collider enemy in hitEnemies) //infliger les degats aux ennemies
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}
