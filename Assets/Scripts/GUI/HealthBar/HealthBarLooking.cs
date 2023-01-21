using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarLooking : HealthBar
{
    private GameObject main_camera;
    public GameObject enemyPosition;

    private void Start()
    {
        main_camera = GameObject.Find("Main Camera").gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + main_camera.transform.rotation * Vector3.back, main_camera.transform.rotation * Vector3.down);
        transform.position = enemyPosition.transform.position + Vector3.up + Vector3.forward;
    }
}
