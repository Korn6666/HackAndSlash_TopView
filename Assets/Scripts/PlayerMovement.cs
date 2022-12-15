using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 10f;
    private Rigidbody Rbd;
    private float translationForce = 20;

    void Start()
    {
        Rbd = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*float translationX = Input.GetAxis("Horizontal") * speed;
        float translationZ = Input.GetAxis("Vertical") * speed;
        transform.Translate(translationX, 0, translationZ);*/ //   Système de Déplacement sans RigidBody

        //Système de déplacement avec le RigidBody
        float sensX = Input.GetAxis("Horizontal");
        float sensY = Input.GetAxis("Vertical");
        Vector3 directionInput = new Vector3(translationForce * sensX, 0, translationForce * sensY);
        Rbd.MovePosition(transform.position + directionInput * Time.deltaTime * speed);

        // The player look to the mouse position
        /*Vector3 newDirection = Vector3.RotateTowards(transform.position, Input.mousePosition, speed * Time.deltaTime, 0.0f);
        Vector3 newDirection2D = new Vector3(newDirection.x, 0, newDirection.z);
        Rbd.MoveRotation(Quaternion.LookRotation(newDirection2D));*/


        /*Vector3 direction = Input.mousePosition - transform.position;
        Vector3 direction2D = new Vector3(direction.x, 0, direction.z);
        transform.forward = direction2D;*/

        /*Vector3 direction = Input.mousePosition - transform.position;
        transform.LookAt(new Vector3(Input.mousePosition.x, 0, Input.mousePosition.z));*/
    }
}
