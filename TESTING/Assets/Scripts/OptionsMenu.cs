using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider music, sfx;
    [SerializeField] private TextMeshProUGUI low, medium, high;

    private void OnEnable()
    {
        music.value = gameData.music;
        sfx.value = gameData.sfx;

        SetSFXVolume(gameData.sfx);
        SetMusicVolume(gameData.music);

        if(QualitySettings.GetQualityLevel() == 0)
        {
            low.color = new Color32(247, 198, 194, 255);
            medium.color = new Color32(217, 215, 194, 255);
            high.color = new Color32(217, 215, 194, 255);
        }
        else if (QualitySettings.GetQualityLevel() == 1)
        {
            low.color = new Color32(217, 215, 194, 255);
            medium.color = new Color32(247, 198, 194, 255);
            high.color = new Color32(217, 215, 194, 255);
        }
        else if (QualitySettings.GetQualityLevel() == 2)
        {
            low.color = new Color32(217, 215, 194, 255);
            medium.color = new Color32(217, 215, 194, 255);
            high.color = new Color32(247, 198, 194, 255);
        }
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);

        if(qualityIndex == 0)
        {
            low.color = new Color32(247, 198, 194, 255);
            medium.color = new Color32(217, 215, 194, 255);
            high.color = new Color32(217, 215, 194, 255);
        }else if(qualityIndex == 1)
        {
            low.color = new Color32(217, 215, 194, 255);
            medium.color = new Color32(247, 198, 194, 255);
            high.color = new Color32(217, 215, 194, 255);
        }else if(qualityIndex == 2)
        {
            low.color = new Color32(217, 215, 194, 255);
            medium.color = new Color32(217, 215, 194, 255);
            high.color = new Color32(247, 198, 194, 255);
        }
    }
}
