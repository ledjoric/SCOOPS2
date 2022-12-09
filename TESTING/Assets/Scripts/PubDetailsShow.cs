using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PubDetailsShow : MonoBehaviour
{
    [SerializeField] GameObject darkPanel;

    private void OnEnable()
    {
        darkPanel.SetActive(true);
    }

    private void OnDisable()
    {
        darkPanel.SetActive(false);
        
        foreach(Transform child in gameObject.transform)
        {
            if((child.name == "Publisher" && !child.gameObject.activeInHierarchy) || child.name == "BtnExit")
            {
                child.gameObject.SetActive(true);
            }else
            {
                child.gameObject.SetActive(false);
            }
        }
        
    }

    public void PubDetailsExit()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        gameObject.SetActive(false);
    }
}
