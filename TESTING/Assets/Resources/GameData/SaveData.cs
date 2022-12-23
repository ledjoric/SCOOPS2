using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // PLAYER DATA
    public float[] position;
    public float rotation;

    // PLAYER PERSONALIZATION
    public string playerName;
    public string playerSexuality;

    // SETTINGS
    public float music;
    public float sfx;

    // ARTICLE MINIGAME
    public List<int> mgArticlesList;
    public int articlesMinigame;

    // NPC MG_ARTICLE INTERVAL VARIABLE
    public int mg_ArticleInterval;

    // NPC DIALOG
    public bool idShown;
    public bool loadedData;
    public bool win;
    //public int targetId_1;
    //public int targetId_2;

    // PLAYER MOVEMENT INTERRUPTION
    //public bool dialogActive;
    //public bool guideActive;
    //public bool minigameActive;

    // INTERACTED NPC VARIABLES
    public bool guardNPC;
    public bool customerNPC;
    public bool cashierNPC;
    public bool baristaNPC;
    public bool parkNPC;
    public bool vendorNPC;
    public bool storeOwnerNPC;
    public bool roadNPC;

    // OBJECT BOOLEANS
    public bool isWatched;
    public bool cryptoDone;
    //public bool isOpen;
    //public bool isInside;

    // TUTORIAL
    public bool tutorial;
    public int wtd;
    public int basics;
    public int craap;

    public bool collect;
    public bool explore;
    public bool evaluate;
    public bool publish;

    public bool phone;
    public bool npc;
    public bool objects;
    public bool article;
    public bool clue;

    public bool currency;
    public bool relevance;
    public bool authority;
    public bool accuracy;
    public bool purpose;

    // LIST OF CLUES, OBJECTIVES, AND ARTICLES
    public List<int> articlesList;
    public List<string> objectivesList;
    public List<string> cluesList;

    // PROGRESS VARIABLES
    //public int storyProgress;
    public int stage;
    public int minigameProgress;

    // TRUST METER VALUES
    //public float maxPoints = 100;
    public float currentPoints;
    //public float velocity = 0;
    //public int totalPoints;

    // SELECTED ARTCILES
    public int selectedLimit;
    public List<int> selectedArticles;
    public List<int> selectedArticlesIndex;
    public List<int> stageTwoArticles;

    // PHONE ARTICLE
    public int viewArticleIndex;

    // VIEWED
    public List<int> viewedArticles;
    public List<string> viewedClues;
    public int viewedFF;

    public SaveData(GameData gameData)
    {
        position = new float[3];
        position[0] = gameData.position.x;
        position[1] = gameData.position.y;
        position[2] = gameData.position.z;

        rotation = gameData.rotation;

        playerName = gameData.playerName;
        playerSexuality = gameData.playerSexuality;

        music = gameData.music;
        sfx = gameData.sfx;

        mgArticlesList = gameData.mgArticlesList;
        articlesMinigame = gameData.articlesMinigame;

        mg_ArticleInterval = gameData.mg_ArticleInterval;

        idShown = gameData.idShown;
        loadedData = gameData.loadedData;
        win = gameData.win;

        guardNPC = gameData.guardNPC;
        customerNPC = gameData.customerNPC;
        cashierNPC = gameData.cashierNPC;
        baristaNPC = gameData.baristaNPC;
        parkNPC = gameData.parkNPC;
        vendorNPC = gameData.vendorNPC;
        storeOwnerNPC = gameData.storeOwnerNPC;
        roadNPC = gameData.roadNPC;

        isWatched = gameData.isWatched;
        cryptoDone = gameData.cryptoDone;

        tutorial = gameData.tutorial;
        wtd = gameData.wtd;
        basics = gameData.basics;
        craap = gameData.craap;

        collect = gameData.collect;
        explore = gameData.explore;
        evaluate = gameData.evaluate;
        publish = gameData.publish;

        phone = gameData.phone;
        npc = gameData.npc;
        objects = gameData.objects;
        article = gameData.article;
        clue = gameData.clue;

        currency = gameData.currency;
        relevance = gameData.relevance;
        authority = gameData.authority;
        accuracy = gameData.accuracy;
        purpose = gameData.purpose;

        articlesList = gameData.articlesList;
        cluesList = gameData.cluesList;

        stage = gameData.stage;
        minigameProgress = gameData.minigameProgress;

        currentPoints = gameData.currentPoints;
        
        viewedArticles = gameData.viewedArticles;
        viewedClues = gameData.viewedClues;
        viewedFF = gameData.viewedFF;
    }
}
