using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public Rigidbody RB;
    [SerializeField] public PlayerMageAttack playerMageAttack;

    public float speed;

    public LayerMask enemyLayers;
    public LayerMask environmentLayers;
}
