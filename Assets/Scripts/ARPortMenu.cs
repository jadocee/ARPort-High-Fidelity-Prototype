using System;
using UnityEngine;


public class ARPortMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuContent;
    private void Awake()
    {
        if (menuContent != null)
        {
            menuContent.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}