using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerXp : MonoBehaviour
{
    public Slider slider; //Permet de modifier la bar d'xp
    [SerializeField] TMPro.TextMeshProUGUI textPlayerLevel;
    [SerializeField] TMPro.TextMeshProUGUI xpText;


    public float currentXp;
    public float levelUpXp;

    public int playerLevel;





    // Start is called before the first frame update
    void Start()
    {
        playerLevel = 0;
        LevelUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            WinXp(1f);
        }
    }

    public void WinXp(float xpToAdd)
    {
        currentXp += xpToAdd;
        SetXp(currentXp);
        xpText.text = "Xp: "+currentXp.ToString()+"/"+levelUpXp.ToString(); // on change l'affichage de l'xp

        if (currentXp >= levelUpXp && playerLevel < 10)
        {
            LevelUp();
        }
    }

    private void LevelUp() //Méthode appelée dès que l'on level up
    {
        playerLevel += 1;
        textPlayerLevel.text = playerLevel.ToString(); // on change l'affichage du level 
        levelUpXp = playerLevel * playerLevel;

        currentXp = 0;
        SetXpToLevelUp(levelUpXp);
        xpText.text = "Xp: " + currentXp.ToString() + "/" + levelUpXp.ToString(); // on change l'affichage de l'xp
    }




    public void SetXpToLevelUp(float maxXp)
    {
        slider.maxValue = maxXp;
        slider.value = 0;
    }
    public void SetXp(float xp)
    {
        slider.value = xp;
    }
}
