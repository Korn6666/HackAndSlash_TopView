using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalling : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    [SerializeField] private float FallingSeuil;
    [SerializeField] private bool Falling = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if(player != null)
        {
            if (player.transform.position.y < FallingSeuil)
            {
                Falling = true;
            }
            else
            {
                Falling = false;
            }

            if (Falling)
            {
                ThePlayerFalls();
            }
        }


    }

    private void ThePlayerFalls()
    {
        Debug.Log("Le player tombe");
        player.GetComponent<PlayerHealth>().TakeDamage(1000);
    }
}

