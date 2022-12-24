using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask enemyLayers;

    public Transform spell1AttackPoint;
    public float spell1AttackRange = 0.5f;

    //Les conditions d'attaque : l'input pour attaquer, cooldown de l'attaque, vérifier que le personnage est en position pour attaquer (vérifier qu'un autre attaque n'est pas lancé)
    private bool attacking = false;

    public float spell1AttackTime = 0.25f; //temps de l'animation de l'attaque 1 (spell 1) 
    private float spell1AttackTimeTimer = 0f;
    public float spell1CoolDown = 3f; //cooldown de l'attaque 1 (spell 1) 
    private float spell1CoolDownTimer = 0f;




    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && spell1CoolDownTimer <= 0 && attacking == false) //premiere attaque(spell 1), clic gauche
        {
            Spell1Attack();
            Debug.Log("trigger");
        }

        if (Input.GetMouseButtonDown(1)) //deuxieme attaque(spell 2), clique droit
        {

        }

        //if (Input.GetKeyDown(KeyCode.Space)) //deuxieme attaque(spell 3), sur espace
        {
            
        }

        if(spell1CoolDownTimer > 0) //gestion du CoolDown du spell 1
        {
            spell1CoolDownTimer -= Time.deltaTime;
        }

        if (attacking)
        {
            if(spell1AttackTimeTimer > 0)
            {
                spell1AttackTimeTimer -= Time.deltaTime;
            }
            else { attacking = false; }
        }


    }

    public void Spell1Attack()
    {
        attacking = true; //nous attaquons
        spell1CoolDownTimer = spell1CoolDown; //lancement du cooldown de l'attaque 
        spell1AttackTimeTimer = spell1AttackTime; //lancement de l'animation

        //lancer les animations


        //detecter les enemy in range
        Collider[] hitEnemies = Physics.OverlapSphere(spell1AttackPoint.position, spell1AttackRange, enemyLayers);

        //infliger les degats aux ennemies
        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(20);
        }
    }
}
