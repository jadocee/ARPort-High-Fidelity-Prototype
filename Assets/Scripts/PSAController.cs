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
    int newVal ;
    string newMessage;
    
    

    public void Start()
    {
       
        
    }


    public void DisplayPsa()
    {
        
        if (i < 100)
        {
            Dialog.InstantiateFromPrefab(psaPrefab,
                new DialogProperty("Alert " + i,
                    genMessage(i),
                    DialogButtonHelpers.OK), true, true);
            i++;
        }
    }

    private string genMessage(int i1)
    {
        newVal = i1 % 5;
        
        if (newVal == 1)
        {
            newMessage = "Hello user this is an alert";
        }
        if (newVal == 2)
        {
            newMessage = "Yay this is the second one you got";
        }
        if (newVal == 3)
        {
            newMessage = "damn Now there is an emergency";
        }
        if (newVal == 4)
        {
            newMessage = "This is the last one hopefully";
        }
        if(newVal == 0)
        {
            newMessage = "This will be the an alert just for you";
        }
        return newMessage;
    }

    public void DisplayConfimation()
{
    Dialog.InstantiateFromPrefab(confirmationPrefab, new DialogProperty("Confirmation", "This is an example of a Confirmation Alert with a choice message for the user, placed at near interaction range", DialogButtonHelpers.YesNo), true, true);

        
            
}
}

    

    



