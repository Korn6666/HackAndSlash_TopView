using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy: MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Skeleton;
    public GameObject Liche;
    private GameObject[] EnemyList;

    [SerializeField] GameObject Boss;

    private Vector3 EnemySpawn;
    void Start()
    {

        EnemyList = new GameObject[] { Skeleton , Skeleton, Liche}; // 2/3 chances d'avoir un squelette et 1/3 Liche.
    }

    private IEnumerator SpawnanEnemy()
    {
        int index = Random.Range(0, EnemyList.Length);
        GameObject EnemyToSpawn = EnemyList[index];
        yield return new WaitForSeconds(0f);
        Instantiate(EnemyToSpawn, EnemySpawn, Quaternion.identity);
    }

    public IEnumerator SpawnTheBoss()
    {
        yield return new WaitForSeconds(0f);
        Instantiate(Boss, EnemySpawn, Quaternion.identity);
    }

    public void StartSpawnEnemy(bool Boss)
    {
        GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag("Spawn");
        int index = Random.Range(0, spawnpoints.Length);
        GameObject currentPoint = spawnpoints[index];
        if (!currentPoint) return;
        EnemySpawn = currentPoint.transform.position;

        if (Boss)
        {
            StartCoroutine(SpawnTheBoss());
        }
        else
        {
            StartCoroutine(SpawnanEnemy());
        }
    }
}
