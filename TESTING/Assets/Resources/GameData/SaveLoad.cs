using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private Transform male, female;
    private Transform player;

    //public GameObject[] allObjects;
    //public bool[] activeStatus;

    private void OnEnable()
    {
        if(gameData.isResume)
        {
            Invoke("loadData", 0.001f);
            //loadData();
        }
    }

    private void Start()
    {
        /*
        allObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        activeStatus = new bool[allObjects.Length];
        for(int i = 0; i < allObjects.Length; i++)
        {
            activeStatus[i] = allObjects[i].activeInHierarchy;
        }
        */
    }

    private void OnApplicationQuit()
    {
        saveData();
    }

    public void saveData()
    {
        SaveSystem.savePlayer(gameData);
    }

    public void loadData()
    {
        SaveData data = SaveSystem.loadPlayer();

        gameData.playerName = data.playerName;
        gameData.playerSexuality = data.playerSexuality;

        StartCoroutine(setPlayerPosition(data.position[0], data.position[1], data.position[2], data.rotation));

        //gameData.music = data.music;
        //gameData.sfx = data.sfx;
        //gameData.audioMixer.SetFloat("SFX", data.music);
        //gameData.audioMixer.SetFloat("Music", data.sfx);

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

        gameData.currentPoints = data.currentPoints;

        gameData.viewedArticles = data.viewedArticles;
        gameData.viewedClues = data.viewedClues;
        gameData.viewedFF = data.viewedFF;
    }

    private IEnumerator setPlayerPosition(float Px, float Py, float Pz, float Ry)
    {
        if(gameData.playerSexuality == "Female")
        {
            player = female;
        }else
        {
            player = male;
        }

        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.001f);

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
