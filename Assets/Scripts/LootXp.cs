using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootXp : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f) * 100 * Time.deltaTime);
    }


    void OnCollisionEnter(Collision collision) //Permet de savoir si le joueur a ramassé l'xp
    {
        if (collision.collider.tag == "Player") //Détecte une collision avec Player
        {
            GameObject.Find("XpBar").GetComponent<PlayerXp>().WinXp(1);
            Destroy(gameObject);
        }
    }
}
