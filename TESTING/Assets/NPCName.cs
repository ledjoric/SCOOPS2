using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCName : MonoBehaviour
{
    private static NPCName instance;
    private Camera mainCamera;
    private Vector3 offset = new Vector3(0, 1f, 0);

    [SerializeField] private TextMeshProUGUI txtName;
    private TextMeshProUGUI instTxtName;

    private void Start()
    {
        instTxtName = Instantiate(txtName, FindObjectOfType<Canvas>().transform).GetComponent<TextMeshProUGUI>();
        instTxtName.GetComponent<TextMeshProUGUI>().text = gameObject.name;

        if(instTxtName.GetComponent<TextMeshProUGUI>().text == "Vivian" || instTxtName.GetComponent<TextMeshProUGUI>().text == "Noah" || instTxtName.GetComponent<TextMeshProUGUI>().text == "Stacy")
        {
            instTxtName.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //if (instance != this) return;
        
        if (!mainCamera) mainCamera = Camera.main;
        instTxtName.transform.position = mainCamera.WorldToScreenPoint(transform.GetChild(0).position + offset);

        
    }
}
