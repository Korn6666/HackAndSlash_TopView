using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Projectiles
{

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.velocity = playerMageAttack.transform.forward * speed * Time.deltaTime; // use translate for bugs
    }

    void OnCollisionEnter(Collision collision) //Permet de savoir si le joueur a ramassé l'xp
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 12) //Détecte une collision avec enemy et l'environement 
        {
            //detecter les enemy in range
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, playerMageAttack.spell1Range, enemyLayers);

            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(playerMageAttack.spell1Damage);
                enemy.GetComponent<EnemyHealth>().TakeKnockBack(transform.position, -15f);
            }
            Destroy(gameObject); //Détruis l'objet
        }
    }
}
