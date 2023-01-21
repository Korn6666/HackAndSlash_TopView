using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalling : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float FallingSeuil;
    public bool Falling = false;

    private void Update()
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

    private void ThePlayerFalls()
    {
        Debug.Log("Le player tombe");
        Destroy(player);
    }
}

