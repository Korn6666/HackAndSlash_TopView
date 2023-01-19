using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMageAttack : PlayerAttack
{
    private Animator playerAnimator;

    //SPELL 1
    [SerializeField] private GameObject  fireBall;
    public float spell1Range = 5f;


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

        yield return new WaitForSeconds(0.6f); //temps d'animation
        GameObject fireBallObject = Instantiate(fireBall, transform.position + Vector3.up, Quaternion.identity);
        fireBallObject.GetComponent<FireBall>().playerMageAttack = gameObject.GetComponent<PlayerMageAttack>();

        attacking = false;
        yield return null;
    }

    IEnumerator Spell2Attack()
    {
        attacking = true; //nous attaquons
        spell2CoolDownTimer = spell2CoolDown; //lancement du cooldown de l'attaque 

        playerAnimator.SetTrigger("JumpAttack");
        // ajouter le cast time
        yield return new WaitForSeconds(0.6f); //temps d'animation

        GameObject orbeObject = Instantiate(orbe, transform.position + new Vector3(0f, 0f, 4f) + Vector3.up, Quaternion.identity);
        orbeObject.GetComponent<Orbe>().playerMageAttack = gameObject.GetComponent<PlayerMageAttack>();

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
