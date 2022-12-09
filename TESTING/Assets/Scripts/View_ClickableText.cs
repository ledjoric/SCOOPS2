using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class View_ClickableText : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject publisherDetails;
    [SerializeField] private GameData gameData;

    private string refTitle;
    //private int articleIndex;

    public void OnPointerClick(PointerEventData eventData)
    {
        var text = GetComponent<TextMeshProUGUI>();
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
            if (linkIndex != -1)
            {
                refTitle = transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text;
                //gameData.viewArticleIndex = gameData.selectedArticles[transform.parent.parent.GetSiblingIndex()] - 1;
                //Debug.Log(gameData.getPublisherDetail(0).name);
                publisherDetails.SetActive(true);

                publisherDetails.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(gameData.getPublisherDetail(gameData.viewArticleIndex).pub_photo);
                publisherDetails.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = gameData.getPublisherDetail(gameData.viewArticleIndex).name;
                publisherDetails.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = gameData.getPublisherDetail(gameData.viewArticleIndex).type;
                publisherDetails.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = gameData.getPublisherDetail(gameData.viewArticleIndex).description;
            }
        }
    }
}
