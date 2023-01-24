using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFosse : MonoBehaviour
{

    public float scrollSpeed = 0.5F;
    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }

    private void OnTriggerEnter(Collider entity)
    {
        if (entity != null)
        {
            if (entity.gameObject.layer == 9)
            {
                entity.gameObject.GetComponent<EnemyHealth>().TakeDamage(1000);
            } else if (entity.gameObject.tag == "Player")
            {
                entity.gameObject.GetComponent<PlayerHealth>().TakeDamage(1000);
            }
        }
    }
}