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
        SetVolumeInit(GameManager.volumeValue);
        SetMusicInit(GameManager.musicValue);
        SetEffectInit(GameManager.effectValue);
        SetQualityIneit(GameManager.qualityIndex);
    }

    public void SetVolume(float volume)
    {
        GameManager.volumeValue = volume;
        audioMixer.SetFloat("volume", volume);
        volumeText.text = (((volume + 80)/80)*100).ToString("F0");
    }

    public void SetMusic(float volume)
    {
        GameManager.musicValue = volume;
        audioMixer.SetFloat("music", volume);
        musicText.text = (((volume + 80) / 80) * 100).ToString("F0");
    }

    public void SetEffect(float volume)
    {
        GameManager.effectValue = volume;
        audioMixer.SetFloat("effect", volume);
        effectText.text = (((volume + 80) / 80) * 100).ToString("F0");
    }


    public void SetQuality(int qualityIndex)
    {
        GameManager.qualityIndex = qualityIndex;
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
