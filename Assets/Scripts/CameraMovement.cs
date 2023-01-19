using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float speedZoom = 0.05f;
    private Vector3 zoom = Vector3.zero;
    private Vector3 positionFromPlayer; 


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // L'un ou l'autre:
        positionFromPlayer = new Vector3(-0.5f, -13.7f, 7.2f); // Met directe la bonne position
        //positionFromPlayer = player.transform.position - transform.position; // Enregistre la position de la caméra comparé à celle de player pour la conserver ensuite. 

        transform.position = player.transform.position + positionFromPlayer;
        
    }

    void Update()
    {
        //Vector3 forDebug = player.transform.position - transform.position;
        //Debug.Log(forDebug);

        Vector3 playerPosition = player.transform.position;
        Vector3 zoomDelta = positionFromPlayer * Input.mouseScrollDelta.y * speedZoom;
        zoom += zoomDelta;
        transform.position = player.transform.position - positionFromPlayer + zoom; // Position caméra comme Diablo3
    }
}
