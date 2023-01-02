using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;

public class ViewArticle : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject articleView, articleContent, btnBack, titleTxt, aNotif;
    [SerializeField] private Animator animator;


    public void viewArticle()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        articleView.SetActive(true);
        btnBack.SetActive(true);
        titleTxt.SetActive(false);
        transform.parent.gameObject.SetActive(false);

        if (gameData.viewedArticles == null)
        {
            gameData.viewedArticles = new List<int> { gameData.articlesList[transform.GetSiblingIndex() - 2] };
        }
        else if (!gameData.viewedArticles.Contains(gameData.articlesList[transform.GetSiblingIndex() - 2]))
        {
            gameData.viewedArticles.Add(gameData.articlesList[transform.GetSiblingIndex() - 2]);
        }

        if (gameData.articlesList.Count == gameData.viewedArticles.Count)
        {
            aNotif.SetActive(false);
        }

        if ((gameData.cluesList.Count == gameData.viewedClues.Count) && (gameData.articlesList.Count == gameData.viewedArticles.Count) && (gameData.articlesMinigame == gameData.viewedFF))
        {
            animator.SetTrigger("HideNotif");
        }

        gameData.viewArticleIndex = gameData.articlesList[transform.GetSiblingIndex() - 2];

        articleContent.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = gameData.getArticle(gameData.viewArticleIndex).title;
        articleContent.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Published by <link=publisher><color=blue><u><b><font=\"Fredoka-Bold SDF\">" +
            gameData.getPublisherDetail(gameData.viewArticleIndex).name + "</font></b></u></color></link> on <font=\"Fredoka-Bold SDF\">" +
            gameData.getArticle(gameData.viewArticleIndex).date + "</font>";
        articleContent.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = gameData.getArticle(gameData.viewArticleIndex).body;
        articleContent.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(gameData.getArticle(gameData.viewArticleIndex).photo);
    }

    public void closeArticleView()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        btnBack.SetActive(false);
        articleView.SetActive(false);
        titleTxt.SetActive(true);
        transform.parent.gameObject.SetActive(true);
    }
}
