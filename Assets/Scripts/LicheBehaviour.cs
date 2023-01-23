using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LicheBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform spellPoint;
    public bool isSpelling;
    public bool presence;
    [SerializeField] float spellRange = 10;
    [SerializeField] float spellDamage = 1;
    [SerializeField] float spellHeal = 1;
    [SerializeField] GameObject spellZone;

    //[SerializeField] private LayerMask Skeleton;
    [SerializeField] private float CoolDown = 10;
    private float CoolDownTimer;

    [SerializeField] private float spellTime = 5;
    private float spellTimeTimer;
    [SerializeField] private float TimeBetweenAttackOrHeal = 1;

    private Animator Animator;
    public float waitForAnimation = 1.2f;
    [SerializeField] private float  waitForAnimationTimer;

    [SerializeField] private AudioSource Incantation;



    
    void Start()
    {
        Animator = gameObject.GetComponent<Animator>();
        spellTimeTimer = spellTime;
        isSpelling = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool canAttack = gameObject.GetComponent<EnemyMovement>().canAttack;

        // Pour vérifier si il doit lancer le sort
        Collider[] hitEntities = Physics.OverlapSphere(spellPoint.position, spellRange);
        foreach (Collider entity in hitEntities)
        {
            if ( entity.tag == "Player" || entity.tag == "Skeleton" || entity.tag == "OtherEnemy")
            {
                if (CoolDownTimer <= 0 && !canAttack)
                {
                    StartCoroutine(Spell());
                }
            }
        }

        // CoolDown & waitForAnimation
        if (CoolDownTimer > 0)
        {
            CoolDownTimer -= Time.deltaTime;
        }
        else if (waitForAnimation > 0)
        {
           //s waitForAnimationTimer -= Time.deltaTime;
        }

        //Casting
        if (isSpelling)
        {
            if (spellTimeTimer > 0)
            {
                spellTimeTimer -= Time.deltaTime;
            }else 
            { 
                isSpelling = false; 
            }
        }

        //Animation
        Animator.SetBool("isSpelling", isSpelling);
    }

    private IEnumerator Spell()
    {
        isSpelling = true;
        spellTimeTimer = spellTime + waitForAnimation;
        
        CoolDownTimer = CoolDown;
        yield return new WaitForSeconds(waitForAnimation);

        spellZone.SetActive(true);
        Incantation.Play();
        float Timer = 1;
        while (isSpelling)
        {
            if (Timer >= TimeBetweenAttackOrHeal)
            {
                Collider[] hitEntities = Physics.OverlapSphere(spellPoint.position, spellRange);
                presence = false;
                foreach (Collider entity in hitEntities) //infliger les degats aux ennemies
                {                   
                    if (entity.tag == "Player")  
                    {
                        entity.gameObject.GetComponent<PlayerHealth>().hitByLich = true;
                        entity.GetComponent<PlayerHealth>().TakeDamage(spellDamage);
                        presence = true;
                    }else if (entity.tag == "Skeleton" || entity.tag == "TheOtherEnemy")
                    {
                        if (entity.GetComponent<EnemyHealth>().health < entity.GetComponent<EnemyHealth>().maxHealth)
                        {
                            entity.GetComponent<EnemyHealth>().TakeDamage(-spellHeal);
                        }
                        presence = true;
                    }
                }
                Timer = 0;
            }
            if ( !presence )
            {
                isSpelling = false;
            }
            Timer += Time.deltaTime;
            yield return null;
        }
        Incantation.Stop();

        spellZone.SetActive(false);

        yield return null;
    }
}
