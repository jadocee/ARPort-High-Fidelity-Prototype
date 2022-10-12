using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using Microsoft.MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;

public class PSAController : MonoBehaviour
{
    [SerializeField] private GameObject psaPrefab;
    [SerializeField] private GameObject psaContainer;
    private String header;
    private String description;
    

    /*public void Awake()
    {
        if (!psaContainer) 
        {
            Debug.Log("PSA Container is missing");
        }

        if (!psaPrefab)
        {
            Debug.Log("PSA Prehab is missing");
        }
        psaContainer.SetActive(false);
        psaContainer.GetComponent<Follow>().enabled = false;
    }*/
    

    public void DisplayPsa()
    {
        var psa = Instantiate(psaPrefab, psaContainer.transform.position, psaContainer.transform.rotation);
        if (psa != null)
        {
            psa.SetActive(true);
            /*psa.GetComponent<DescriptionManager>().Title.text = header;
            psa.GetComponent<DescriptionManager>().DescriptionText.text = description;
            psa.transform.SetParent(psaContainer.transform, false);*/

            //psaContainer.GetComponent<Follow>().enabled = true;
        }
    }
    
    //  void Update()
    // {
    //     Instantiate(psaPrefab, psaContainer.transform.position, psaContainer.transform.rotation);
    // }

}

