using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoad : MonoBehaviour
{
    [SerializeField] private GameObject warrior;
    [SerializeField] private GameObject mage;
    [SerializeField] private GameObject inge;

    private Vector3 spawnPosition;
    [SerializeField] private GameObject spawnObject;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = spawnObject.transform.position;
        GameManager.instance.GetSprite();
        if (GameManager.instance.playerClasse == "warrior")
        {
            Instantiate(warrior, spawnPosition, Quaternion.identity);
        }
        else if(GameManager.instance.playerClasse == "mage")
        {
            Instantiate(mage, spawnPosition, Quaternion.identity);
        }
        else if (GameManager.instance.playerClasse == "inge")
        {
            Instantiate(inge, spawnPosition, Quaternion.identity);
        }
    }
}
