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
    private GameObject HMB;
    private bool HMCancel;
    private void Awake() //Creates the introduction dialog on awake
    {
        var toaster = GameObject.FindGameObjectWithTag("DialogController");
        if (toaster != null)
        {
            var toasterScript = toaster.GetComponent<DialogController>();
            if (toasterScript != null)
            {
                toasterScript.OpenOkayDialog("Welcome to the ARPort AR Tutorial", "This tutorial is designed to teach new Augmented Reality users about the types of interactions performed during AR that are utilised in the ARPort application. To begin, dismiss this pop-up by tapping the button labelled \"OK\" using your hand.", callback: (property) =>
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
                    toasterScript.OpenOkayDialog("Using the Hand Menu", "In the ARPort application, objects and menus can appear based on the gesture you're making with your hand. Try positioning your left hand flat with your palm facing towards you. Upon successfully performing the gesture a button labelled \"Complete Task\" will appear. Press this button to proceed with the tutorial.", callback: (property) =>
                    {
                        if ()
                        {
                        Debug.Log("Dismissed");
                        Destroy(property.TargetDialog.gameObject);
                        PinchTutorial();
                        HMButton.SetActive(false);
                        }
                    });
                }
            }
    }

    public void dismissHM() {
        Debug.Log("Dismissed");
        Destroy(property.TargetDialog.gameObject);
        PinchTutorial();
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
                toasterScript.OpenOkayDialog("Moving Menus with the Pinch Gesture", "", callback: (property) =>
                {
                    if (property.ResultContext.ButtonType.Equals(DialogButtonType.OK))
                    {
                        Debug.Log("Dismissed");
                        Destroy(property.TargetDialog.gameObject);
                    }
                });
            }
        }
    }
}
