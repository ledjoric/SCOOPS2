using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject main, play, options, start, btnBack, title, startDialog, darkPanel, loading, tips;
    [SerializeField] private TMP_InputField playerName;
    [SerializeField] private GameData gameData;

    private string sexuality;

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
        startDialog.SetActive(true);
    }

    public void back()
    {
        if(play.activeInHierarchy)
        {
            main.SetActive(true);
            play.SetActive(false);
        }else if(start.activeInHierarchy)
        {
            play.SetActive(true);
            title.SetActive(true);
            start.SetActive(false);
            btnBack.SetActive(false);
        }else if(options.activeInHierarchy)
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
        if(EventSystem.current.currentSelectedGameObject.name == "Male")
        {
            sexuality = "Male";
        }else if(EventSystem.current.currentSelectedGameObject.name == "Female")
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

        loading.SetActive(true);
        tips.SetActive(true);   
    }

    public void no()
    {
        darkPanel.SetActive(false);
        startDialog.SetActive(false);
    }
}
