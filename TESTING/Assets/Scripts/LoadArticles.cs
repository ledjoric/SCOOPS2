using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using System;

public class LoadArticles : MonoBehaviour
{
    // PERSONAL ARTICLES JSON
    private P_Articles P_ArticlesJson;
    [SerializeField] private TextAsset P_ArticlesData;

    [SerializeField] private GameObject btnConfirm, confirmDialog, darkPanel, articleChoose, articleEval, publisherDetail;
    [SerializeField] private Transform publisherPanel, articlePanel, p_ArticlePanel;

    private GameObject articleTemplate, g, checkIcon;
    private Image btnSelect;

    // RANDOMIZER
    private int randomNumber, index, finalArticleIndex;
    private List<int> RndmList = new List<int>();

    [SerializeField] private GameData gameData;

    private string articleTitle;
    private int articleIndex;

    private void Start()
    {
        ///gameData.stage = 1;
        //gameData.selectedLimit = 3;
        //gameData.articlesJson = JsonUtility.FromJson<Articles>(articlesData.text);
        //gameData.selectedArticles.Clear();
        //gameData.selectedArticlesIndex.Clear();
        //gameData.stageTwoArticles.Clear();
    }

    private void Awake()
    {
        P_ArticlesJson = JsonUtility.FromJson<P_Articles>(P_ArticlesData.text);
    }

    private void OnEnable()
    {
        //gameData.articlesJson = JsonUtility.FromJson<Articles>(articlesData.text);
        if (gameData.stage == 1)
        {
            loadStageOne();
            //reachedLimit();
        }else if(gameData.stage == 2)
        {
            //gameData.selectedArticlesIndex.Clear();
            loadStageTwo();
        }else if(gameData.stage == 3)
        {
            loadStageThree();
        }
    }

    public void selectArticle()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        articleTitle = EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text;
        articleIndex = EventSystem.current.currentSelectedGameObject.transform.parent.GetSiblingIndex();
        btnSelect = EventSystem.current.currentSelectedGameObject.transform.GetComponent<Image>();
        checkIcon = EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(5).gameObject;


        if (gameData.stage == 1)
        {
            stageOneSelect();
        }
        else if (gameData.stage == 2)
        {
            stageTwoSelect();
        }


