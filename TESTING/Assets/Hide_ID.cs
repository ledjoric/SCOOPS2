using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_ID : MonoBehaviour
{
    [SerializeField] private GameObject darkPanel;
    [SerializeField] private GameData gameData;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            darkPanel.SetActive(false);
            gameObject.SetActive(false);
            gameData.idShown = true;
        }
    }
}
