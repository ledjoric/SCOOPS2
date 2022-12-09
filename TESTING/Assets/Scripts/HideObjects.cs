using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObjects : MonoBehaviour
{
    [SerializeField] private GameObject hud, bg;

    private void OnEnable()
    {
        hud.SetActive(false);
        bg.SetActive(true);
    }

    private void OnDisable()
    {
        hud.SetActive(true);
        bg.SetActive(false);
    }
}
