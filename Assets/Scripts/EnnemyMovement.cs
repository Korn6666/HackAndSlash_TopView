using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private Rigidbody Rbd;
    [SerializeField] private float speed = 5;
    [SerializeField] private float distanceSeuil = 3;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Rbd = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (!player) return;
        Vector3 direction = player.transform.position - transform.position;
        Vector3 direction2D = new Vector3(direction.x, 0, direction.z);
        if (direction.magnitude > distanceSeuil)
        {
            Rbd.MovePosition(transform.position + direction2D * Time.deltaTime * speed);
        }
    }
}
