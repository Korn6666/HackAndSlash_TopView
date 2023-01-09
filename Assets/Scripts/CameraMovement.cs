using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    [SerializeField] private float speedZoom = 0.05f;
    private Vector3 zoom = Vector3.zero;
    [SerializeField] private Vector3 positionFromPlayer; //= new Vector3(3.8f, -11.1f, 5.3f);


    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        positionFromPlayer = player.transform.position - transform.position; // Enregistre la position de la caméra comparé à celle de player pour la conserver ensuite. 
        Debug.Log(positionFromPlayer);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 playerPosition = player.transform.position;
        Vector3 zoomDelta = positionFromPlayer * Input.mouseScrollDelta.y * speedZoom;
        zoom += zoomDelta;
        transform.position = player.transform.position - positionFromPlayer + zoom; // Position caméra comme Diablo3
    }
}
