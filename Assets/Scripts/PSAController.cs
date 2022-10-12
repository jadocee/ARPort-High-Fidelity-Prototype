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
    
    

    public void Start()
    {
       
        
    }
    

    public void DisplayPsa()
    {
       Dialog.InstantiateFromPrefab(psaPrefab, new DialogProperty("Alert 1", "This is an example of an Alert with a choice message for the user, placed at near interaction range", DialogButtonHelpers.OK), true, true);

        
            
        }
    
public void DisplayConfimation()
{
    Dialog.InstantiateFromPrefab(psaPrefab, new DialogProperty("Confirmation", "This is an example of a Confirmation Alert with a choice message for the user, placed at near interaction range", DialogButtonHelpers.YesNo), true, true);

        
            
}
}

    

    



