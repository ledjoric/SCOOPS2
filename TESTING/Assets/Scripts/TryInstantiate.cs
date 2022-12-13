using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TryInstantiate : MonoBehaviour
{
    private GameObject cluesTemplate, articleTemplate, MG_ArticleTemplate, divider, objTxt, g;

    [SerializeField] private GameObject txtNoContent, notif, cNotif;
    [SerializeField] private Animator animator;
    [SerializeField] private GameData gameData;
    private string[] clueSplit, clueSplit_2;
    private int clues;

    private void OnEnable()
    {
        if (gameObject.name == "Articles")
        {
            txtNoContent.SetActive(false);

            //articleTemplate = transform.GetChild(0).gameObject;
            

            if (gameData.articlesList != null && gameData.articlesList.Any())
            {
                GetComponent<VerticalLayoutGroup>().enabled = false;
                GetComponent<VerticalLayoutGroup>().childScaleHeight = false;
                for (int i = 0; i < gameData.articlesList.Count; i++)
                {
                    if (!gameData.viewedArticles.Contains(gameData.articlesList[i]))
                    {
                        articleTemplate = transform.GetChild(0).gameObject;
                    }else
                    {
                        articleTemplate = transform.GetChild(1).gameObject;
                    }

                    g = Instantiate(articleTemplate, transform);
                    g.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = gameData.getArticle(gameData.articlesList[i]).title;
                    g.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Published by " +
                        gameData.getPublisherDetail(gameData.articlesList[i]).name + " on " +
                        gameData.getArticle(gameData.articlesList[i]).date;
                    g.SetActive(true);
                }
                StartCoroutine(articleRefresh());
            }
            else
            {
                txtNoContent.SetActive(true);
                txtNoContent.GetComponent<TextMeshProUGUI>().text = "No available articles right now, come back later!";
            }
        }
        else if (gameObject.name == "Objectives")
        {
            txtNoContent.SetActive(false);
            divider = transform.GetChild(0).gameObject;
            objTxt = transform.GetChild(1).gameObject;

            if (gameData.objectivesList != null && gameData.objectivesList.Any())
            {
                /*
                for (int i = 0; i < gameData.objectivesList.Count; i++)
                {
                    g = Instantiate(objTxt, transform);
                    g.transform.GetComponent<TextMeshProUGUI>().text = gameData.objectivesList[i];
                    g.SetActive(true);
                    g = Instantiate(divider, transform);
                    g.SetActive(true);
                }
                */
                clues = 0;
                cluesCount();

                g = Instantiate(objTxt, transform);
                g.transform.GetComponent<TextMeshProUGUI>().text = gameData.objectivesList[0] + "(" + gameData.articlesList.Count + "/" + gameData.articlesJson.articles.Length + ")";
                g.SetActive(true);
                g = Instantiate(divider, transform);
                g.SetActive(true);

                g = Instantiate(objTxt, transform);
                g.transform.GetComponent<TextMeshProUGUI>().text = gameData.objectivesList[1] + "(" + clues + "/5)";
                g.SetActive(true);
                g = Instantiate(divider, transform);
                g.SetActive(true);

                g = Instantiate(objTxt, transform);
                g.transform.GetComponent<TextMeshProUGUI>().text = gameData.objectivesList[2] + "(" + gameData.minigameProgress + "/6)";
                g.SetActive(true);
                g = Instantiate(divider, transform);
                g.SetActive(true);
            }
            else
            {
                txtNoContent.SetActive(true);
                txtNoContent.GetComponent<TextMeshProUGUI>().text = "No available objectives right now, come back later!";
            }
        }
        else if (gameObject.name == "Clues")
        {
            txtNoContent.SetActive(false);
            cluesTemplate = transform.GetChild(0).gameObject;
            
            if (gameData.cluesList != null && gameData.cluesList.Any())
            {
                for (int i = 0; i < gameData.cluesList.Count; i++)
                {
                    clueSplit = gameData.cluesList[i].Split("#");
                    g = Instantiate(cluesTemplate, transform);
                    if (clueSplit[0] == "clue")
                    {
                        g.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("CluesIcon/CluesLogo");
                    }else if(clueSplit[0] == "tip")
                    {
                        g.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("CluesIcon/TipsLogo");
                    }
                    g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = clueSplit[1];

                    // CHECK IF VIEWED
                    if(gameData.viewedClues.Contains(gameData.cluesList[i]))
                    {
                        g.transform.GetChild(2).gameObject.SetActive(false);
                    }else
                    {
                        g.transform.GetChild(2).gameObject.SetActive(true);
                    }

                    g.SetActive(true);
                }
            }
            else
            {
                txtNoContent.SetActive(true);
                txtNoContent.GetComponent<TextMeshProUGUI>().text = "No available clues right now, come back later!";
            }
            
            // ADD TO VIEWED CLUES
            if (gameData.viewedClues.Count != gameData.cluesList.Count)
            {
                for (int i = 0; i < gameData.cluesList.Count; i++)
                {
                    if (gameData.viewedClues == null && !gameData.viewedClues.Any())
                    {
                        gameData.viewedClues = new List<string> { gameData.cluesList[i] };
                    }
                    else if (!gameData.viewedClues.Contains(gameData.cluesList[i]))
                    {
                        gameData.viewedClues.Add(gameData.cluesList[i]);
                    }
                }
            }

            if (gameData.cluesList.Count == gameData.viewedClues.Count)
            {
                cNotif.SetActive(false);
            }

            if ((gameData.cluesList.Count == gameData.viewedClues.Count) && (gameData.articlesList.Count == gameData.viewedArticles.Count) && (gameData.articlesMinigame == gameData.viewedFF))
            {
                animator.SetTrigger("HideNotif");
            }

            StartCoroutine(cluesRefresh());
        }
        else if(gameObject.name == "FireFacts")
        {
            txtNoContent.SetActive(false);
            GetComponent<VerticalLayoutGroup>().enabled = false;
            MG_ArticleTemplate = transform.GetChild(0).gameObject;
            if (gameData.mgArticlesList != null && gameData.mgArticlesList.Any())
            {
                for (int i = 0; i < gameData.mgArticlesList.Count; i++)
                {
                    g = Instantiate(MG_ArticleTemplate, transform);
                    g.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(gameData.getMG_Article(gameData.mgArticlesList[i]).mg_photo);
                    g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = gameData.getMG_Article(gameData.mgArticlesList[i]).title;
                    g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Published by <link=publisher><color=blue><u><b><font=\"Fredoka-Bold SDF\">" +
                        gameData.getMG_Publisher(gameData.mgArticlesList[i]).name + "</font></b></u></color></link> on <font=\"Fredoka-Bold SDF\">" +
                        gameData.getMG_Article(gameData.mgArticlesList[i]).date + "</font>";
                    g.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = gameData.getMG_Article(gameData.mgArticlesList[i]).description;
                    g.SetActive(true);
                }
                StartCoroutine(refresh());
            }
            else
            {
                txtNoContent.SetActive(true);
                txtNoContent.GetComponent<TextMeshProUGUI>().text = "No available articles right now, come back later!";
            }
            
        }
    }

    private void OnDisable()
    {
        foreach (Transform clone in gameObject.transform)
        {
            if (clone.name == "ArticleCard(Clone)" || clone.name == "NewArticleCard(Clone)" || clone.name == "ViewedArticleCard(Clone)" || clone.name == "ObjectiveText(Clone)" || clone.name == "ClueCard(Clone)" || clone.name == "Divider(Clone)")
            {
                Destroy(clone.gameObject);
            }
        }
    }

    public void cluesCount()
    {
        foreach (string clue in gameData.cluesList)
        {
            clueSplit_2 = clue.Split('#');
            if (clueSplit_2[0] == "clue")
            {
                clues++;
            }
        }
    }

    private IEnumerator refresh()
    {
        yield return new WaitForSeconds(0.001f);
        GetComponent<VerticalLayoutGroup>().enabled = true;
    }
    
    private IEnumerator articleRefresh()
    {
        yield return new WaitForSeconds(0.001f);
        GetComponent<VerticalLayoutGroup>().enabled = true;
        yield return new WaitForSeconds(0.001f);
        GetComponent<VerticalLayoutGroup>().childScaleHeight = true;
    }

    private IEnumerator cluesRefresh()
    {
        GetComponent<VerticalLayoutGroup>().enabled = false;
        yield return new WaitForSeconds(0.001f);
        GetComponent<VerticalLayoutGroup>().enabled = true;
    }
}
