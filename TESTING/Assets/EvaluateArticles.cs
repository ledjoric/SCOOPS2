using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EvaluateArticles : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject evalDialog, eval, interactButton, mainCanvas;

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

        if(gameData.articlesList.Count == 6)
        {
            evalDialog.transform.GetChild(0).gameObject.SetActive(true);
            evalDialog.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            evalDialog.transform.GetChild(0).gameObject.SetActive(false);
            evalDialog.transform.GetChild(1).gameObject.SetActive(true);
            evalDialog.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Inisufficient articles (" +
                gameData.articlesList.Count + "/" + gameData.articlesJson.articles.Length + ").\nCheck your objectives!";
        }
    }

    public void complete()
    {
        evalDialog.SetActive(false);
        mainCanvas.SetActive(false);
        eval.SetActive(true);
    }

    public void incomplete()
    {
        evalDialog.SetActive(false);
    }
}
