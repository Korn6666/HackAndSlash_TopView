using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        StartCoroutine(ExplosionLifeTime());
    }


    IEnumerator ExplosionLifeTime() // temps de vie de 1 sec
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<ParticleSystem>().Stop();
        Destroy(gameObject);
    }
}
