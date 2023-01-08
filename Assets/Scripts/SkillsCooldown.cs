using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsCooldown : MonoBehaviour
{
    static public SkillsCooldown instance;

    [SerializeField] private Image spell1Image;
    [SerializeField] private Image spell2Image;
    [SerializeField] private Image spell3Image;

    [SerializeField] public Image spell1ImageCooldown;
    [SerializeField] public Image spell2ImageCooldown;
    [SerializeField] public Image spell3ImageCooldown;

    [SerializeField] public TMPro.TextMeshProUGUI spell1Timer;
    [SerializeField] public TMPro.TextMeshProUGUI spell2Timer;
    [SerializeField] public TMPro.TextMeshProUGUI spell3Timer;


    //Warrior
    [SerializeField] private Sprite warriorSpell1Sprite;
    [SerializeField] private Sprite warriorSpell2Sprite;
    [SerializeField] private Sprite warriorSpell3Sprite;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        string playerClasse = PlayerUpgrade.playerClasse; //Bug ici, il faut la mettre dans le player sans doute
        spell1ImageCooldown.fillAmount = 0f;
        spell2ImageCooldown.fillAmount = 0f;
        spell3ImageCooldown.fillAmount = 0f;

        spell1Timer.text = "";
        spell2Timer.text = "";
        spell3Timer.text = "";

        playerClasse = "warrior"; //Pour le test
        if (playerClasse == "warrior")
        {
            spell1Image.sprite = warriorSpell1Sprite;
            spell2Image.sprite = warriorSpell2Sprite;
            spell3Image.sprite = warriorSpell3Sprite;
        }
        
    }


    public static IEnumerator Spell1Cooldown(float cooldown)
    {
        float timer = 0f;
        while(timer < cooldown)
        {
            timer += Time.deltaTime;
            instance.spell1ImageCooldown.fillAmount = 1 - (timer / cooldown);
            float textTimer = cooldown - timer;
            instance.spell1Timer.text = textTimer.ToString("F1"); // On ecrit le temps de CD
            yield return null;
        }
        instance.spell1Timer.text = "";
        yield return null;
    }

    public static IEnumerator Spell2Cooldown(float cooldown)
    {
        float timer = 0f;
        while (timer < cooldown)
        {
            timer += Time.deltaTime;
            instance.spell2ImageCooldown.fillAmount = 1 - (timer / cooldown);
            float textTimer = cooldown - timer;
            instance.spell2Timer.text = textTimer.ToString("F1"); // On ecrit le temps de CD
            yield return null;
        }
        instance.spell2Timer.text = "";
        yield return null;
    }

    public static IEnumerator Spell3Cooldown(float cooldown)
    {
        float timer = 0f;
        while (timer < cooldown)
        {
            timer += Time.deltaTime;
            instance.spell3ImageCooldown.fillAmount = 1 - (timer / cooldown);
            float textTimer = cooldown - timer;
            instance.spell3Timer.text = textTimer.ToString("F1"); // On ecrit le temps de CD
            yield return null;
        }
        instance.spell3Timer.text = "";
        yield return null;
    }
}
