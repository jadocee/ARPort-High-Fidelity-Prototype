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
    public TextMeshProUGUI header;
    public TextMeshProUGUI description;
    

    public void Start()
    {
       
        header.text = "Alert 1-1";
        description.text = "Description yay";
    }
    

    public void DisplayPsa()
    {
        var psa = Instantiate(psaPrefab, psaContainer.transform.position, psaContainer.transform.rotation);
        if (psa != null)
        {
            psa.SetActive(true);
            var serviceAlert = psa.GetComponent<DescriptionManager>();
            if (serviceAlert != null)
            {
               // serviceAlert.Title = header;
                //serviceAlert.DescriptionText = description;
            }
            
            //psa.transform.SetParent(psaContainer.transform, false);

            //psaContainer.GetComponent<Follow>().enabled = true;
        }
    }
    
    //  void Update()
    // {
    //     Instantiate(psaPrefab, psaContainer.transform.position, psaContainer.transform.rotation);
    // }

}

