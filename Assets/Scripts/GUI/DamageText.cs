using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    private GameObject main_camera;

    // Start is called before the first frame update
    void Start()
    {
        main_camera = GameObject.Find("Main Camera").gameObject;
        Destroy(transform.parent.gameObject, 0.7f); // ce detruit apres 0.7 sec
    }

    private void Update()
    {
        transform.LookAt(transform.position + main_camera.transform.rotation * Vector3.forward, main_camera.transform.rotation * Vector3.up);
    }
}
