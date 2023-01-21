using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIngeAttack : PlayerAttack
{
    private Animator playerAnimator;

    //SPELL 1
    [SerializeField] private GameObject  electrode;
    private GameObject electrode1;
    private GameObject electrode2;


    //SPELL 2
    [SerializeField] private GameObject orbe;
    public float spell2Range = 1f;
    public float spell2Knockback = 0f;
    public float spell2HitCount = 5f;


    //SPELL 3
    [SerializeField] private GameObject wall;
    public float spell3Range = 3f; // AOE area range
    public float spell3WallScale = 1f;

    [SerializeField] public static float jumpForwardForce = 10;


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerAnimator = gameObject.GetComponent<Animator>();
        playerAnimator.applyRootMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && spell1CoolDownTimer <= 0 && !attacking) //premiere attaque(spell 1), clic gauche
        {
            StartCoroutine(SkillsCooldown.Spell1Cooldown(spell1CoolDown));
            StartCoroutine(Spell1Attack());
        }

        if (Input.GetMouseButtonDown(1) && spell2CoolDownTimer <= 0 && !attacking) //deuxieme attaque(spell 2), clique droit
        {
            StartCoroutine(SkillsCooldown.Spell2Cooldown(spell2CoolDown));
            StartCoroutine(Spell2Attack());
        }

        if (Input.GetKeyDown(KeyCode.Space) && spell3CoolDownTimer <= 0 && !attacking) //deuxieme attaque(spell 3), sur espace
        {
            StartCoroutine(SkillsCooldown.Spell3Cooldown(spell3CoolDown));
            StartCoroutine(Spell3Attack());
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
    }


    IEnumerator Spell1Attack()
    {
        attacking = true; //nous attaquons
        spell1CoolDownTimer = spell1CoolDown; //lancement du cooldown de l'attaque 

        playerAnimator.SetTrigger("Trigger");
        playerAnimator.SetFloat("Trigger Number", 2);
        playerAnimator.SetTrigger("BasicAttack");

        Destroy(electrode2);
        electrode2 = electrode1;
        electrode1  = Instantiate(electrode, transform.position, Quaternion.identity);



        attacking = false;
        yield return null;
    }

    IEnumerator ElectrodeLink()
    {
        //Ray 
        yield return null;
    }

    IEnumerator Spell2Attack()
    {
        attacking = true; //nous attaquons
        spell2CoolDownTimer = spell2CoolDown; //lancement du cooldown de l'attaque 

        playerAnimator.SetTrigger("JumpAttack");

        //detecter les enemy in range
        Collider[] hitEnemies = Physics.OverlapSphere(electrode1.transform.position, 2, enemyLayers);

        //infliger les degats aux ennemies
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(spell1Damage);
            //stun
        }

        attacking = false;
        yield return null;
    }

    IEnumerator Spell3Attack()
    {
        attacking = true; //nous attaquons
        spell3CoolDownTimer = spell3CoolDown; //lancement du cooldown de l'attaque
        playerAnimator.SetTrigger("360Attack");

        Vector3 wallSpawnPosition = playerMovement.GetMousePositionOnPlane();

        GameObject wallObject = Instantiate(wall, wallSpawnPosition, gameObject.transform.rotation);
        wallObject.GetComponent<Wall>().playerMageAttack = gameObject.GetComponent<PlayerMageAttack>();


        yield return new WaitForSeconds(0.6f); //temps d'animation
        attacking = false;
        yield return null;

    }
}
