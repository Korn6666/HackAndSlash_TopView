using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarLooking : HealthBar
{
    public Camera main_camera;
    public GameObject enemyPosition;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + main_camera.transform.rotation * Vector3.back, main_camera.transform.rotation * Vector3.down);
        transform.position = enemyPosition.transform.position + Vector3.up + Vector3.forward;
    }
}
