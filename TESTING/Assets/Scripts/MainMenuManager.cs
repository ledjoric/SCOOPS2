using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject main, play, options, resume, start, btnBack, title, startDialog, darkPanel, loading, tips, incDialog;
    [SerializeField] private TMP_InputField playerName;
    [SerializeField] private GameData gameData;

    [SerializeField] private AudioMixer audioMixer;

    private string sexuality;

    private void OnEnable()
    {
        Invoke("existingSave", 0.01f);
        Invoke("loadSettings", 0.01f);
    }

    private void OnApplicationQuit()
    {
        SettingsSaveSystem.saveSettings(gameData);
    }

    public void playGame()
    {
        play.SetActive(true);
        main.SetActive(false);
    }

    public void openOptions()
    {
        options.SetActive(true);
        btnBack.SetActive(true);
        main.SetActive(false);
        title.SetActive(false);
    }

    public void newGame()
    {
        start.SetActive(true);
        btnBack.SetActive(true);
        play.SetActive(false);
        title.SetActive(false);
    }

    public void startGame()
    {
        darkPanel.SetActive(true);

        if (playerName.text != "" && sexuality != "")
        {
            startDialog.SetActive(true);
        }
        else
        {
            incDialog.SetActive(true);
        }

    }

    public void back()
    {
        if (play.activeInHierarchy)
        {
            main.SetActive(true);
            play.SetActive(false);
        }
        else if (start.activeInHierarchy)
        {
            play.SetActive(true);
            title.SetActive(true);
            start.SetActive(false);
            btnBack.SetActive(false);
        }
        else if (options.activeInHierarchy)
        {
            main.SetActive(true);
            title.SetActive(true);
            options.SetActive(false);
            btnBack.SetActive(false);
        }
    }

    public void exit()
    {
        Application.Quit();
    }

    public void chooseSexuality()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Male")
        {
            sexuality = "Male";
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Female")
        {
            sexuality = "Female";
        }
    }

    // ILIPAT SA IBANG SCRIPT SANA
    public void yes()
    {
        // get player name
        gameData.playerName = playerName.text;
        gameData.playerSexuality = sexuality;
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "Music", 1f, -80f));

        loading.SetActive(true);
        tips.SetActive(true);
    }

    public void no()
    {
        darkPanel.SetActive(false);
        startDialog.SetActive(false);
    }

    private void existingSave()
    {
        string path = Application.persistentDataPath + "/player.scps";
        if (File.Exists(path))
        {
            resume.GetComponent<Button>().interactable = true;
        }
        else
        {
            resume.GetComponent<Button>().interactable = false;
        }
    }

    private void loadSettings()
    {
        SettingsSaveData data = SettingsSaveSystem.loadSettings();

        gameData.music = data.music;
        gameData.sfx = data.sfx;
        gameData.quality = data.quality;

        gameData.audioMixer.SetFloat("SFX", data.music);
        gameData.audioMixer.SetFloat("Music", data.sfx);
        QualitySettings.SetQualityLevel(data.quality);
    }
}
