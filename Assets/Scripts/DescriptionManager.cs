using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using TMPro;
using UnityEngine;


public class DescriptionManager : MonoBehaviour
{
    public TextMeshPro DescriptionText;

    public string testdescription = "This is a sample Description";
    // Start is called before the first frame update
    void Start()
    {
        DescriptionText.text = "test: " + testdescription;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
