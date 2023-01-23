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
    IEnumerator linkCoroutine;
    [SerializeField] private float waitForSpell1Animation = 0.5f;


    //SPELL 2
    [SerializeField] private AudioSource spell2Audio;
    public float spell2Range = 5f;
    public float spell2StunTime = 1.5f;


    //SPELL 3
    [SerializeField] private AudioSource spell3AudioBuff;
    [SerializeField] private AudioSource spell3AudioDebuff;
    public float spell3MoveSpeed;
    public float spell3BuffDuration;


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerAnimator = gameObject.GetComponent<Animator>();
        playerAnimator.applyRootMotion = false;
        linkCoroutine = ElectrodeLink();
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

        StopCoroutine(linkCoroutine);

        yield return new WaitForSeconds(waitForSpell1Animation);
        Destroy(electrode2);
        electrode2 = electrode1;
        electrode1  = Instantiate(electrode, transform.position + Vector3.up, Quaternion.identity);

        if(electrode2 != null) { StartCoroutine(linkCoroutine); }
        
        attacking = false;
        yield return null;
    }

    IEnumerator ElectrodeLink()

    {
        while (true)
        {
            RaycastHit[] hitEnemy;

            Vector3 linkDirection = electrode2.transform.position - electrode1.transform.position;
            Ray link = new Ray(electrode1.transform.position, linkDirection);
            Debug.DrawRay(electrode1.transform.position, linkDirection);
            hitEnemy = Physics.RaycastAll(link, Mathf.Infinity);


            foreach (RaycastHit hit in hitEnemy)
            {
                if (hit.collider.gameObject.layer  == 9)
                {
                    hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(spell1Damage);
                }
            }

            yield return new WaitForSeconds(0.5f);
            yield return null;
        }
    }

    IEnumerator Spell2Attack()
    {
        attacking = true; //nous attaquons
        spell2CoolDownTimer = spell2CoolDown; //lancement du cooldown de l'attaque 

        playerAnimator.SetTrigger("Freeze");
        spell2Audio.Play();

        if (electrode1 != null)
        {
            //detecter les enemy in range
            Collider[] hitEnemies = Physics.OverlapSphere(electrode1.transform.position, spell2Range, enemyLayers);

            //infliger les degats aux ennemies
            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(spell2Damage);
                enemy.GetComponent<EnemyHealth>().TakeStun(spell2StunTime);
            }
        }

        if (electrode2 != null)
        {
            //detecter les enemy in range
            Collider[] hitEnemies = Physics.OverlapSphere(electrode2.transform.position, spell2Range, enemyLayers);

            //infliger les degats aux ennemies
            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(spell2Damage);
                enemy.GetComponent<EnemyHealth>().TakeStun(spell2StunTime);
            }
        }

        attacking = false;
        yield return null;
    }

    IEnumerator Spell3Attack()
    {
        spell3CoolDownTimer = spell3CoolDown; //lancement du cooldown de l'attaque
        float bonusSpeed = spell3MoveSpeed;
        playerAnimator.speed += 0.5f;
        PlayerMovement.speed += bonusSpeed;

        spell3AudioBuff.Play();

        yield return new WaitForSeconds(spell3BuffDuration); //temps du buff

        playerAnimator.speed -= 0.5f;
        PlayerMovement.speed -= bonusSpeed;

        spell3AudioDebuff.Play();

        yield return null;

    }
}
