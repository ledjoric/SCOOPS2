using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ArticleViewRefresh : MonoBehaviour
{
    [SerializeField] private GameObject content;

    private void OnEnable()
    {
        StartCoroutine(refreshLayout());
    }

    private IEnumerator refreshLayout()
    {
        content.GetComponent<VerticalLayoutGroup>().enabled = false;
        yield return new WaitForSeconds(0.001f);
        content.GetComponent<VerticalLayoutGroup>().enabled = true;
    }
}
