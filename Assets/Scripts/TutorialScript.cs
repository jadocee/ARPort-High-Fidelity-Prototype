using JetBrains.Annotations;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ToastNotifications;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField]
    private GameObject HMButton;
    [SerializeField]
    private GameObject PinchTut;
    [SerializeField]
    private GameObject TutContainer;
    private bool HMCancel;
    private Dialog CurrentD;
    private void Awake() //Creates the introduction dialog on awake
    {
        var toaster = GameObject.FindGameObjectWithTag("DialogController");
        if (toaster != null)
        {
            var toasterScript = toaster.GetComponent<DialogController>();
            if (toasterScript != null)
            {
                toasterScript.OpenOkayDialog("<size=0.09>Welcome to the ARPort AR Tutorial</size>", "<size=0.06>This tutorial is designed to teach new Augmented Reality users about the types of interactions performed during AR that are utilised in the ARPort application.</size>\n\n<size=0.06><b>To begin, dismiss this pop-up by tapping the button labelled <color=orange>\"OK\"</color> using your hand.</b></size>", DialogController.DialogSize.Large, callback: (property) =>
                {
                    if (property.ResultContext.ButtonType.Equals(DialogButtonType.OK))
                    {
                        Debug.Log("Dismissed");
                        Destroy(property.TargetDialog.gameObject);
                        HMButton.SetActive(true);
                        HandMenuTutorial();
                    }
                });
            }
        }
    }

    public void HandMenuTutorial()
    {
        var toaster = GameObject.FindGameObjectWithTag("DialogController");
            if (toaster != null)
            {
                var toasterScript = toaster.GetComponent<DialogController>();
                if (toasterScript != null)
                {
                CurrentD = toasterScript.OpenDialog("<size=0.09>Using the Hand Menu</size>", "<size=0.06>In the ARPort application, objects and menus can appear based on the gesture you're making with your hand.</size>\n\n<size=0.06><b>Try positioning your left hand flat with your palm facing towards you. Upon successfully performing the gesture a button labelled <color=orange>\"Complete Task\"</color> will appear, which you can press to proceed with the tutorial.</b></size>", DialogController.DialogSize.Large);
                }
            }
    }

    public void dismissHM() {
        Debug.Log("Dismissed");
        Destroy(CurrentD.gameObject);
        PinchTutorial();
        PinchTut.SetActive(true);
        HMButton.SetActive(false);
    }


    public void PinchTutorial()
    {
        var toaster = GameObject.FindGameObjectWithTag("DialogController");
        if (toaster != null)
        {
            var toasterScript = toaster.GetComponent<DialogController>();
            if (toasterScript != null)
            {
            CurrentD = toasterScript.OpenDialog("<size=0.09>Moving Menus with the Pinch Gesture</size>", "<size=0.06>In the ARPort application, you are able to perform a pinch gesture to move menus around the display</size>\n\n<size=0.06><b>Locate the pinch menu and move it closer to your position. To perform this action from a distance, open your palm towards the bar beneath the menu and pinch once the line that originates from your hand ends in a circle, drag it towards you by bringing your hand back. Press the <color=orange>\"X\"</color> button on the Pinch Menu to complete the tutorial", DialogController.DialogSize.Large);
            } 
        }
    }

    public void EndTutorial() {
        Debug.Log("Dismissed");
        Destroy(CurrentD.gameObject);
        TutContainer.SetActive(false);
    }
}
