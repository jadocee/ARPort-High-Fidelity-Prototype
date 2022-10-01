using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UX;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    private TouchScreenKeyboard keyboard;
    private string keyboardText;
    private MRTKTMPInputField mrtktmpInputField;

    // Start is called before the first frame update
    void Start()
    {
        mrtktmpInputField = this.gameObject.GetComponent<MRTKTMPInputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (keyboard != null)
        {
            // keyboardText = keyboard.text;
            mrtktmpInputField.text = keyboardText;
        }
    }

    public void OpenSystemKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false);
    }
}
