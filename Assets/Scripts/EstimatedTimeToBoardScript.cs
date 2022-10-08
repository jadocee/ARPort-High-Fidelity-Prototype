using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EstimatedTimeToBoardScript : MonoBehaviour
{
    char ch = 'A';
    public Component[] buttonArray;
    private GameObject findingETABar;
    // Start is called before the first frame update
    void Start()
    {
        buttonArray = GetComponentsInChildren<TextMeshPro>();

        foreach (TextMeshPro text in buttonArray) {
            text.SetText("Gate " + ch);
            ch++;
        }
        
    }

    // The following methods run when the described button is pressed
    public void topLeft() {
        findETABar();
        TextMeshPro tmp = findingETABar.GetComponentInChildren<TextMeshPro>(true);
        tmp.SetText("Navigating to Gate A \nExpected to arrive in 10 minutes | 1km to go");
    }

    public void topRight() {
        findETABar();
        TextMeshPro tmp = findingETABar.GetComponentInChildren<TextMeshPro>(true);
        tmp.SetText("Navigating to Gate B \nExpected to arrive in 20 minutes | 2km to go");
        
    }

    public void bottomLeft() {
        findETABar();
        TextMeshPro tmp = findingETABar.GetComponentInChildren<TextMeshPro>(true);
        tmp.SetText("Navigating to Gate C \nExpected to arrive in 30 minutes | 3km to go");
    }

    public void bottomRight() {
        findETABar();
        TextMeshPro tmp = findingETABar.GetComponentInChildren<TextMeshPro>(true);
        tmp.SetText("Navigating to Gate D \nExpected to arrive in 40 minutes | 4km to go");
    }

    public void findETABar() {
        findingETABar = GameObject.Find("ETABar");
    }

}
