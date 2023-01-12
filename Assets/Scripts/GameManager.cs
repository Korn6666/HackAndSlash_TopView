using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    /*
    //last
    [SerializeField] private Sprite mageSpell1Sprite; //image du skill de la classe
    [SerializeField] private Sprite mageSpell2Sprite; //image du skill de la classe
    [SerializeField] private Sprite mageSpell3Sprite; //image du skill de la classe
    */

    public Sprite playerSpell1Sprite;
    public Sprite playerSpell2Sprite;
    public Sprite playerSpell3Sprite;

    // Start is called before the first frame update
    void Start()
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
    }


}
