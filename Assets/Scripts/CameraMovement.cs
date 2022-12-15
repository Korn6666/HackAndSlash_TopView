using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private Vector3 positionFromPlayer;
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        positionFromPlayer = player.transform.position - transform.position; // Enregistre la position de la caméra comparé à celle de player pour la conserver ensuite. 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        transform.position = player.transform.position - positionFromPlayer; // Position caméra comme Diablo3
    }
}
