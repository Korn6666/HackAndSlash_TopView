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

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        spell1ImageCooldown.fillAmount = 0f;
        spell2ImageCooldown.fillAmount = 0f;
        spell3ImageCooldown.fillAmount = 0f;

        spell1Timer.text = "";
        spell2Timer.text = "";
        spell3Timer.text = "";
        Debug.Log(GameManager.instance.playerSpell1Sprite);
        spell1Image.sprite = GameManager.instance.playerSpell1Sprite; // on recup les sprites des images grace au game manager
        spell2Image.sprite = GameManager.instance.playerSpell2Sprite;
        spell3Image.sprite = GameManager.instance.playerSpell3Sprite;
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
