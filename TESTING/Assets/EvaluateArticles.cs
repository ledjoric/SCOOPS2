using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EvaluateArticles : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject evalDialog, eval, interactButton, mainCanvas, darkPanel;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioMixer audioMixer;

    private string[] clueSplit;
    private int clues;

    private void OnTriggerEnter(Collider collisionInfo)
    {
        interactButton.SetActive(true);
        interactButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("InteractionAsset/OBJECT");
        interactButton.GetComponentInChildren<TextMeshProUGUI>().text = "  Use";
        interactButton.GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;

        interactButton.GetComponent<Button>().onClick.RemoveListener(showDialog);
        interactButton.GetComponent<Button>().onClick.AddListener(showDialog);

        gameObject.GetComponent<Outline>().enabled = true;
    }

    private void OnTriggerExit(Collider collisionInfo)
    {
        interactButton.SetActive(false);
        interactButton.GetComponent<Button>().onClick.RemoveAllListeners();

        gameObject.GetComponent<Outline>().enabled = false;
    }

    private void showDialog()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        evalDialog.SetActive(true);
        darkPanel.SetActive(true);
        cluesCount();

        if(gameData.articlesList.Count == 6)
        {
            evalDialog.transform.GetChild(0).gameObject.SetActive(true);
            evalDialog.transform.GetChild(1).gameObject.SetActive(false);

            if (clues != 5)
            {
                evalDialog.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text
                    = "Insufficient clues. Are you sure you want to proceed? You cannot go back once you enter.";
            } else
            {
                evalDialog.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text
                    = "Are you sure you want to enter the Evaluation stage? You cannot go back once you enter.";
            }
            
        }
        else
        {
            evalDialog.transform.GetChild(0).gameObject.SetActive(false);
            evalDialog.transform.GetChild(1).gameObject.SetActive(true);
            evalDialog.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Insufficient articles (" +
                gameData.articlesList.Count + "/" + gameData.articlesJson.articles.Length + ").\nCheck your objectives!";
        }
    }

    public void complete()
    {
        //animator.SetTrigger("Start");
        //evalDialog.SetActive(false);
        //mainCanvas.SetActive(false);
        //eval.SetActive(true);
        FindObjectOfType<LevelLoader>().randomTips();
        StartCoroutine(transition());
    }

    private IEnumerator transition()
    {
        animator.SetTrigger("Start");
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "Music", 1, -80f));
        yield return new WaitForSeconds(1f);

        FindObjectOfType<AudioManager>().StopPlaying("MainTheme");
        evalDialog.SetActive(false);
        mainCanvas.SetActive(false);
        eval.SetActive(true);

        yield return new WaitForSeconds(1f);

        FindObjectOfType<AudioManager>().Play("ArticleChoosing");
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "Music", 1, 1));
        animator.SetTrigger("End");
    }

    public void incomplete()
    {
        evalDialog.SetActive(false);
    }

    public void cluesCount()
    {
        foreach(string clue in gameData.cluesList)
        {
            clueSplit = clue.Split('#');
            if (clueSplit[0] == "clue")
            {
                clues++;
            }
        }
    }
}
