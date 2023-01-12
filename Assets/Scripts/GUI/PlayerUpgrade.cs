using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerUpgrade : MonoBehaviour
{
    private GameObject player;
    private PlayerAttack playerAttack;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private PlayerWarriorAttack playerWarriorAttack;
    private PlayerMageAttack playerMageAttack;

    [SerializeField] private GameObject canvasUpgrade;
    [SerializeField] private GameObject canvasBlackBackground;

    [SerializeField] private GameObject Upgrade1;
    [SerializeField] private GameObject Upgrade2;
    [SerializeField] private GameObject Upgrade3;



    private List<Upgrade> allUpgrades = new List<Upgrade>();
    private List<Upgrade> selectedUpgrades;
    private bool isPaused; //Permet de savoir si le jeu est en pause

    //Ajouter des images ici //////////////////////////////////////
    //List des sprites à utiliser 
    [SerializeField] private Sprite speedUpgradeSprite;
    [SerializeField] private Sprite healthPointUpgradeSprite;

    // Start is called before the first frame update
    void Start()
    {
        canvasUpgrade.SetActive(false);
        canvasBlackBackground.SetActive(false);
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerAttack = player.GetComponent<PlayerAttack>();
        //playerMovement = player.GetComponent<PlayerMovement>();
        //playerHealth = player.GetComponent<PlayerHealth>();

        //Global upgrade
        allUpgrades.Add(new Upgrade("PLayerSpeedUpgrade", "Increase move speed by 20%", PlayerMovement.speed, 1f, 0.20f, speedUpgradeSprite));
        allUpgrades.Add(new Upgrade("PLayerSpell1CoolDownUpgrade", "Reduce skill 1 cooldown", playerAttack.spell1CoolDown, 1f, -0.20f, GameManager.instance.playerSpell1Sprite));
        allUpgrades.Add(new Upgrade("PLayerSpell2CoolDownUpgrade", "Reduce skill 2 cooldown", playerAttack.spell2CoolDown, 1f, -0.20f, GameManager.instance.playerSpell2Sprite));
        allUpgrades.Add(new Upgrade("PLayerSpell3CoolDownUpgrade", "Reduce skill 3 cooldown", playerAttack.spell3CoolDown, 1f, -0.20f, GameManager.instance.playerSpell3Sprite));
        allUpgrades.Add(new Upgrade("PLayerHealthPointUpgrade", "Increase your health by 20% and regen it", PlayerHealth.playerMaxHealth, 1f, 0.20f, healthPointUpgradeSprite));


        //warrior upgrade
        if(GameManager.instance.playerClasse == "warrior")
        {
            playerWarriorAttack = player.GetComponent<PlayerWarriorAttack>();

            allUpgrades.Add(new Upgrade("WarriorSpell1Damage", "Increase basic attack damage by 20%", playerAttack.spell1Damage, 1f, 0.20f, GameManager.instance.playerSpell1Sprite));
            allUpgrades.Add(new Upgrade("WarriorSpell2Damage", "Increase jump attack damage by 30%", playerAttack.spell2Damage, 1f, 0.30f, GameManager.instance.playerSpell2Sprite));
            allUpgrades.Add(new Upgrade("WarriorSpell3Damage", "Increase spin attack damage by 25%", playerAttack.spell3Damage, 1f, 0.25f, GameManager.instance.playerSpell3Sprite));
            allUpgrades.Add(new Upgrade("WarriorSpell2Range", "Increase jump range by 10%", playerWarriorAttack.jumpForwardForce, 1f, 0.10f, GameManager.instance.playerSpell2Sprite));
            allUpgrades.Add(new Upgrade("WarriorSpell3Range", "Increase spin AOE range by 20%", playerWarriorAttack.spell3AttackRange, 1f, 0.20f, GameManager.instance.playerSpell3Sprite));
        }

        //mage upgrade
        if (GameManager.instance.playerClasse == "mage")
        {
            playerMageAttack = player.GetComponent<PlayerMageAttack>();

            allUpgrades.Add(new Upgrade("MageSpell1Damage", "Increase fire ball attack damage by 20%", playerAttack.spell1Damage, 1f, 0.20f, GameManager.instance.playerSpell1Sprite));
            allUpgrades.Add(new Upgrade("MageSpell2Knockback", "Add knockback to the orb", playerMageAttack.spell2Knockback, 1f, 0.20f, GameManager.instance.playerSpell2Sprite));
            allUpgrades.Add(new Upgrade("MageSpell2HitCount", "Increases the number of enemies hit by the orb", playerMageAttack.spell2HitCount, 1f, 2f, GameManager.instance.playerSpell2Sprite));
            allUpgrades.Add(new Upgrade("MageSpell3Scale", "Increase ice wall size by 10%", playerMageAttack.spell3WallScale, 1f, 0.20f, GameManager.instance.playerSpell3Sprite));
            allUpgrades.Add(new Upgrade("MageSpell3Damage", "Add damage to the ice wall", playerMageAttack.spell3Damage, 1f, 0.10f, GameManager.instance.playerSpell3Sprite));
        }
    }

    private class Upgrade
    {
        public string name;
        public string description;
        public int level;
        public float baseValue; //valeur de base de la stats
        public float upgrade; // stats multiplier 
        public float value; // % d'amelioration
        public Sprite image;

       /* public string get_name() { return name; }
        public string get_description() { return description; }
        public int get_level() { return level; }
        public float get_baseValue() { return baseValue; }
        public float get_upgrade() { return upgrade; }
        public float  get_value() { return value; } */
        public Upgrade(string _name, string _description, float _baseValue, float _upgrade, float _value, Sprite _image) //constructeur
        {
            name = _name;
            description = _description;
            level = 1;
            baseValue = _baseValue;
            upgrade = _upgrade;
            value = _value;
            image = _image;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) //test
        {
            /*
            var uniqueRandomList = GetRandomUpgrade(allUpgrades, 3);

            //Debug.Log("All elements => " + string.Join(", ", allUpgrades));
            //Debug.Log("Unique random elements => " + string.Join(", ", uniqueRandomList));
            Debug.Log("*************************************************************");
            foreach (Upgrade item in uniqueRandomList)
            {
                Debug.Log(item.get_name());
            }
            */

            StartCoroutine(LevelUpUpgrade());
            Debug.Log("triger");

        }
    }


    public IEnumerator LevelUpUpgrade()
    {
        Time.timeScale = 0;
        canvasUpgrade.SetActive(true);
        canvasBlackBackground.SetActive(true);

        selectedUpgrades = GetRandomUpgrade(allUpgrades, 3);

        Upgrade1.transform.GetChild(1).GetComponentInChildren<TMPro.TextMeshProUGUI>().text = selectedUpgrades[0].level.ToString();
        Upgrade1.transform.GetChild(3).GetComponentInChildren<TMPro.TextMeshProUGUI>().text = selectedUpgrades[0].description;
        Upgrade1.transform.GetChild(2).GetComponentInChildren<Image>().sprite = selectedUpgrades[0].image;

        Upgrade2.transform.GetChild(1).GetComponentInChildren<TMPro.TextMeshProUGUI>().text = selectedUpgrades[1].level.ToString();
        Upgrade2.transform.GetChild(3).GetComponentInChildren<TMPro.TextMeshProUGUI>().text = selectedUpgrades[1].description;
        Upgrade2.transform.GetChild(2).GetComponentInChildren<Image>().sprite = selectedUpgrades[1].image;

        Upgrade3.transform.GetChild(1).GetComponentInChildren<TMPro.TextMeshProUGUI>().text = selectedUpgrades[2].level.ToString();
        Upgrade3.transform.GetChild(3).GetComponentInChildren<TMPro.TextMeshProUGUI>().text = selectedUpgrades[2].description;
        Upgrade3.transform.GetChild(2).GetComponentInChildren<Image>().sprite = selectedUpgrades[2].image;

        isPaused = true;
        while (isPaused)
        {
            yield return null;
        }

        canvasUpgrade.SetActive(false);
        canvasBlackBackground.SetActive(false);

        Time.timeScale = 1;
        yield return null;
    }

    public void OnClicUpgrade(int numUpgrade)
    {
        var up = selectedUpgrades[numUpgrade];
        switch (up.name)
        {
            case "PLayerSpeedUpgrade":
                up.level += 1;
                up.upgrade += up.value;
                PlayerMovement.speed = up.baseValue * up.upgrade;
                break;

            case "PLayerSpell1CoolDownUpgrade":
                up.level += 1;
                up.upgrade += up.value;
                playerAttack.spell1CoolDown = up.baseValue * up.upgrade;
                break;

            case "PLayerSpell2CoolDownUpgrade":
                up.level += 1;
                up.upgrade += up.value;
                playerAttack.spell2CoolDown = up.baseValue * up.upgrade;
                break;

            case "PLayerSpell3CoolDownUpgrade":
                up.level += 1;
                up.upgrade += up.value;
                playerAttack.spell3CoolDown = up.baseValue * up.upgrade;
                break;

            case "PLayerHealthPointUpgrade":
                up.level += 1;
                up.upgrade += up.value;
                PlayerHealth.playerMaxHealth = up.baseValue * up.upgrade;
                player.GetComponent<PlayerHealth>().SetMaxHealthUpgrade();
                break;

            case "WarriorSpell1Damage":
                up.level += 1;
                up.upgrade += up.value;
                playerAttack.spell1Damage = up.baseValue * up.upgrade;
                break;

            case "WarriorSpell2Damage":
                up.level += 1;
                up.upgrade += up.value;
                playerAttack.spell2Damage = up.baseValue * up.upgrade;
                break;

            case "WarriorSpell3Damage":
                up.level += 1;
                up.upgrade += up.value;
                playerAttack.spell3Damage = up.baseValue * up.upgrade;
                break;

            case "WarriorSpell2Range":
                up.level += 1;
                up.upgrade += up.value;
                playerWarriorAttack.jumpForwardForce = up.baseValue * up.upgrade;
                break;

            case "WarriorSpell3Range":
                up.level += 1;
                up.upgrade += up.value;
                playerWarriorAttack.spell3AttackRange = up.baseValue * up.upgrade;
                break;

            case "MageSpell1Damage":
                up.level += 1;
                up.upgrade += up.value;
                playerAttack.spell1Damage = up.baseValue * up.upgrade;
                break;

            case "MageSpell2Knockback":
                if(up.level == 1)
                {
                    up.baseValue = -20f;
                    up.description = "Increase orb knockback by 20%";
                }
                up.level += 1;
                up.upgrade += up.value;
                playerMageAttack.spell2Knockback = up.baseValue * up.upgrade;
                break;

            case "MageSpell2HitCount":
                up.level += 1;
                playerMageAttack.spell2HitCount += ((int)up.value);
                break;

            case "MageSpell3Scale":
                up.level += 1;
                up.upgrade += up.value;
                playerMageAttack.spell3WallScale = up.baseValue * up.upgrade;
                break;

            case "MageSpell3Damage":
                if (up.level == 1)
                {
                    up.baseValue = 10f;
                    up.description = "Increase ice wall damage by 10%";
                }
                up.level += 1;
                up.upgrade += up.value;
                playerAttack.spell3Damage = up.baseValue * up.upgrade;
                break;

        }
        isPaused = false;
    }



    private List<Upgrade> GetRandomUpgrade(List<Upgrade> inputAllUpgrades, int count)
    {
        List<Upgrade> inputAllUpgradesClone = new List<Upgrade>(inputAllUpgrades);

        for (int i = 0; i < inputAllUpgradesClone.Count - 1; i++)
        {
            Upgrade temp = inputAllUpgradesClone[i];
            int rand = Random.Range(i, inputAllUpgradesClone.Count);
            inputAllUpgradesClone[i] = inputAllUpgradesClone[rand];
            inputAllUpgradesClone[rand] = temp;
        }

        return inputAllUpgradesClone.GetRange(0, count);
    }
}
