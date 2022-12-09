using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDialog : MonoBehaviour
{
    public GameObject dialogBox;
    // Start is called before the first frame update
    void Start()
    {
        // dialogBox = GameObject.FindGameObjectWithTag("DialogBox");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showDialog() {
        //dialogBox.gameObject.SetActive(!dialogBox.gameObject.activeInHierarchy); 
    }
}
