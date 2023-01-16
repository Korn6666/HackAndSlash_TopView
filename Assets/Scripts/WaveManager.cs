﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float waitnextwave=5f;
    private float waitnextspawn=0.5f;
    private int activeEnemyCount = 0;
    public int currentWave; // Numéro de la vague

    void Start()
    {
        StartCoroutine(WavesRoutine());
    }

    private void Update()
    {
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

        DecrementEnemyCount();
        
    }

    private IEnumerator WavesRoutine()
    {
        int tampo = 1;
        int Wavetampon = 1;
        int currentWaveNbEnemy = 1;
        currentWave = 1;
        while (activeEnemyCount < 500)
        {
            yield return new WaitForSeconds(waitnextwave);
            for (int i=1; i<=currentWaveNbEnemy; i++)
            {
                FindObjectOfType<SpawnEnemy>().StartSpawnEnemy();
                activeEnemyCount += 1;
                yield return new WaitForSeconds(waitnextspawn);
            }

            tampo = currentWaveNbEnemy;
            currentWaveNbEnemy = currentWaveNbEnemy + Wavetampon; 
            Wavetampon = tampo;

            while (activeEnemyCount > 0)
            {
                yield return null;
            }

            currentWave += 1;
        }
    }

}
