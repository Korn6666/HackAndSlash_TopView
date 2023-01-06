using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy: MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject EnemyTest;
    private Vector3 EnemySpawn;
    void Start()
    {
        

    }

    private IEnumerator SpawnanEnemy()
    {
        yield return new WaitForSeconds(0f);
        Instantiate(EnemyTest, EnemySpawn, Quaternion.identity);
    }

    public void StartSpawnEnemy()
    {
        GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag("Spawn");
        int index = Random.Range(0, spawnpoints.Length);
        GameObject currentPoint = spawnpoints[index];
        if (!currentPoint) return;
        EnemySpawn = currentPoint.transform.position;
        StartCoroutine(SpawnanEnemy());
    }
}
