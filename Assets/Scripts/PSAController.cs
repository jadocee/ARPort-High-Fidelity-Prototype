using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using UnityEngine;using UnityEngine.PlayerLoop;

public class PSAController : MonoBehaviour
{
    [SerializeField] private GameObject psaPrefab;
    [SerializeField] private GameObject psaContainer;

    public void Awake()
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
    }

    public void DisplayPSA(String header, String desc = "")
    {
        var psa = Instantiate(psaPrefab, psaContainer.transform.position, psaContainer.transform.rotation);
        if (psa != null)
        {
            var sericeAlert = psa.GetComponent<PSA>();
            if (sericeAlert != null)
            {
                
            }

            psaContainer.GetComponent<Follow>().enabled = true;
        }
    }
    
     void Update()
    {
        Instantiate(psaPrefab, psaContainer.transform.position, psaContainer.transform.rotation);
    }

}


public class PSA
{
    public PSA()
    {
        
    }
}
