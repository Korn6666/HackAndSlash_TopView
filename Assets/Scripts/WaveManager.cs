using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float waitnextwave=5f;
    private float waitnextspawn=1f;
    private int activeEnemyCount = 0;
    public GameObject EnnemyTest;
    void Start()
    {
        StartCoroutine(StartWavesRoutine());
    }
    public void DecrementEnemyCount()
    {
        if(activeEnemyCount > 0)
        {
            --activeEnemyCount;
        }
    }
    public void OnDestroy()
    {
        if (tag.Equals("Enemy"))
        {
            DecrementEnemyCount();
        }
    }

    private IEnumerator StartWavesRoutine()
    {
        int tempo = 0;
        int Wavetampon = 0;
        int currentWave = 1;
        while (activeEnemyCount < 500)
        {
            yield return new WaitForSeconds(waitnextwave);
            for (int i=0; i<=currentWave/2-1; i++)
            {
                FindObjectOfType<SpawnEnemy>().StartSpawnEnemy();
                FindObjectOfType<SpawnEnemy>().StartSpawnEnemy();
                yield return new WaitForSeconds(waitnextspawn);
            }
            if (currentWave % 2 != 0)
            {
                FindObjectOfType<SpawnEnemy>().StartSpawnEnemy();
                yield return new WaitForSeconds(waitnextspawn);
            }
            tempo = currentWave;
            currentWave = currentWave + Wavetampon;
            Wavetampon = tempo;
            
        }
    }

}
