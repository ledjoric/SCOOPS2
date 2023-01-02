using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadGuide : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    [SerializeField] private TextAsset wtdData;
    [SerializeField] private TextAsset craapData;
    [SerializeField] private TextAsset basicsData;
    private Guides wtdJson;
    private Guides craapJson;
    private Guides basicsJson;

    [SerializeField] private TextMeshProUGUI basicsTitle, basicsDetails, craapTitle, craapDetails;
    [SerializeField] private Transform wtdItems, wtdContent, basicsItems, basicsContent, craapItems, craapContent;
    [SerializeField] private GameObject examplesTemplate, wtd, basics, craap, btnExit, focus;
    private GameObject g;

    private int index;
    private string[] detailSplit;
    private string btnName;

    private void OnEnable()
    {
        gameData.guideActive = true;

        if (gameData.tutorial)
        {
            btnExit.SetActive(false);
            focus.SetActive(false);
        }
        else
        {
            btnExit.SetActive(true);
        }
    }

    private void OnDisable()
    {
        gameData.guideActive = false;
    }

    private void Start()
    {
        wtdJson = JsonUtility.FromJson<Guides>(wtdData.text);
        craapJson = JsonUtility.FromJson<Guides>(craapData.text);
        basicsJson = JsonUtility.FromJson<Guides>(basicsData.text);

        btnName = "";

        loadItems();
        if (gameData.tutorial)
        {
            wtd.transform.SetParent(transform);
            wtd.SetActive(true);
        }
        else
        {
            wtd.transform.SetParent(transform);
            basics.transform.SetParent(transform);
            craap.transform.SetParent(transform);

            wtd.SetActive(true);
            basics.SetActive(true);
            craap.SetActive(true);
        }
    }

    private void loadItems()
    {
        basicsTitle.text = basicsJson.guide_title;
        basicsDetails.text = basicsJson.guide_details;

        craapTitle.text = craapJson.guide_title;
        craapDetails.text = craapJson.guide_details;

        foreach (Transform items in wtdItems)
        {
            items.GetComponentInChildren<TextMeshProUGUI>().text = getWTDGuide(items.GetSiblingIndex()).name;
        }

        foreach (Transform items in basicsItems)
        {
            items.GetComponentInChildren<TextMeshProUGUI>().text = getBasicsGuide(items.GetSiblingIndex()).name;
        }

        foreach (Transform items in craapItems)
        {
            items.GetComponentInChildren<TextMeshProUGUI>().text = getCRAAPGuide(items.GetSiblingIndex()).name;
        }

        refreshWTD();
        refreshBasics();
        refreshCRAAP();
    }

    public void wtdtemClick()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        index = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();
        btnName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;

        wtdContent.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = getWTDContent(index).title;
        wtdContent.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(getWTDContent(index).photo);
        wtdContent.GetChild(2).GetComponent<TextMeshProUGUI>().text = getWTDContent(index).details;

        wtdTutorialProg();
        refreshWTD();
    }


    public void basicsItemClick()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        index = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();
        btnName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;

        detailSplit = getBasicsContent(index).details.Split('#');

        basicsContent.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = getBasicsContent(index).title;
        basicsContent.GetChild(1).GetComponent<TextMeshProUGUI>().text = detailSplit[0];
        basicsContent.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>(getBasicsContent(index).photo);
        basicsContent.GetChild(2).GetComponent<Image>().SetNativeSize();
        basicsContent.GetChild(3).GetComponent<TextMeshProUGUI>().text = detailSplit[1];

        basicsTutorialProg();
        refreshBasics();
    }

    public void craapItemClick()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        foreach (Transform clone in craapContent.GetChild(3))
        {
            if (clone.name == "Example(Clone)")
            {
                Destroy(clone.gameObject);
            }
        }

        index = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();
        btnName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;

        craapContent.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = getCRAAPContent(index).title;
        craapContent.GetChild(1).GetComponent<TextMeshProUGUI>().text = getCRAAPContent(index).details;

        examplesTemplate.gameObject.SetActive(false);

        foreach (Examples examples in getCRAAPContent(index).examples)
        {
            g = Instantiate(examplesTemplate, craapContent.GetChild(3));
            g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = examples.example;
            g.SetActive(true);
        }

        craapTutorialProg();
        refreshCRAAP();
    }

    private void wtdTutorialProg()
    {
        if (gameData.tutorial)
        {
            if (btnName == "Collect" && !gameData.collect)
            {
                gameData.collect = true;
                gameData.wtd++;
            }
            else if (btnName == "Explore" && !gameData.explore)
            {
                gameData.explore = true;
                gameData.wtd++;
            }
            else if (btnName == "Evaluate" && !gameData.evaluate)
            {
                gameData.evaluate = true;
                gameData.wtd++;
            }
            else if (btnName == "Publish" && !gameData.publish)
            {
                gameData.publish = true;
                gameData.wtd++;
            }

            if (gameData.wtd == 4)
            {
                basics.transform.SetParent(transform);
                basics.SetActive(true);
            }
        }
    }

    private void basicsTutorialProg()
    {
        if (gameData.tutorial)
        {
            if (btnName == "Phone" && !gameData.phone)
            {
                gameData.phone = true;
                gameData.basics++;
            }
            else if (btnName == "NPCs" && !gameData.npc)
            {
                gameData.npc = true;
                gameData.basics++;
            }
            else if (btnName == "Objects" && !gameData.objects)
            {
                gameData.objects = true;
                gameData.basics++;
            }
            else if (btnName == "Articles" && !gameData.article)
            {
                gameData.article = true;
                gameData.basics++;
            }
            else if (btnName == "Clues" && !gameData.clue)
            {
                gameData.clue = true;
                gameData.basics++;
            }

            if (gameData.basics == 5)
            {
                craap.transform.SetParent(transform);
                craap.SetActive(true);
            }
        }
    }

    private void craapTutorialProg()
    {
        if (gameData.tutorial)
        {
            if (btnName == "Currency" && !gameData.currency)
            {
                gameData.currency = true;
                gameData.craap++;
            }
            else if (btnName == "Relevance" && !gameData.relevance)
            {
                gameData.relevance = true;
                gameData.craap++;
            }
            else if (btnName == "Authority" && !gameData.authority)
            {
                gameData.authority = true;
                gameData.craap++;
            }
            else if (btnName == "Accuracy" && !gameData.accuracy)
            {
                gameData.accuracy = true;
                gameData.craap++;
            }
            else if (btnName == "Purpose" && !gameData.purpose)
            {
                gameData.purpose = true;
                gameData.craap++;
            }

            if (gameData.craap == 5)
            {
                gameData.tutorial = false;
                btnExit.SetActive(true);
            }
        }
    }


    private void refreshWTD()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(wtdContent.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(wtdItems.parent.GetComponent<RectTransform>());
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
    private Guide getWTDGuide(int index)
    {
        return wtdJson.guide[index];
    }

    private Content getWTDContent(int index)
    {
        return wtdJson.guide[index].content[0];
    }

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