        reachedLimit();
    }

    public void selectArticle_2()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        articleTitle = EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text;
        articleIndex = EventSystem.current.currentSelectedGameObject.transform.parent.GetSiblingIndex();
        btnSelect = EventSystem.current.currentSelectedGameObject.transform.GetComponent<Image>();
        checkIcon = EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(5).gameObject;

        if (!gameData.cluesList.Contains(gameData.getPublisherDetail(gameData.selectedArticles[articleIndex]-1).clue) && btnSelect.sprite != Resources.Load<Sprite>("CRA/Deselectbutton"))
        {
            publisherDetail.SetActive(true);
            publisherDetail.transform.GetChild(0).gameObject.SetActive(false);
            publisherDetail.transform.GetChild(1).gameObject.SetActive(true);
            publisherDetail.transform.GetChild(2).gameObject.SetActive(false);
        }else
        {
            proceedSelection();
        }
    }

    public void proceedSelection()
    {
        

        publisherDetail.SetActive(false);

        if (gameData.stage == 1)
        {
            stageOneSelect();
        }
        else if (gameData.stage == 2)
        {
            stageTwoSelect();
        }

        reachedLimit();
    }

    private void reachedLimit()
    {
        // WHEN SELECTED ALL NEEDED ARTICLES
        if(gameData.stage == 1)
        {
            if (gameData.selectedArticles.Count == gameData.selectedLimit)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (!gameData.selectedArticlesIndex.Contains(i))
                    {
                        transform.GetChild(i).GetChild(4).gameObject.SetActive(true);
                    }
                }
                btnConfirm.SetActive(true);
            }
            else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (!gameData.selectedArticlesIndex.Contains(i))
                    {
                        transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
                    }
                }
                btnConfirm.SetActive(false);
            }
        }
        else if(gameData.stage == 2)
        {
            if (gameData.stageTwoArticles.Count == gameData.selectedLimit)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (!gameData.selectedArticlesIndex.Contains(i))
                    {
                        transform.GetChild(i).GetChild(4).gameObject.SetActive(true);
                    }
                }
                btnConfirm.SetActive(true);
            }
            else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (!gameData.selectedArticlesIndex.Contains(i))
                    {
                        transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
                    }
                }
                btnConfirm.SetActive(false);
            }
        }
    }

    public void loadStageOne()
    {
        // RELEVANCE ARTICLE / STAGE 1
        articleTemplate = transform.parent.GetChild(1).gameObject;
        RndmList = new List<int>(new int[gameData.articlesJson.articles.Length]);

        for (int i = 0; i < gameData.articlesJson.articles.Length; i++)
        {
            randomNumber = UnityEngine.Random.Range(0, (gameData.articlesJson.articles.Length) + 1);
            while (RndmList.Contains(randomNumber))
            {
                randomNumber = UnityEngine.Random.Range(0, (gameData.articlesJson.articles.Length) + 1);
            }
            RndmList[i] = randomNumber;
            index = randomNumber - 1;
            //Debug.Log(index);

            g = Instantiate(articleTemplate, transform);
            g.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = gameData.getArticle(index).title;
            g.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(gameData.getArticle(index).photo);
            g.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = gameData.getArticle(index).body;
            g.SetActive(true);
        }

        StartCoroutine(refresh());
    }

    public void loadStageTwo()
    {
        // CURRENCY AND AUTHORITY / STAGE 2
        articleTemplate = transform.parent.GetChild(0).gameObject;
        RndmList = new List<int>(new int[gameData.selectedArticles.Count]);

        for (int i = 0; i < gameData.selectedArticles.Count; i++)
        {
            index = gameData.selectedArticles[i]-1;
            g = Instantiate(articleTemplate, transform);
            g.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = gameData.getArticle(index).title;
            g.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Published by <link=publisher><color=blue><u><b><font=\"Fredoka-Bold SDF\">" + 
                gameData.getPublisherDetail(index).name + "</font></b></u></color></link> on <font=\"Fredoka-Bold SDF\">" + gameData.getArticle(index).date + "</font>";
            g.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(gameData.getArticle(index).photo);
            g.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = gameData.getArticle(index).body;
            g.SetActive(true);
        }

        StartCoroutine(refresh());
    }

    public void loadStageThree()
    {
        // EVALUATION PART / PUBLISHING
        if(transform.parent.parent.name == "Evaluation" && gameData.stage == 3)
        {
            finalArticleIndex = gameData.stageTwoArticles[0]-1;

            // BASED PUBLISHER PANEL
            publisherPanel.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(gameData.getPublisherDetail(finalArticleIndex).pub_photo);
            publisherPanel.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = gameData.getPublisherDetail(finalArticleIndex).name;
            publisherPanel.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = gameData.getPublisherDetail(finalArticleIndex).type;
            publisherPanel.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().text = gameData.getPublisherDetail(finalArticleIndex).description;

            // BASED ARTICLE PANEL
            articlePanel.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = gameData.getArticle(finalArticleIndex).title;
            articlePanel.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Published by <font=\"Fredoka-Bold SDF\">" + gameData.getPublisherDetail(finalArticleIndex).name +
                "</font> on <font=\"Fredoka-Bold SDF\">" + gameData.getArticle(finalArticleIndex).date + "</font>";
            articlePanel.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = gameData.getArticle(finalArticleIndex).body;
            articlePanel.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(gameData.getArticle(finalArticleIndex).photo);


            // ARTICLE TO PUBLISH
            p_ArticlePanel.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = getP_Article(finalArticleIndex).title;
            p_ArticlePanel.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Published by <font=\"Fredoka-Bold SDF\">" + gameData.playerName + "</font> on <font=\"Fredoka-Bold SDF\">" + 
                DateTime.Now.ToString("Y") + "</font>";
            p_ArticlePanel.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = getP_Article(finalArticleIndex).body;
            p_ArticlePanel.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(getP_Article(finalArticleIndex).photo); 
        }
        StartCoroutine(refreshEval());
    }

    public void stageOneSelect()
    {
        foreach (Article article in gameData.articlesJson.articles)
        {
            if (article.title == articleTitle)
            {
                if (gameData.selectedArticles == null)
                {
                    gameData.selectedArticles = new List<int> { article.id };
                    gameData.selectedArticlesIndex = new List<int> { articleIndex };
                    btnSelect.sprite = Resources.Load<Sprite>("CRA/DeselectButton");
                    gameData.totalPoints += gameData.getArticle(article.id - 1).r_score;
                    checkIcon.SetActive(true);
                }
                else if (!gameData.selectedArticles.Contains(article.id))
                {
                    if (gameData.selectedArticles.Count != gameData.selectedLimit)
                    {
                        gameData.selectedArticles.Add(article.id);
                        gameData.selectedArticlesIndex.Add(articleIndex);
                        btnSelect.sprite = Resources.Load<Sprite>("CRA/DeselectButton");
                        gameData.totalPoints += gameData.getArticle(article.id - 1).r_score;
                        checkIcon.SetActive(true);
                    }
                }
                else if (gameData.selectedArticles.Contains(article.id))
                {
                    gameData.selectedArticles.Remove(article.id);
                    gameData.selectedArticlesIndex.Remove(articleIndex);
                    btnSelect.sprite = Resources.Load<Sprite>("CRA/Selectbutton");
                    gameData.totalPoints -= gameData.getArticle(article.id - 1).r_score;
                    checkIcon.SetActive(false);
                }
            }
        }
    }

    public void stageTwoSelect()
    {
        foreach (Article article in gameData.articlesJson.articles)
        {
            if (article.title == articleTitle)
            {
                if (gameData.stageTwoArticles == null)
                {
                    gameData.stageTwoArticles = new List<int> { article.id };
                    gameData.selectedArticlesIndex = new List<int> { articleIndex };
                    btnSelect.sprite = Resources.Load<Sprite>("CRA/DeselectButton");
                    gameData.totalPoints += gameData.getArticle(article.id - 1).ca_score;
                    checkIcon.SetActive(true);
                }
                else if (!gameData.stageTwoArticles.Contains(article.id))
                {
                    if (gameData.stageTwoArticles.Count != gameData.selectedLimit)
                    {
                        gameData.stageTwoArticles.Add(article.id);
                        gameData.selectedArticlesIndex.Add(articleIndex);
                        btnSelect.sprite = Resources.Load<Sprite>("CRA/DeselectButton");
                        gameData.totalPoints += gameData.getArticle(article.id - 1).ca_score;
                        checkIcon.SetActive(true);
                    }
                }
                else if (gameData.stageTwoArticles.Contains(article.id))
                {
                    gameData.stageTwoArticles.Remove(article.id);
                    gameData.selectedArticlesIndex.Remove(articleIndex);
                    btnSelect.sprite = Resources.Load<Sprite>("CRA/Selectbutton");
                    gameData.totalPoints -= gameData.getArticle(article.id - 1).ca_score;
                    checkIcon.SetActive(false);
                }
            }
        }
    }

    // PERSONAL ARTICLE JSON GETTER
    private P_Article getP_Article(int P_ArticleIndex)
    {
        return P_ArticlesJson.p_articles[P_ArticleIndex];
    }

    public void goBack()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        gameData.stage--;
        articleEval.gameObject.SetActive(false);
        articleChoose.SetActive(true);
        if(transform.parent.parent.name == "ArticleChoosing")
        {
            StartCoroutine(backToS2());
        }
        gameData.selectedArticlesIndex.Clear();
        gameData.stageTwoArticles.Clear();
    }

    public void publishArticle()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        darkPanel.gameObject.SetActive(true);
        confirmDialog.SetActive(true);
        confirmDialog.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Pubilish this article?";
    }

    public void confirmSelection()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        darkPanel.gameObject.SetActive(true);
        confirmDialog.SetActive(true);
    }

    private IEnumerator refresh()
    {
        yield return new WaitForSeconds(0.001f);
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(2).GetChild(0).GetChild(0).GetComponent<VerticalLayoutGroup>().enabled = true;
        }
    }

    private IEnumerator refreshEval()
    {
        p_ArticlePanel.GetComponent<VerticalLayoutGroup>().enabled = false;
        articlePanel.GetComponent<VerticalLayoutGroup>().enabled = false;
        yield return new WaitForSeconds(0.001f);
        articlePanel.GetComponent<VerticalLayoutGroup>().enabled = true;
        p_ArticlePanel.GetComponent<VerticalLayoutGroup>().enabled = true;
    }

    private IEnumerator backToS2()
    {
        yield return new WaitForSeconds(0.001f);
        loadStageTwo();
    }
}
