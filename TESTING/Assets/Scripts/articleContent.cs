using System.Collections;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class articleContent : MonoBehaviour
{
    private int articleIndex;

    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject ffDialog, darkPanel, ffNotif;
    [SerializeField] private Animator notifAnim;

    // MINIGAME POINTING SYSTEM

    public void share()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        articleIndex = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetSiblingIndex() - 1;


        if (gameData.getMG_Article(gameData.mgArticlesList[articleIndex]).credibility == "Credible")
        {
            darkPanel.GetComponent<Image>().color = new Color32(118, 209, 127, 130);
            ffDialog.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameData.getMG_Publisher(gameData.mgArticlesList[articleIndex]).name + " is a credible source";
        }
        else if (gameData.getMG_Article(gameData.mgArticlesList[articleIndex]).credibility == "NotCredible")
        {
            darkPanel.GetComponent<Image>().color = new Color32(244, 67, 54, 130);
            ffDialog.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameData.getMG_Publisher(gameData.mgArticlesList[articleIndex]).name + " is not a credible source";
        }

        darkPanel.gameObject.SetActive(true);
        ffDialog.gameObject.SetActive(true);
        ffDialog.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("FireFactsIcons/Share");
        ffDialog.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Shared!";
        ffDialog.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(159, 181, 191, 255);

        gameData.viewedFF++;
        gameData.minigameProgress++;

        if (gameData.minigameProgress == gameData.articlesMinigame)
        {
            ffNotif.SetActive(false);
        }

        if ((gameData.cluesList.Count == gameData.viewedClues.Count) && (gameData.articlesList.Count == gameData.viewedArticles.Count) && (gameData.articlesMinigame == gameData.viewedFF))
        {
            notifAnim.SetTrigger("HideNotif");
        }

        foreach(Transform buttons in EventSystem.current.currentSelectedGameObject.transform.parent)
        {
            buttons.GetComponent<Button>().enabled = false;
        }

        StartCoroutine(removeArticle());
    }

    public void like()
    {
        Debug.Log(gameData.getMG_Article(articleIndex).credibility);
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        articleIndex = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetSiblingIndex() - 1;
        if (gameData.getMG_Article(gameData.mgArticlesList[articleIndex]).credibility == "Credible")
        {
            darkPanel.GetComponent<Image>().color = new Color32(118, 209, 127, 130);
            ffDialog.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameData.getMG_Publisher(gameData.mgArticlesList[articleIndex]).name + " is a credible source";
        }
        else if(gameData.getMG_Article(gameData.mgArticlesList[articleIndex]).credibility == "NotCredible")
        {
            darkPanel.GetComponent<Image>().color = new Color32(244, 67, 54, 130);
            ffDialog.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameData.getMG_Publisher(gameData.mgArticlesList[articleIndex]).name + " is not a credible source";
        }

        darkPanel.gameObject.SetActive(true);
        ffDialog.gameObject.SetActive(true);
        ffDialog.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("FireFactsIcons/Like");
        ffDialog.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Liked!";
        ffDialog.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(183,128,128,255);

        gameData.viewedFF++;
        gameData.minigameProgress++;

        if (gameData.minigameProgress == gameData.articlesMinigame)
        {
            ffNotif.SetActive(false);
        }

        if ((gameData.cluesList.Count == gameData.viewedClues.Count) && (gameData.articlesList.Count == gameData.viewedArticles.Count) && (gameData.articlesMinigame == gameData.viewedFF))
        {
            notifAnim.SetTrigger("HideNotif");
        }

        foreach (Transform buttons in EventSystem.current.currentSelectedGameObject.transform.parent)
        {
            buttons.GetComponent<Button>().enabled = false;
        }

        StartCoroutine(removeArticle());
    }

    public void doubt()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        articleIndex = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetSiblingIndex() - 1;
        if (gameData.getMG_Article(gameData.mgArticlesList[articleIndex]).credibility == "Credible")
        {
            darkPanel.GetComponent<Image>().color = new Color32(244, 67, 54, 130);
            ffDialog.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameData.getMG_Publisher(gameData.mgArticlesList[articleIndex]).name + " is a credible source";
        }
        else if (gameData.getMG_Article(gameData.mgArticlesList[articleIndex]).credibility == "NotCredible")
        {
            darkPanel.GetComponent<Image>().color = new Color32(118, 209, 127, 130);
            ffDialog.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameData.getMG_Publisher(gameData.mgArticlesList[articleIndex]).name + " is not a credible source";
        }

        darkPanel.gameObject.SetActive(true);
        ffDialog.gameObject.SetActive(true);
        ffDialog.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("FireFactsIcons/Doubt");
        ffDialog.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Doubted!";
        ffDialog.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(191, 130, 128, 255);

        gameData.viewedFF++;
        gameData.minigameProgress++;

        if (gameData.minigameProgress == gameData.articlesMinigame)
        {
            ffNotif.SetActive(false);
        }

        if ((gameData.cluesList.Count == gameData.viewedClues.Count) && (gameData.articlesList.Count == gameData.viewedArticles.Count) && (gameData.articlesMinigame == gameData.viewedFF))
        {
            notifAnim.SetTrigger("HideNotif");
        }

        foreach (Button buttons in EventSystem.current.currentSelectedGameObject.transform.parent)
        {
            buttons.enabled = false;
        }

        StartCoroutine(removeArticle());
    }

    private IEnumerator removeArticle()
    {
        yield return StartCoroutine(hideDialog(1f));
        Destroy(EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject);
        gameData.mgArticlesList.RemoveAt(articleIndex);
    }

    private IEnumerator hideDialog(float time)
    {
        yield return new WaitForSeconds(time);
        ffDialog.SetActive(false);
        darkPanel.SetActive(false);
    }
}
