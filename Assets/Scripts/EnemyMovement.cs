using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private Rigidbody Rbd;
    public float speed;
    [SerializeField] private float distanceSeuil = 3;
    private Animator Animator;
    public bool canAttack;

    public bool speedUpgrade;
    public float standardSpeed = 5;
    private float speedUpgradeValue;

    public float distanceToPlayer2D;

    private NavMeshAgent EnemyAgent;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Rbd = gameObject.GetComponent<Rigidbody>();
        Animator = gameObject.GetComponent<Animator>();
        speed = standardSpeed;
        speedUpgradeValue = standardSpeed * 1.5f; // Augmentation de 50% grace au crie du boss
        speedUpgrade = false;
        EnemyAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAgent.speed = speed;
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (!player) return;

        Vector3 directionToPlayer = player.transform.position - transform.position;
        Vector3 directionToPlayer2D = new Vector3(directionToPlayer.x, 0, directionToPlayer.z);
        distanceToPlayer2D = directionToPlayer2D.magnitude;

        if (speedUpgrade)
        {
            speed = speedUpgradeValue;
        }else
        {
            speed = standardSpeed;
        }

        if (gameObject.tag == "Liche")
        {
            if (!gameObject.GetComponent<LicheBehaviour>().isSpelling)
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
                Move();
            }else
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }
        }else if (gameObject.tag == "Skeleton")
        {
            Move(); 
        }else if (gameObject.tag == "Boss")
        {
            if (gameObject.GetComponent<BossBehaviour>().isCharging)
            {
                Vector3 target = gameObject.GetComponent<BossBehaviour>().target;
                Charge(target);
            }
            else { Move(); }
        }
        
    }
    void Move()
    {
        Vector3 distance = player.transform.position - transform.position;
        Vector3 distance2D = new Vector3(distance.x, 0, distance.z);
        if (gameObject.GetComponent<NavMeshAgent>().enabled == true)
        {
            EnemyAgent.SetDestination(player.transform.position);

        }
        if (distance2D.magnitude > distanceSeuil + 1)
        {
            if (gameObject.tag == "Skeleton" || gameObject.tag == "Boss")
            {
                Animator.SetBool("ForwardSpeed", true);
                Animator.SetBool("onPlayerContact", false);
            }
            canAttack = false;
        }
        else
        {
            if (gameObject.tag == "Skeleton" || gameObject.tag == "Boss")
            {
                Animator.SetBool("ForwardSpeed", false);
                Animator.SetBool("onPlayerContact", true);
            }
            canAttack = true;
        }
    }

    void Charge(Vector3 target)
    {
        //Animator.ResetTrigger("Attack");
        Vector3 direction = target - transform.position;
        Vector3 direction2D = new Vector3(direction.x, 0, direction.z);

        Vector3 Ndirection2D = direction2D.normalized;

        //Rbd.MovePosition(transform.position + Ndirection2D * Time.deltaTime * speed);

        transform.position = transform.position + Ndirection2D * Time.deltaTime * speed;

        if (direction2D.magnitude > 1)
        {
            Animator.SetBool("isCharging", true);
        }
        else
        {
            Animator.SetBool("isCharging", false);
            gameObject.GetComponent<BossBehaviour>().isCharging = false;
        }


        if (distanceToPlayer2D < 10 && gameObject.GetComponent<BossBehaviour>().isCharging)
        {
            Animator.SetTrigger("Attack");
        }

        transform.LookAt(target);


    }




    void OldMove()
    {
        Vector3 direction = player.transform.position - transform.position;
        Vector3 direction2D = new Vector3(direction.x, 0, direction.z);
        EnemyAgent.SetDestination(player.transform.position);
        if (direction2D.magnitude > distanceSeuil)
        {
            Vector3 Ndirection2D = direction2D.normalized;
            Rbd.MovePosition(transform.position + Ndirection2D * Time.deltaTime * speed);
            //transform.position = transform.position + Ndirection2D * Time.deltaTime* speed;


            if (direction2D.magnitude > distanceSeuil + 1)
            {
                if (gameObject.tag == "Skeleton" || gameObject.tag == "Boss")
                {
                    Animator.SetBool("ForwardSpeed", true);
                    Animator.SetBool("onPlayerContact", false);
                }
                canAttack = false;
            }
        }
        else
        {
            if (gameObject.tag == "Skeleton" || gameObject.tag == "Boss")
            {
                Animator.SetBool("ForwardSpeed", false);
                Animator.SetBool("onPlayerContact", true);
            }
            canAttack = true;
        }
    }

}

