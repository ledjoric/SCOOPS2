using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject articleChoosing, intro;
    [SerializeField] TextMeshProUGUI objectiveTxt;
    [SerializeField] private Transform male, female;
    private Transform player;

    private void Awake()
    {
        if (gameData.isResume)
        {
            //Invoke("loadData", 0.001f);
            loadData();
        }

        string path = Application.persistentDataPath + "/settings.scps";
        if (File.Exists(path))
        {
            loadSettingsData();
        }
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        //allObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        string path = Application.persistentDataPath + "/settings.scps";
        if(File.Exists(path))
        {
            loadSettingsData();
        }
    }

    private void OnDisable()
    {
        saveData();
    }

    private void OnApplicationQuit()
    {
        //saveData();
    }

    public void saveData()
    {
        SaveSystem.savePlayer(gameData);
        SettingsSaveSystem.saveSettings(gameData);
    }

    public void loadData()
    {
        SaveData data = SaveSystem.loadPlayer();

        gameData.playerName = data.playerName;
        gameData.playerSexuality = data.playerSexuality;

        StartCoroutine(setPlayerPosition(data.position[0], data.position[1], data.position[2], data.rotation));

        gameData.mgArticlesList = data.mgArticlesList;
        gameData.articlesMinigame = data.articlesMinigame;

        gameData.mg_ArticleInterval = data.mg_ArticleInterval;

        gameData.idShown = data.idShown;
        gameData.loadedData = data.loadedData;
        gameData.win = data.win;

        gameData.guardNPC = data.guardNPC;
        gameData.customerNPC = data.customerNPC;
        gameData.cashierNPC = data.cashierNPC;
        gameData.baristaNPC = data.baristaNPC;
        gameData.parkNPC = data.parkNPC;
        gameData.vendorNPC = data.vendorNPC;
        gameData.storeOwnerNPC = data.storeOwnerNPC;
        gameData.roadNPC = data.roadNPC;

        gameData.isWatched = data.isWatched;
        gameData.cryptoDone = data.cryptoDone;

        gameData.tutorial = data.tutorial;
        gameData.wtd = data.wtd;
        gameData.basics = data.basics;
        gameData.craap = data.craap;

        gameData.collect = data.collect;
        gameData.explore = data.explore;
        gameData.evaluate = data.evaluate;
        gameData.publish = data.publish;

        gameData.phone = data.phone;
        gameData.npc = data.npc;
        gameData.objects = data.objects;
        gameData.article = data.article;
        gameData.clue = data.clue;

        gameData.currency = data.currency;
        gameData.relevance = data.relevance;
        gameData.authority = data.authority;
        gameData.accuracy = data.accuracy;
        gameData.purpose = data.purpose;

        gameData.articlesList = data.articlesList;
        gameData.cluesList = data.cluesList;

        gameData.minigameProgress = data.minigameProgress;

        gameData.isEvaluating = data.isEvaluating;

        //gameData.currentPoints = data.currentPoints;

        gameData.viewedArticles = data.viewedArticles;
        gameData.viewedClues = data.viewedClues;
        gameData.viewedFF = data.viewedFF;

        if (data.articlesList.Count != 6)
        {
            objectiveTxt.text = "Collect Articles (" + data.articlesList.Count + "/6)";
        }
        else
        {
            objectiveTxt.text = "Check you laptop!";
        }

        if (data.isEvaluating)
        {
            articleChoosing.SetActive(true);
        }
        else
        {
            articleChoosing.SetActive(false);
        }

        if (data.tutorial)
        {
            intro.SetActive(true);
        }
        else
        {
            intro.SetActive(false);
        }
    }

    public void loadSettingsData()
    {
        SettingsSaveData data = SettingsSaveSystem.loadSettings();

        gameData.music = data.music;
        gameData.sfx = data.sfx;
        gameData.quality = data.quality;

        gameData.audioMixer.SetFloat("SFX", data.music);
        gameData.audioMixer.SetFloat("Music", data.sfx);
        QualitySettings.SetQualityLevel(data.quality);
    }

    private IEnumerator setPlayerPosition(float Px, float Py, float Pz, float Ry)
    {
        if (gameData.playerSexuality == "Female")
        {
            player = female;
        }
        else
        {
            player = male;
        }

        //player.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.00001f);

        Vector3 playerPosition;
        playerPosition.x = Px;
        playerPosition.y = Py;
        playerPosition.z = Pz;

        Quaternion target = Quaternion.Euler(0, Ry, 0);

        player.position = playerPosition;
        player.rotation = target;
        player.gameObject.SetActive(true);
    }
}
