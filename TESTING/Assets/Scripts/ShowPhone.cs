using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class ShowPhone : MonoBehaviour, ISelectHandler
{
    [SerializeField] GameObject phonePanel, invPanel, invPanel2;

    public void OnSelect(BaseEventData eventData)
    {
        phonePanel.SetActive(true);
        invPanel.SetActive(true);
        invPanel2.SetActive(true);
    }
}
