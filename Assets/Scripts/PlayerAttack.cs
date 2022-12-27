using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask enemyLayers;
    private bool attacking = false;


    //SPELL 1
    public Transform spell1AttackPoint;
    public float spell1AttackRange = 0.5f;

    //Les conditions d'attaque : l'input pour attaquer, cooldown de l'attaque, vérifier que le personnage est en position pour attaquer (vérifier qu'un autre attaque n'est pas lancé)
    public float spell1AttackTime = 0.25f; //temps de l'animation de l'attaque 1 (spell 1) 
    private float spell1AttackTimeTimer = 0f;
    public float spell1CoolDown = 1f; //cooldown de l'attaque 1 (spell 1) 
    private float spell1CoolDownTimer = 0f;





    //SPELL 3
    public Transform spell3AttackPoint;
    public float spell3AttackRange = 2f;

    //Les conditions d'attaque : l'input pour attaquer, cooldown de l'attaque, vérifier que le personnage est en position pour attaquer (vérifier qu'un autre attaque n'est pas lancé)
    public float spell3AttackTime = 3f; //temps de l'animation du spell 3 
    private float spell3AttackTimeTimer = 0f;
    public float spell3CoolDown = 5f; //cooldown du spell 3 
    private float spell3CoolDownTimer = 0f;





    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && spell1CoolDownTimer <= 0 && !attacking) //premiere attaque(spell 1), clic gauche
        {
            Debug.Log("trigger spell 1" + attacking);
            Spell1Attack();
        }

        if (Input.GetMouseButtonDown(1)) //deuxieme attaque(spell 2), clique droit
        {

        }

        if (Input.GetKeyDown(KeyCode.Space) && spell3CoolDownTimer <= 0 && attacking == false) //deuxieme attaque(spell 3), sur espace
        {
            StartCoroutine(Spell3Attack());
            Debug.Log(attacking);
        }



        // COOLDOWN
        if(spell1CoolDownTimer > 0) //gestion du CoolDown du spell 1
        {
            spell1CoolDownTimer -= Time.deltaTime;
        }
        if (spell3CoolDownTimer > 0) //gestion du CoolDown du spell 3
        {
            spell3CoolDownTimer -= Time.deltaTime;
        }



        //CASTTIME
        if (attacking)
        {
            if(spell1AttackTimeTimer > 0)
            {
                spell1AttackTimeTimer -= Time.deltaTime;
            }
            else if (spell3AttackTimeTimer > 0)
            {
                spell3AttackTimeTimer -= Time.deltaTime;
            }
            else { attacking = false; }
        }


    }

    public void Spell1Attack()
    {
        attacking = true; //nous attaquons
        spell1CoolDownTimer = spell1CoolDown; //lancement du cooldown de l'attaque 
        spell1AttackTimeTimer = spell1AttackTime; //lancement de l'animation

        //lancer les animations A FAIRE!!!!!!


        //detecter les enemy in range
        Collider[] hitEnemies = Physics.OverlapSphere(spell1AttackPoint.position, spell1AttackRange, enemyLayers);

        //infliger les degats aux ennemies
        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(20); // 20 de degats est a titre de test, on appelera un fonction pour calculer les DD
           // enemy.GetComponent<EnemyHealth>().TakeKnockBack(transform.position, -2); //Ajoute le knockback à la cible
        }
    }


    IEnumerator Spell3Attack()
    {
        attacking = true; //nous attaquons
        spell3CoolDownTimer = spell3CoolDown; //lancement du cooldown de l'attaque 
        spell3AttackTimeTimer = spell3AttackTime; //lancement de l'animation
        float timer = 1f;
        while(attacking)
        {
            if(timer >= 1)
            {
                Collider[] hitEnemies = Physics.OverlapSphere(spell3AttackPoint.position, spell3AttackRange, enemyLayers); //infliger les degats aux ennemies

                foreach (Collider enemy in hitEnemies) //infliger les degats aux ennemies
                {
                    enemy.GetComponent<EnemyHealth>().TakeDamage(5); // 5 de degats est a titre de test, on appelera un fonction pour calculer les DD
                }

                timer = 0f;
            }
            timer += Time.deltaTime;
            yield return null;
        }
        yield return null;

    }
}
