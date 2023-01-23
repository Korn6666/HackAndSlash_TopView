using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //mettre dans la scene parrallele de unity au start pour pouvoir l'appeler quand je veux
    public static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public string playerClasse;

    //Warrior
    [SerializeField] private Sprite warriorSpell1Sprite; //image du skill de la classe
    [SerializeField] private Sprite warriorSpell2Sprite; //image du skill de la classe
    [SerializeField] private Sprite warriorSpell3Sprite; //image du skill de la classe

    //Mage
    [SerializeField] private Sprite mageSpell1Sprite; //image du skill de la classe
    [SerializeField] private Sprite mageSpell2Sprite; //image du skill de la classe
    [SerializeField] private Sprite mageSpell3Sprite; //image du skill de la classe
  
    //Inge
    [SerializeField] private Sprite ingeSpell1Sprite; //image du skill de la classe
    [SerializeField] private Sprite ingeSpell2Sprite; //image du skill de la classe
    [SerializeField] private Sprite ingeSpell3Sprite; //image du skill de la classe
    
    // ne pas toucher dans inspector
    public Sprite playerSpell1Sprite;
    public Sprite playerSpell2Sprite;
    public Sprite playerSpell3Sprite;


    // pour les settings
    public float volumeValue;
    public float effectValue;
    public float musicValue;
    public int qualityIndex;

    public void GetSprite() //met à jour les sprites au lancement du jeu
    {
        if (playerClasse == "warrior")
        {
            playerSpell1Sprite = warriorSpell1Sprite;
            playerSpell2Sprite = warriorSpell2Sprite;
            playerSpell3Sprite = warriorSpell3Sprite;
        }

        if (playerClasse == "mage")
        {
            playerSpell1Sprite = mageSpell1Sprite;
            playerSpell2Sprite = mageSpell2Sprite;
            playerSpell3Sprite = mageSpell3Sprite;
        }

        if (playerClasse == "inge")
        {
            playerSpell1Sprite = ingeSpell1Sprite;
            playerSpell2Sprite = ingeSpell2Sprite;
            playerSpell3Sprite = ingeSpell3Sprite;
        }
    }


}
