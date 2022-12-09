using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject phonePanel, contentPanel;

    public void openPhone()
    {
        //phonePanel.GetComponent<LayoutElement>().ignoreLayout = true;
        phonePanel.SetActive(true);
        //invPanel.SetActive(true);
        //invPanel2.SetActive(true);
    }

    public void closePhone()
    {
        //phonePanel.GetComponent<LayoutElement>().ignoreLayout = true;
        phonePanel.SetActive(false);
        // contentPanel.SetActive(false);
        //invPanel.SetActive(false);
        //invPanel2.SetActive(false);
    }
}
