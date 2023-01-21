using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarPlayer : HealthBar
{
    [SerializeField] private TMPro.TextMeshProUGUI healthText;
    [SerializeField] private TMPro.TextMeshProUGUI healthPourcentText;

    private void Update()
    {
        SetUI(slider.value);
    }

    private void SetUI(float health)
    {
        healthText.text = health.ToString("F0") + "/" + slider.maxValue.ToString("F0");
        healthPourcentText.text = ((health * 100) / slider.maxValue).ToString("F0") + "%";
    }
}
