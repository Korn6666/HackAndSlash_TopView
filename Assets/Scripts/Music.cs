using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static GameObject music;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        if (music == null)
        {
            music = transform.gameObject;
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }
}
