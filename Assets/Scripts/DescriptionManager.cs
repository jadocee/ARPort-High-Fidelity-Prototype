using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using TMPro;
using UnityEngine;


public class DescriptionManager : MonoBehaviour
{


    public TextMeshProUGUI DescriptionText1;
    public TextMeshProUGUI DescriptionText2;
    public TextMeshProUGUI DescriptionText3;
    public TextMeshProUGUI ConfirmedText;

    public string testdescription1 = "This is a sample description";
    public string testdescription2 = "This is a another alert";
    public string testdescription3 = "Damn alert 3 is out";
    
    public string active = "False";
    // Start is called before the first frame update
    void Start()
    {
        DescriptionText1.text = testdescription1;
        DescriptionText2.text = testdescription2;
        DescriptionText3.text = testdescription3;
        ConfirmedText.text = "The status of the confirmation is " + active;

    }
    

    // Update is called once per frame
    void Update()
    {


    }
}
