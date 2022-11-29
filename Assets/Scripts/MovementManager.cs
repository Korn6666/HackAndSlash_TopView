using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 10.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Horizontal") * speed;
        transform.Translate(translation,0,0);

    }
}
