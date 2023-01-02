using UnityEngine;
using UnityEngine.EventSystems;

public class PublishScript : MonoBehaviour
{
    [SerializeField] GameObject publishContent1, publishContent2, cluesContent1, cluesContent2;

    public void viewClues()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "BtnViewClues1")
        {
            publishContent1.SetActive(false);
            cluesContent1.SetActive(true);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "BtnViewClues2")
        {
            publishContent2.SetActive(false);
            cluesContent2.SetActive(true);
        }
    }

    public void exitClues()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "BtnBack1")
        {
            publishContent1.SetActive(true);
            cluesContent1.SetActive(false);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "BtnBack2")
        {
            publishContent2.SetActive(true);
            cluesContent2.SetActive(false);
        }
    }
}
