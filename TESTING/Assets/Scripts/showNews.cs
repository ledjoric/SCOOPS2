using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showNews : MonoBehaviour
{
    [SerializeField] GameObject _toggleObject;


    // Start is called before the first frame update
    void Start()
    {
        // disableMovement = GameObject.Find("Fixed Joystick");
        // disableHud = GameObject.Find("Phone");
        // uiUse = Instantiate(UI, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newsPaperShow()
    {
        _toggleObject.SetActive(!_toggleObject.gameObject.activeInHierarchy);
    }
}
