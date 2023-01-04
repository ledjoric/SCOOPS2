using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
public class GameData : ScriptableObject
{
    // RESUME
    public bool isResume;

    // PLAYER DATA
    public Vector3 position;
    public float rotation;

    // PLAYER PERSONALIZATION
    public string playerName;
    public string playerSexuality;

    // SETTINGS
    [SerializeField] public AudioMixer audioMixer;
    public float music;
    public float sfx;
    public int quality;

    // JSON READER
    public Articles articlesJson;
    public MG_Articles mgArticlesJson;

    // JSON ASSET
    [SerializeField] private TextAsset articlesData;
    [SerializeField] private TextAsset mgArticlesData;

    // ARTICLE MINIGAME
    public List<int> mgArticlesList;
    public int articlesMinigame;

    // NPC MG_ARTICLE INTERVAL VARIABLE
    public int mg_ArticleInterval;

    // NPC DIALOG
    public bool idShown;
    public bool loadedData;
    public bool win;
    public int targetId_1;
    public int targetId_2;

    // PLAYER MOVEMENT INTERRUPTION
    public bool dialogActive;
    public bool guideActive;
    public bool minigameActive;

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
    public bool isOpen;
    public bool isInside;

    // TUTORIAL
    public bool tutorial = true;
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
    public int storyProgress;
    public int stage;
    public int minigameProgress;

    // TRUST METER VALUES
    public float maxPoints = 100;
    public float currentPoints;
    public float velocity = 0;
    public Gradient gradient;
    public int totalPoints;

    // SELECTED ARTCILES
    public int selectedLimit;
    public List<int> selectedArticles;
    public List<int> selectedArticlesIndex;
    public List<int> stageTwoArticles;
    public bool isEvaluating;

    // PHONE ARTICLE
    public int viewArticleIndex;

    // VIEWED
    public List<int> viewedArticles;
    public List<string> viewedClues;
    public int viewedFF;

    // OBJECT STATUS
    public List<string> allObjectsName;
    public List<bool> activeStatus;

    private void Awake()
    {
        tutorial = true;
    }

    private void OnEnable()
    {
        articlesJson = JsonUtility.FromJson<Articles>(articlesData.text);
        mgArticlesJson = JsonUtility.FromJson<MG_Articles>(mgArticlesData.text);

        stage = 1;
        selectedLimit = 3;

        totalPoints = 0;

        selectedArticles.Clear();
        selectedArticlesIndex.Clear();
        stageTwoArticles.Clear();

        idShown = false;
        loadedData = true;
        dialogActive = false;
        win = false;
        targetId_1 = 0;
        targetId_2 = 0;

        //tutorial = true;
        quality = QualitySettings.GetQualityLevel();

        isEvaluating = false;
    }

    // TRUST METER METHODS
    public void trustChange(int value)
    {
        currentPoints += value;
        limitPoints();
    }

    public void limitPoints()
    {
        if (currentPoints < 0)
        {
            currentPoints = 0;
        }
        else if (currentPoints > 100)
        {
            currentPoints = 100;
        }
    }

    // SET VOLUMES VALUE
    public void setMusic(float volume)
    {
        music = volume;
    }

    public void setSFX(float volume)
    {
        sfx = volume;
    }

    public void setQuality(int qualityIndex)
    {
        quality = qualityIndex;
    }

    // ADDING CLUES, OBJECTIVES, AND clues
    public void addArticles(int article)
    {
        if (articlesList == null)
        {
            articlesList = new List<int> { article };
        }
        else if (!articlesList.Contains(article))
        {
            articlesList.Add(article);
        }
    }

    public void addObjectives(string objective)
    {
        if (objectivesList == null)
        {
            objectivesList = new List<string> { objective };
        }
        else if (!objectivesList.Contains(objective))
        {
            objectivesList.Add(objective);
        }
    }

    public void addClues(string clues)
    {
        if (cluesList == null)
        {
            cluesList = new List<string> { clues };
        }
        else if (!cluesList.Contains(clues))
        {
            cluesList.Add(clues);
        }
    }

    // ADD MINIGAME ARTICLE
    public void addMG_Article(int mg_article)
    {
        if (mgArticlesList == null)
        {
            mgArticlesList = new List<int> { mg_article };
        }
        else if (!mgArticlesList.Contains(mg_article))
        {
            mgArticlesList.Add(mg_article);
        }
    }

    // ARTICLE JSON GETTERS
    public Article getArticle(int articleIndex)
    {
        return articlesJson.articles[articleIndex];
    }

    public PublisherDetail getPublisherDetail(int articleIndex)
    {
        return articlesJson.articles[articleIndex].publisher[0];
    }

    // MINIGAME ARTICLES JSON GETTERS
    public MG_Article getMG_Article(int mgArticleIndex)
    {
        return mgArticlesJson.mg_articles[mgArticleIndex];
    }

    public MG_Publisher getMG_Publisher(int mgArticleIndex)
    {
        return mgArticlesJson.mg_articles[mgArticleIndex].mg_publisher[0];
    }
}
