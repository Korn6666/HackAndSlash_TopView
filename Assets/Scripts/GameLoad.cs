using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoad : MonoBehaviour
{
    [SerializeField] private GameObject warrior;
    [SerializeField] private GameObject mage;
    //[SerializeField] private GameObject ?;  AUSI ajouter le IF !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    private Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = new Vector3(0f, 0.5f, 0f);
        GameManager.instance.GetSprite();
        if (GameManager.instance.playerClasse == "warrior")
        {
            Instantiate(warrior, spawnPosition, Quaternion.identity);
        }
        else if(GameManager.instance.playerClasse == "mage")
        {
            Instantiate(mage, spawnPosition, Quaternion.identity);
        }
    }
}
