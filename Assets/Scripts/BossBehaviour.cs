using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : EnemyAttack
{
    public bool presence;
    [SerializeField] private float standardBossSpeed;
    [SerializeField] private float standardBossDamages = 5;


    // GroundAttack
    public Transform groundAttackPoint;
    [SerializeField] float groundAttackRange = 10;
    [SerializeField] float groundAttackDamage = 20;
    [SerializeField] private float groundAttackTime = 5;
    public float groundAttackTimeTimer;
    [SerializeField] private float groundAttackForce = 6;
    public bool groundAttacking;
    public float waitForGroundAttackAnimation = 2;

    // BattleCry
    public float waitForBattleCryAnimation = 2;
    [SerializeField] private float BattleCryCoolDown = 10;
    private float BattleCryCoolDownTimer;
    private bool isCrying;

    //Charge
    private float chargeAttackTimer;
    public bool isCharging;
    [SerializeField] private float speedCharge = 10;
    [SerializeField] private float chargeDamages = 20;
    [SerializeField] private LayerMask layerEnemy;


    // SpeedUpgradeCry
    private float speedCrycoolDownTime;
    private float speedCryCoolDownTimer;

    private float speedUpgradeCryTime;
    private float speedUpgradeTimer;

    private float speedCryRange = 10;
    private float waitForSpeedCryAnimation;

    // HealthStep

    private float health;
    private float halfHealth;
    private float quarterHealth;

    private GameObject[] Enemies;


    [SerializeField] private float waitForAnimationTimer;


    public LayerMask playerLayer;


    private void Awake()
    {
        halfHealth = gameObject.GetComponent<EnemyHealth>().maxHealth / 2;
        quarterHealth = gameObject.GetComponent<EnemyHealth>().maxHealth / 4;
    }

    void Update()
    {
        health = gameObject.GetComponent<EnemyHealth>().health;

        //comportement 1
        if (health > halfHealth)
        {
            FirstBehaviour();
        } 
        //comportement2
        else if (health > quarterHealth)
        {
            SecondBehaviour();
        }


        // CoolDowns & chargeAttackTimer 
        if (BattleCryCoolDownTimer > 0)
        {
            BattleCryCoolDownTimer -= Time.deltaTime;
        }
        if (chargeAttackTimer > 0)
        {
            chargeAttackTimer -= Time.deltaTime;
        }
        if (groundAttackTimeTimer > 0)
        {
            groundAttackTimeTimer -= Time.deltaTime;
        }
        if (speedCryCoolDownTimer > 0)
        {
            speedCryCoolDownTimer -= Time.deltaTime;
        }
        if (speedUpgradeTimer > 0)
        {
            speedUpgradeTimer -= Time.deltaTime;
        }


    }

    void OnTriggerEnter(Collider collision)
    {
        if (!isCrying && collision.gameObject.layer != layerEnemy)
        {
            chargeAttackTimer = 0.5f;
            isCharging = false;
        }
        
    }

    IEnumerator groundAttack()
    {
        groundAttacking = true; //nous attaquons
        groundAttackTimeTimer = groundAttackTime;
        
        Animator.SetTrigger("groundAttack");
        // On désactive le mouvement et on s'assure qu'il n'attaque pas
        gameObject.GetComponent<EnemyMovement>().canAttack = false;
        gameObject.GetComponent<EnemyMovement>().enabled = false; 

        yield return new WaitForSeconds(waitForGroundAttackAnimation);

        gameObject.GetComponent<EnemyMovement>().enabled = true; // On redonne accès au mouvement
        Collider[] hitEntities = Physics.OverlapSphere(groundAttackPoint.position, groundAttackRange, playerLayer); //infliger les degats aux ennemies

        foreach (Collider entity in hitEntities) //infliger les degats aux ennemies
        {
            float distance = Vector3.Distance(transform.position, entity.transform.position);
            entity.GetComponent<PlayerHealth>().TakeDamage(groundAttackDamage - distance);
            entity.GetComponent<PlayerHealth>().TakeKnockBack(transform.position, -11);
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero; // On annule l'impulsion du saut
        groundAttacking = false;
        yield return null;
    }

    IEnumerator BattleCry()
    {
        isCrying = true;
        BattleCryCoolDownTimer = BattleCryCoolDown; //lancement du cooldown de l'attaque 
        Animator.SetTrigger("BattleCry");
        isCharging = true;
        gameObject.GetComponent<EnemyMovement>().enabled = false; // On désactive le mouvement

        yield return new WaitForSeconds(waitForBattleCryAnimation);
        gameObject.GetComponent<EnemyMovement>().enabled = true; // On redonne accès au mouvement
        isCrying = false;
        yield return null;
    }

    IEnumerator SpeedUpgradeCry()
    {
        speedCryCoolDownTimer = speedCrycoolDownTime;
        speedUpgradeTimer = speedUpgradeCryTime;

        gameObject.GetComponent<EnemyMovement>().enabled = false; // On désactive le mouvement
        
 
        yield return new WaitForSeconds(waitForSpeedCryAnimation);
        gameObject.GetComponent<EnemyMovement>().enabled = true; // On redonne accès au mouvement

        Collider[] hitEntities = Physics.OverlapSphere(transform.position, speedCryRange);
        foreach (Collider entity in hitEntities) //infliger les degats aux ennemies
        {
            if (entity.tag == "Skeleton" || entity.tag == "Lich")
            {
                entity.gameObject.GetComponent<EnemyMovement>().speedUpgrade = true;
            }
        }

        yield return new WaitForSeconds(speedUpgradeCryTime);

        foreach (Collider entity in hitEntities) //infliger les degats aux ennemies
        {
            if (entity.tag == "Skeleton" || entity.tag == "Lich")
            {
                entity.gameObject.GetComponent<EnemyMovement>().speedUpgrade = false;
            }
        }

        yield return null;



    }

    void FirstBehaviour()
    {
        if (BattleCryCoolDownTimer <= 0 && !groundAttacking && !isCharging && !canAttack)
        {
            StartCoroutine(BattleCry());
        }
        else if (groundAttackTimeTimer <= 0 && !isCharging && chargeAttackTimer < 0)
        {
            StartCoroutine(groundAttack());
        }


        if (isCharging)
        {
            knockbackPower = -11;
            swordAttackAnimationTime = 0.3f;
            Animator.SetBool("isCharging", true);
            Animator.SetFloat("SpeedRun", 1.5f);
            gameObject.GetComponent<EnemyMovement>().speed = speedCharge;
            swordAttackDamages = chargeDamages;

        }
        else
        {
            // Attendre qu'il fasse son attaque de charge
            if (chargeAttackTimer <= 0)
            {
                knockbackPower = 0;
                swordAttackAnimationTime = 1.3f;
                Animator.SetBool("isCharging", false);
                Animator.SetFloat("SpeedRun", 1);
                gameObject.GetComponent<EnemyMovement>().speed = standardBossSpeed;
                swordAttackDamages = standardBossDamages;
            }
        }


    }

    void SecondBehaviour()
    {

    }
}
