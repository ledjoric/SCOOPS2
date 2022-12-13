using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class OpenApp : MonoBehaviour
{

    // WIP
    [SerializeField] private GameObject contentPanel, content, backGround, btnBack, options, darkPanel;
    [SerializeField] private TextMeshProUGUI titleTxt;
    [SerializeField] private Animator animator;
    // public int startIndex;

    public void openApp()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        string iconName = EventSystem.current.currentSelectedGameObject.name;

        if (!contentPanel.activeInHierarchy)
        {
            contentPanel.SetActive(true);
        }

        // CLOSE THE LAST OPEN APP
        foreach (Transform contents in content.transform)
        {

            if (contents.gameObject.activeInHierarchy)
            {
                contents.gameObject.SetActive(false);

                if(btnBack.activeInHierarchy)
                {
                    btnBack.SetActive(false);
                    titleTxt.gameObject.SetActive(true);
                }
            }
        }

        for (int i = 0; i < content.transform.childCount; i++)
        {
            if (iconName == content.transform.GetChild(i).name)
            {
                // SET UI
                if (iconName == "FireFacts")
                {
                    backGround.SetActive(true);
                }
                else if (iconName != "FireFacts" && backGround.activeInHierarchy)
                {
                    backGround.SetActive(false);
                }

                // SET THE OBJECT ACTIVE
                if (!content.transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    titleTxt.text = iconName;
                    content.transform.GetChild(i).gameObject.SetActive(true);
                    content.GetComponent<ScrollRect>().content = content.transform.GetChild(i).GetComponent<RectTransform>();
                }
            }
        }
    }

    public void openOptions()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        options.SetActive(true);
        contentPanel.SetActive(false);
    }

    public void exitOptions()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        options.SetActive(false);
    }

    public void exitGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        Application.Quit();
    }
}
