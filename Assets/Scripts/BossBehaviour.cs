using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BossBehaviour : EnemyAttack
{
    [SerializeField] private float standardBossSpeed;
    [SerializeField] private float standardBossDamages = 10;


    // GroundAttack
    [SerializeField] float groundAttackRange = 10;
    [SerializeField] float groundAttackDamage = 20;
    [SerializeField] private float groundAttackTime = 5;
    public float groundAttackTimeTimer;
    public bool groundAttacking;
    public float waitForGroundAttackAnimation = 2;
    [SerializeField] private float waitForEarthQuakeAnimation = 1;
    [SerializeField] private GameObject EarthQuake;
    private float distanceToPlayer;
    private string groundAttackAnimationName;
    private float knockupPower;


    // BattleCry
    private float waitForBattleCryAnimation = 2;
    [SerializeField] private float BattleCryCoolDown = 10;
    private float BattleCryCoolDownTimer;
    private bool isCrying;

    //Charge
    [SerializeField]  private float chargeAttackTimer;
    public bool isCharging;
    public Vector3 target;
    [SerializeField] private float speedCharge = 20;
    [SerializeField] private float chargeDamages = 20;
    [SerializeField] private LayerMask layerEnemy;


    // SpeedUpgradeCry
    [SerializeField] private float speedCryCoolDownTime;
    private float speedCryCoolDownTimer;

    [SerializeField] private float speedUpgradeCryTime;
    private float speedUpgradeTimer;

    [SerializeField]  private float waitForSpeedCryAnimation;

    public WaveManager WaveManager;

    // HealthStep

    private float health;
    private float halfHealth;
    private float quarterHealth;

    [SerializeField] private float waitForAnimationTimer;


    public LayerMask playerLayer;


    private void Awake()
    {
        halfHealth = gameObject.GetComponent<EnemyHealth>().maxHealth / 2;
        quarterHealth = gameObject.GetComponent<EnemyHealth>().maxHealth / 4;
        BattleCryCoolDownTimer = BattleCryCoolDown;
        swordAttackCoolDown = 2;
    }

    void Update()
    {
        health = gameObject.GetComponent<EnemyHealth>().health;
        distanceToPlayer = (player.transform.position - transform.position).magnitude;
        
        //Gestion des comportements
        if (health > halfHealth)
        {
            FirstBehaviour();
        }
        //comportement2
        else if (health > quarterHealth)
        {
            SecondBehaviour();
        }
        //comportement3
        else
        {
            ThirdBehaviour();
        }

        //FirstBehaviour();
        //SecondBehaviour();
        //ThirdBehaviour();

        if (isCharging)
        {
            knockbackPower = -11;
            attackAnimationTime = 0;
            gameObject.GetComponent<EnemyMovement>().standardSpeed = speedCharge;
            swordAttackDamages = chargeDamages;
        }
        else
        {
            // Attendre qu'il fasse son attaque de charge
            if (chargeAttackTimer <= 0)
            {
                knockbackPower = 0;
                attackAnimationTime = 0.7f;
                Animator.SetBool("isCharging", false);
                //Animator.SetFloat("SpeedRun", 1);
                gameObject.GetComponent<EnemyMovement>().standardSpeed = standardBossSpeed;
                swordAttackDamages = standardBossDamages;
            }
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
            Debug.Log("Collision mgl");
            chargeAttackTimer = 0.2f;
            isCharging = false;
        }
        
    }

    IEnumerator groundAttack()
    {
        groundAttacking = true; //nous attaquons
        groundAttackTimeTimer = groundAttackTime;

        Animator.SetTrigger(groundAttackAnimationName);
        Animator.ResetTrigger("Attack");

        // On désactive le mouvement et on s'assure qu'il n'attaque pas ni qu'il ne court pas dans le vide
        
        gameObject.GetComponent<EnemyMovement>().canAttack = false;
        Animator.SetBool("ForwardSpeed", false);
        //gameObject.GetComponent<Nav>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<EnemyMovement>().enabled = false; 

        yield return new WaitForSeconds(waitForGroundAttackAnimation);

        Collider[] hitEntities = Physics.OverlapSphere(transform.position, groundAttackRange, playerLayer); //infliger les degats aux ennemies

        foreach (Collider entity in hitEntities) //infliger les degats aux ennemies
        {
            float distance = Vector3.Distance(transform.position, entity.transform.position);
            entity.GetComponent<PlayerHealth>().TakeDamage(groundAttackDamage - distance);
            entity.GetComponent<PlayerHealth>().TakeKnockBack(transform.position, -11);
            entity.GetComponent<PlayerHealth>().TakeKnockUp(knockupPower);
        }

        EarthQuake.SetActive(true);
        yield return new WaitForSeconds(waitForEarthQuakeAnimation);
        EarthQuake.SetActive(false);
        gameObject.GetComponent<EnemyMovement>().enabled = true; // On redonne accès au mouvement
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
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
        // On désactive le mouvement et on s'assure qu'il n'attaque pas ni qu'il ne court pas dans le vide
        gameObject.GetComponent<EnemyMovement>().canAttack = false;
        Animator.SetBool("ForwardSpeed", false);
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<EnemyMovement>().enabled = false; // On désactive le mouvement

        yield return new WaitForSeconds(waitForBattleCryAnimation);
        Animator.SetBool("isCharging", true);
        target = player.transform.position;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        gameObject.GetComponent<EnemyMovement>().enabled = true; // On redonne accès au mouvement
        isCrying = false;
       
        yield return null;
    }

    IEnumerator SpeedUpgradeCry()
    {
        speedCryCoolDownTimer = speedCryCoolDownTime;
        speedUpgradeTimer = speedUpgradeCryTime;

        Animator.SetTrigger("SpeedCry");
        // On désactive le mouvement et on s'assure qu'il n'attaque pas ni qu'il ne court pas dans le vide
        gameObject.GetComponent<EnemyMovement>().canAttack = false;
        Animator.SetBool("ForwardSpeed", false);
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<EnemyMovement>().enabled = false; // On désactive le mouvement
        
        yield return new WaitForSeconds(waitForSpeedCryAnimation);

        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        gameObject.GetComponent<EnemyMovement>().enabled = true; // On redonne accès au mouvement

        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject entity in gameObjects) 
        {
            if (entity.tag == "Skeleton" || entity.tag == "Lich")
            {
                entity.GetComponent<EnemyMovement>().speedUpgrade = true;
            }
        }

        yield return new WaitForSeconds(speedUpgradeCryTime);

        foreach (GameObject entity in gameObjects) 
        {
            if (entity.tag == "Skeleton" || entity.tag == "Lich")
            {
                entity.GetComponent<EnemyMovement>().speedUpgrade = false;
            }
        }

        yield return null;
    }

    private void FirstBehaviour()
    {
        waitForBattleCryAnimation = 1.5f;
        waitForGroundAttackAnimation = 2;
        groundAttackAnimationName = "groundAttack";
        knockupPower = 0;

        if (BattleCryCoolDownTimer <= 0 && !groundAttacking && !canAttack)
        {
            StartCoroutine(BattleCry());
        }
        else if (groundAttackTimeTimer <= 0 && !isCharging && !isCrying && chargeAttackTimer <= 0 && distanceToPlayer < 10)
        {
            StartCoroutine(groundAttack());
        }

    }

    private void SecondBehaviour()
    {
        waitForBattleCryAnimation = 1;
        chargeDamages = 25;
        BattleCryCoolDown = 5; 
        Debug.Log(BattleCryCoolDownTimer);
        if (BattleCryCoolDownTimer <= 0 && !canAttack)
        {
            
            StartCoroutine(BattleCry());
        }
        else if (speedCryCoolDownTimer <= 0 && !isCharging && chargeAttackTimer < 0 && WaveManager.activeEnemyCount > 0)
        {
            StartCoroutine(SpeedUpgradeCry());
        }
    }

    private void ThirdBehaviour()
    {
        waitForBattleCryAnimation = 0.5f;
        chargeDamages = 30;
        groundAttackDamage = 50;
        waitForGroundAttackAnimation = 1.5f;
        groundAttackAnimationName = "violentGroundAttack";
        knockupPower = 5;

        if (distanceToPlayer < 10 && !isCharging && chargeAttackTimer < 0 && !groundAttacking)
        {
            StartCoroutine(groundAttack());
        }
        else if (!isCharging && chargeAttackTimer <= 0 && !groundAttacking)
        {
            StartCoroutine(BattleCry());
        }
    }
}
