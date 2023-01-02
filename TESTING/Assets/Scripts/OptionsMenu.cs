using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider music, sfx;
    private float musicValue, sfxValue;

    private void OnEnable()
    {
        music.value = gameData.music;
        sfx.value = gameData.sfx;

        SetSFXVolume(gameData.sfx);
        SetMusicVolume(gameData.music);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }
}
