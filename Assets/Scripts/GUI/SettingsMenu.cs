using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMPro.TextMeshProUGUI volumeText;
    public TMPro.TextMeshProUGUI musicText;
    public TMPro.TextMeshProUGUI effectText;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        volumeText.text = (((volume + 80)/80)*100).ToString("F0");
    }

    public void SetMusic(float volume)
    {
        audioMixer.SetFloat("music", volume);
        musicText.text = (((volume + 80) / 80) * 100).ToString("F0");
    }

    public void SetEffect(float volume)
    {
        audioMixer.SetFloat("effect", volume);
        effectText.text = (((volume + 80) / 80) * 100).ToString("F0");
    }


    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }


    /* BUGé
    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutionIndex, true);
    }
    */
}
