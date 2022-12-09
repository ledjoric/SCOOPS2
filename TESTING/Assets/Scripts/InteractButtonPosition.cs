using UnityEngine;
using UnityEngine.UI;

public class InteractButtonPosition : MonoBehaviour
{

    //public Button UI;
    [SerializeField] GameObject uiUse;
    private Vector3 offset = new Vector3(0, 0.5f, 0);
    // Start is called before the first frame update
    void Start()
    {
        //uiUse = Instantiate(UI, FindObjectOfType<Canvas>().transform).GetComponent<Button>();
        //uiUse = GameObject.FindGameObjectWithTag("ViewButton");
    }

    // Update is called once per frame
    void Update()
    {
        uiUse.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + offset);
    }
    private void OnTriggerEnter(Collider collisionInfo)
    {
        if(collisionInfo.CompareTag("Player"))
        {
            uiUse.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collisionInfo)
    {
        if(collisionInfo.CompareTag("Player"))
        {
            //Destroy(uiUse.gameObject);
            uiUse.gameObject.SetActive(false);
        }
    }
        
}
