using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 10f;
    private Rigidbody Rbd;
    private float translationForce = 20;
    private Camera cam;
    [SerializeField] private Vector3 mouseInWorld;
    private Animator playerAnimator;

    
    Vector3 pos = new Vector3(200, 200, 0);



    void Start()
    {
        Rbd = gameObject.GetComponent<Rigidbody>();
        cam = Camera.main;
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Système de déplacement avec le RigidBody
        float sensX = Input.GetAxis("Horizontal");
        float sensZ = Input.GetAxis("Vertical");
        Vector3 directionInput = new Vector3(translationForce * sensX, 0, translationForce * sensZ);

        // Check si il bouge ou pas pour l'animation de mouvement
        if (sensX != 0 || sensZ != 0)
        {
            Rbd.MovePosition(transform.position + directionInput * Time.deltaTime * speed);
            playerAnimator.SetBool("Moving", true);
        }else { playerAnimator.SetBool("Moving", false); }
         

        //Direction du joueur vers la position de la souris
        Vector3 mouseInFloor = GetMousePositionOnPlane();
        Vector3 directionToLook =  mouseInFloor - transform.position;
        Vector3 direction2DToLook = new Vector3(directionToLook.x, 0, directionToLook.z);
        Quaternion rotation = Quaternion.LookRotation(direction2DToLook);
        transform.rotation = rotation;
    }

     public static Vector3 GetMousePositionOnPlane() 
     {
         RaycastHit  hit;
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if(Physics.Raycast (ray, out hit)) 
         {
             Vector3 hitPoint = hit.point;
             hitPoint.y = 0;
 
             return hitPoint;
 
         }
         return Vector3.zero;
     }
}
