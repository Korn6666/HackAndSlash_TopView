using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public static float speed = 2f;

    private Rigidbody Rbd;
    private float translationForce = 20;
    private Camera cam;
    [SerializeField] private Vector3 mouseInWorld;
    public LayerMask layerMaskRayCast;

    private Animator playerAnimator;

    
    Vector3 pos = new Vector3(200, 200, 0);



    void Start()
    {
        Rbd = gameObject.GetComponent<Rigidbody>();
        cam = Camera.main;
        playerAnimator = gameObject.GetComponent<Animator>();
        playerAnimator.applyRootMotion = false;
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

        Vector3 NDTL = new Vector3(directionToLook.normalized.x, 0, directionToLook.normalized.z) ; // NDTL = NormalizeDirectionToLook. 
        float Side = sensX * NDTL.z - sensZ * NDTL.x;
        float Forward = sensZ * NDTL.z + sensX * NDTL.x;
        playerAnimator.SetFloat("Forward", 0.2f*Forward);
        playerAnimator.SetFloat("Side", 0.2f*Side);
    }

     public Vector3 GetMousePositionOnPlane() 
     {
         RaycastHit  hit;
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if(Physics.Raycast (ray, out hit, 100,layerMaskRayCast)) 
         {
             Vector3 hitPoint = hit.point;
             hitPoint.y = 0;
             return hitPoint;
 
         }
         return Vector3.zero;
     }
}
