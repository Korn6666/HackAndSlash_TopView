﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask enemyLayers = 9;
    private bool attacking = false;


    //SPELL 1
    public Transform spell1AttackPoint;
    public float spell1AttackRange = 0.5f;
    public static float spell1Damage = 1f; //Damages

    //Les conditions d'attaque : l'input pour attaquer, cooldown de l'attaque, vérifier que le personnage est en position pour attaquer (vérifier qu'un autre attaque n'est pas lancé)
    public float spell1AttackTime = 0.25f; //temps de l'animation de l'attaque 1 (spell 1) 
    private float spell1AttackTimeTimer = 0f;
    public static float spell1CoolDown = 1f; //cooldown de l'attaque 1 (spell 1) 
    private float spell1CoolDownTimer = 0f;

    private Animator playerAnimator;




    //SPELL 2
    // public Transform spell2AttackPoint; // on utilisera ici le spell3Attackpoint
    public float spell2AttackRange = 10f;
    private bool isOnFloor; //Variable qui nous dit si le player est sur le sol ou non
    public static float spell2Damage = 2f; //Damages

    //Les conditions d'attaque : l'input pour attaquer, cooldown de l'attaque, vérifier que le personnage est en position pour attaquer (vérifier qu'un autre attaque n'est pas lancé)
    public float spell2AttackTime = 2f; //temps de l'animation de l'attaque 2 (spell 2) 
    private float spell2AttackTimeTimer = 0f;
    public static float spell2CoolDown = 5f; //cooldown de l'attaque 2 (spell 2) 
    private float spell2CoolDownTimer = 0f;
    [SerializeField] private float animationJumpWait = 0.25f;




    //SPELL 3
    public Transform spell3AttackPoint;
    public static float spell3AttackRange = 2f; // AOE area range

    //Les conditions d'attaque : l'input pour attaquer, cooldown de l'attaque, vérifier que le personnage est en position pour attaquer (vérifier qu'un autre attaque n'est pas lancé)
    public float spell3AttackTime = 3f; //temps de l'animation du spell 3 
    private float spell3AttackTimeTimer = 0f;
    public static float spell3CoolDown = 5f; //cooldown du spell 3 
    private float spell3CoolDownTimer = 0f;

    public static float spell3Damage = 1f; //Damages

    [SerializeField] private float spell3WaitAnimationTime = 0.75f;
    [SerializeField] public static float jumpForwardForce = 10;






    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        playerAnimator.applyRootMotion = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && spell1CoolDownTimer <= 0 && !attacking) //premiere attaque(spell 1), clic gauche
        {
            Debug.Log("trigger spell 1" + attacking);

            playerAnimator.SetTrigger("Trigger");
            playerAnimator.SetFloat("Trigger Number", 2);
            StartCoroutine(SkillsCooldown.Spell1Cooldown(spell1CoolDown));

            playerAnimator.SetTrigger("BasicAttack");

            StartCoroutine(Spell1Attack());
        }

        if (Input.GetMouseButtonDown(1) && spell2CoolDownTimer <= 0 && !attacking) //deuxieme attaque(spell 2), clique droit
        {
            StartCoroutine(SkillsCooldown.Spell2Cooldown(spell2CoolDown));
            StartCoroutine(Spell2Attack());
        }

        if (Input.GetKeyDown(KeyCode.Space)) //deuxieme attaque(spell 3), sur espace
        {
            StartCoroutine(SkillsCooldown.Spell3Cooldown(spell3CoolDown));
            StartCoroutine(Spell3Attack());
            Debug.Log(attacking);
        }



        // COOLDOWN
        if (spell1CoolDownTimer > 0) //gestion du CoolDown du spell 1
        {
            spell1CoolDownTimer -= Time.deltaTime;
        }
        if (spell2CoolDownTimer > 0) //gestion du CoolDown du spell 2
        {
            spell2CoolDownTimer -= Time.deltaTime;
        }
        if (spell3CoolDownTimer > 0) //gestion du CoolDown du spell 3
        {
            spell3CoolDownTimer -= Time.deltaTime;
        }



        //CASTTIME
        if (attacking)
        {
            if (spell1AttackTimeTimer > 0)
            {
                spell1AttackTimeTimer -= Time.deltaTime;
            }
            else if (spell2AttackTimeTimer > 0)
            {
                
            }
            else if (spell3AttackTimeTimer > 0)
            {
                spell3AttackTimeTimer -= Time.deltaTime;
            }
            else { attacking = false; }
        }


    }

    public IEnumerator Spell1Attack()
    {
        attacking = true; //nous attaquons
        spell1CoolDownTimer = spell1CoolDown; //lancement du cooldown de l'attaque 
        spell1AttackTimeTimer = spell1AttackTime; //lancement de l'animation

        //lancer les animations A FAIRE!!!!!!

        yield return new WaitForSeconds(spell1AttackTime);

        //detecter les enemy in range
        Collider[] hitEnemies = Physics.OverlapSphere(spell1AttackPoint.position, spell1AttackRange, enemyLayers);

        //infliger les degats aux ennemies
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(spell1Damage);
            enemy.GetComponent<EnemyHealth>().TakeKnockBack(transform.position, -11);
        }
        yield return null;
    }

    IEnumerator Spell2Attack()
    {
        attacking = true; //nous attaquons
        spell2CoolDownTimer = spell2CoolDown; //lancement du cooldown de l'attaque 
        spell2AttackTimeTimer = spell2AttackTime; //lancement de l'animation

        //lancer les animations A FAIRE!!!!!!

        playerAnimator.SetTrigger("JumpAttack");


        gameObject.GetComponent<PlayerMovement>().enabled = false; // On désactive le mouvement

        yield return new WaitForSeconds(animationJumpWait);

        GetComponent<Rigidbody>().AddForce(transform.up * 6, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(transform.forward * jumpForwardForce, ForceMode.Impulse); // Fait sauter le joueur en avant

        isOnFloor = false;
        while (isOnFloor == false) // On attend que le player touche de nouveau le sol
        {
            yield return null; 
        }

        //playerAnimator.SetTrigger("OnFloor");

        gameObject.GetComponent<PlayerMovement>().enabled = true; // On redonne accès au mouvement
        Collider[] hitEnemies = Physics.OverlapSphere(spell3AttackPoint.position, spell2AttackRange, enemyLayers); //infliger les degats aux ennemies

        foreach (Collider enemy in hitEnemies) //infliger les degats aux ennemies
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(spell2Damage); // 5 de degats est a titre de test, on appelera un fonction pour calculer les DD
            enemy.GetComponent<EnemyHealth>().TakeKnockBack(transform.position, -11);
        }
        spell2AttackTimeTimer = 0; // permet de reset attacking
        GetComponent<Rigidbody>().velocity = Vector3.zero; // On annule l'impulsion du saut
        yield return null;
    }

    void OnCollisionEnter(Collision collision) //Permet de savoir si le joueur a touché le sol 
    {
        if (collision.collider.name == "Floor") //Détecte une collision avec Floor
        {
            isOnFloor = true;
        }
    }






    IEnumerator Spell3Attack()
    {
        attacking = true; //nous attaquons
        spell3CoolDownTimer = spell3CoolDown; //lancement du cooldown de l'attaque
        spell3AttackTimeTimer = spell3AttackTime; //lancement de l'animation
        playerAnimator.SetTrigger("360Attack");
        yield return new WaitForSeconds(spell3WaitAnimationTime);
        float timer = 1f;
        while (attacking)
        {
            if (timer >= 0.5)
            {
                Debug.Log("yaa");
                Collider[] hitEnemies = Physics.OverlapSphere(spell3AttackPoint.position, spell3AttackRange, enemyLayers); //infliger les degats aux ennemies

                foreach (Collider enemy in hitEnemies) //infliger les degats aux ennemies
                {
                    enemy.GetComponent<EnemyHealth>().TakeDamage(spell3Damage); // 5 de degats est a titre de test, on appelera un fonction pour calculer les DD
                }
                   
                timer = 0f;
            }
            timer += Time.deltaTime;
            yield return null;
        }
        yield return null;

    }
}
