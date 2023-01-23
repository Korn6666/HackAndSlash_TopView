using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMPro.TextMeshProUGUI volumeText;
    public TMPro.TextMeshProUGUI musicText;
    public TMPro.TextMeshProUGUI effectText;

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private TMPro.TMP_Dropdown qualityDropdown;

    private void Start()
    {
        SetVolumeInit(GameManager.instance.volumeValue);
        SetMusicInit(GameManager.instance.musicValue);
        SetEffectInit(GameManager.instance.effectValue);
        SetQualityIneit(GameManager.instance.qualityIndex);
    }

    public void SetVolume(float volume)
    {
        GameManager.instance.volumeValue = volume;
        audioMixer.SetFloat("volume", volume);
        volumeText.text = (((volume + 80)/80)*100).ToString("F0");
    }

    public void SetMusic(float volume)
    {
        GameManager.instance.musicValue = volume;
        audioMixer.SetFloat("music", volume);
        musicText.text = (((volume + 80) / 80) * 100).ToString("F0");
    }

    public void SetEffect(float volume)
    {
        GameManager.instance.effectValue = volume;
        audioMixer.SetFloat("effect", volume);
        effectText.text = (((volume + 80) / 80) * 100).ToString("F0");
    }


    public void SetQuality(int qualityIndex)
    {
        GameManager.instance.qualityIndex = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }


    /* BUGé
    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutionIndex, true);
    }
    */

    public void SetVolumeInit(float volume)
    {
        volumeSlider.value = volume;
        volumeText.text = (((volume + 80) / 80) * 100).ToString("F0");
    }

    public void SetMusicInit(float volume)
    {
        musicSlider.value = volume;
        musicText.text = (((volume + 80) / 80) * 100).ToString("F0");
    }

    public void SetEffectInit(float volume)
    {
        effectSlider.value = volume;
        effectText.text = (((volume + 80) / 80) * 100).ToString("F0");
    }

    public void SetQualityIneit(int qualityIndex)
    {
        qualityDropdown.value = qualityIndex;
    }
}
