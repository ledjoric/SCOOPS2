using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class ClickableText : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject publisherDetails, knownPublisher, unkownPublisher, caution;
    [SerializeField] private GameData gameData;

    private string refTitle;
    //private int articleIndex;

    public void OnPointerClick(PointerEventData eventData)
    {
        var text = GetComponent<TextMeshProUGUI>();
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
            if(linkIndex != -1)
            {
                FindObjectOfType<AudioManager>().Play("ButtonSound");
                refTitle = transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text;

                gameData.viewArticleIndex = gameData.selectedArticles[transform.parent.parent.parent.parent.parent.GetSiblingIndex()] - 1;
                publisherDetails.SetActive(true);
                if (!gameData.cluesList.Contains(gameData.getPublisherDetail(gameData.viewArticleIndex).clue))
                {
                    caution.SetActive(false);
                    unkownPublisher.SetActive(true);
                    knownPublisher.SetActive(false);
                }else
                {
                    caution.SetActive(false);
                    unkownPublisher.SetActive(false);
                    knownPublisher.SetActive(true);

                    knownPublisher.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(gameData.getPublisherDetail(gameData.viewArticleIndex).pub_photo);
                    knownPublisher.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = gameData.getPublisherDetail(gameData.viewArticleIndex).name;
                    knownPublisher.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameData.getPublisherDetail(gameData.viewArticleIndex).type;
                    knownPublisher.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = gameData.getPublisherDetail(gameData.viewArticleIndex).description;
                }
            }
        }
    }
}
