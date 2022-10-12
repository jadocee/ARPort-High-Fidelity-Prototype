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
    [SerializeField] private Dialog psaPrefab;
    [SerializeField] private Dialog confirmationPrefab;
    int i = 1;
    
    

    public void Start()
    {
       
        
    }


    public void DisplayPsa()
    {
        
        if (i < 100)
        {
            Dialog.InstantiateFromPrefab(psaPrefab,
                new DialogProperty("Alert " + i,
                    "This is an example of an Alert with a choice message for the user",
                    DialogButtonHelpers.OK), true, true);
            i++;
        }
    }

    public void DisplayConfimation()
{
    Dialog.InstantiateFromPrefab(confirmationPrefab, new DialogProperty("Confirmation", "This is an example of a Confirmation Alert with a choice message for the user, placed at near interaction range", DialogButtonHelpers.YesNo), true, true);

        
            
}
}

    

    



