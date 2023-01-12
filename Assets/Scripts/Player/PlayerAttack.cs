using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public LayerMask enemyLayers = 9;
    public bool attacking = false;


    //SPELL 1
    [SerializeField] public float spell1Damage; //Damages

    //Les conditions d'attaque : l'input pour attaquer, cooldown de l'attaque, vérifier que le personnage est en position pour attaquer (vérifier qu'un autre attaque n'est pas lancé)
    [SerializeField] public float spell1CoolDown; //cooldown de l'attaque 1 (spell 1) 
    public float spell1CoolDownTimer = 0f;




    //SPELL 2
    // public Transform spell2AttackPoint; // on utilisera ici le spell3Attackpoint
    [SerializeField] public float spell2Damage; //Damages

    //Les conditions d'attaque : l'input pour attaquer, cooldown de l'attaque, vérifier que le personnage est en position pour attaquer (vérifier qu'un autre attaque n'est pas lancé) 
    [SerializeField] public float spell2CoolDown; //cooldown de l'attaque 2 (spell 2) 
    public float spell2CoolDownTimer = 0f;




    //SPELL 3
    [SerializeField] public float spell3Damage; //Damages

    //Les conditions d'attaque : l'input pour attaquer, cooldown de l'attaque, vérifier que le personnage est en position pour attaquer (vérifier qu'un autre attaque n'est pas lancé)
    [SerializeField] public float spell3CoolDown; //cooldown du spell 3 
    public float spell3CoolDownTimer = 0f;
}
