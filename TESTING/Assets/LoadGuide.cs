using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadGuide : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    [SerializeField] private TextAsset craapData;
    [SerializeField] private TextAsset basicsData;
    private Guides craapJson;
    private Guides basicsJson;

    [SerializeField] private TextMeshProUGUI basicsTitle, basicsDetails, craapTitle, craapDetails;
    [SerializeField] private Transform basicsItems, basicsContent, craapItems, craapContent;
    [SerializeField] private GameObject examplesTemplate;
    private GameObject g;

    private int index;
    private string[] detailSplit;

    private void OnEnable()
    {
        gameData.guideActive = true;
    }

    private void OnDisable()
    {
        gameData.guideActive = false;
    }

    private void Start()
    {
        craapJson = JsonUtility.FromJson<Guides>(craapData.text);
        basicsJson = JsonUtility.FromJson<Guides>(basicsData.text);

        loadItems();
    }

    private void loadItems()
    {
        basicsTitle.text = basicsJson.guide_title;
        basicsDetails.text = basicsJson.guide_details;

        craapTitle.text = craapJson.guide_title;
        craapDetails.text = craapJson.guide_details;

        foreach (Transform items in basicsItems)
        {
            items.GetComponentInChildren<TextMeshProUGUI>().text = getBasicsGuide(items.GetSiblingIndex()).name;
        }

        foreach (Transform items in craapItems)
        {
            items.GetComponentInChildren<TextMeshProUGUI>().text = getCRAAPGuide(items.GetSiblingIndex()).name;
        }

        refreshBasics();
        refreshCRAAP();
    }

    public void basicsItemClick()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        index = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();

        detailSplit = getBasicsContent(index).details.Split('#');

        basicsContent.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = getBasicsContent(index).title;
        basicsContent.GetChild(1).GetComponent<TextMeshProUGUI>().text = detailSplit[0];
        basicsContent.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>(getBasicsContent(index).photo);
        basicsContent.GetChild(2).GetComponent<Image>().SetNativeSize();
        basicsContent.GetChild(3).GetComponent<TextMeshProUGUI>().text = detailSplit[1];

        refreshBasics();
    }

    public void craapItemClick()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        foreach (Transform clone in craapContent.GetChild(3))
        {
            if (clone.name == "Example(Clone)" )
            {
                Destroy(clone.gameObject);
            }
        }

        index = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();

        craapContent.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = getCRAAPContent(index).title;
        craapContent.GetChild(1).GetComponent<TextMeshProUGUI>().text = getCRAAPContent(index).details;
        
        examplesTemplate.gameObject.SetActive(false);

        foreach(Examples examples in getCRAAPContent(index).examples)
        {
            g = Instantiate(examplesTemplate, craapContent.GetChild(3));
            g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = examples.example;
            g.SetActive(true);
        }

        refreshCRAAP();
    }

    private void refreshBasics()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(basicsContent.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(basicsItems.parent.GetComponent<RectTransform>());
    }

    private void refreshCRAAP()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(craapContent.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(craapContent.GetChild(3).GetComponent<RectTransform>());
    }

    // GETTERS FOR BASICS JSON
    private Guide getBasicsGuide(int index)
    {
        return basicsJson.guide[index];
    }

    private Content getBasicsContent(int index)
    {
        return basicsJson.guide[index].content[0];
    }

    // GETTERS FOR CRAAP JSON
    private Guide getCRAAPGuide(int index)
    {
        return craapJson.guide[index];
    }

    private Content getCRAAPContent(int index)
    {
        return craapJson.guide[index].content[0];
    }
}
