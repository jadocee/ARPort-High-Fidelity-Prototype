using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class TranslationAlert : MonoBehaviour
{
    public TextMeshPro DescriptionText;
    public bool TranslationStatus;

    // Start is called before the first frame update
    void Start()
    {
        TranslationStatus = false;
    }
    
    void Update()
    {
        
    }
}
