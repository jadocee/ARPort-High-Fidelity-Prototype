using JetBrains.Annotations;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Controller;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] private GameObject hmButton;
    [SerializeField] private GameObject pinchTut;
    [SerializeField] private GameObject pinchMenu;
    private bool _hmCancel;
    private Dialog _currentD;

    public TutorialScript()
    {
        _hmCancel = false;
    }

    private void Awake() //Creates the introduction dialog on awake
    {
        pinchTut.SetActive(false);
        var dialogController = GameObject.FindGameObjectWithTag("DialogController");
        if (dialogController == null) return;
        var toasterScript = dialogController.GetComponent<DialogController>();
        if (toasterScript != null)
        {
            toasterScript.OpenOkayDialog("<size=0.09>Welcome to the ARPort AR Tutorial</size>",
                "<size=0.06>This tutorial is designed to teach new Augmented Reality users about the types of interactions performed during AR that are utilised in the ARPort application.</size>\n\n<size=0.06><b>To begin, dismiss this pop-up by tapping the button labelled <color=orange>\"OK\"</color> using your hand.</b></size>",
                DialogController.DialogSize.Large, callback: (property) =>
                {
                    if (!property.ResultContext.ButtonType.Equals(DialogButtonType.OK)) return;
                    Debug.Log("Dismissed");
                    Destroy(property.TargetDialog.gameObject);
                    hmButton.SetActive(true);
                    HandMenuTutorial();
                });
        }
    }

    private void HandMenuTutorial()
    {
        var dialogController = GameObject.FindGameObjectWithTag("DialogController");
        if (dialogController == null) return;
        var dialogControllerScript = dialogController.GetComponent<DialogController>();
        if (dialogControllerScript != null)
        {
            _currentD = dialogControllerScript.OpenAndGetDialog("<size=0.09>Using the Hand Menu</size>",
                "<size=0.06>In the ARPort application, objects and menus can appear based on the gesture you're making with your hand.</size>\n\n<size=0.06><b>Try positioning your left hand flat with your palm facing towards you. Upon successfully performing the gesture a button labelled <color=orange>\"Complete Task\"</color> will appear, which you can press to proceed with the tutorial.</b></size>",
                DialogController.DialogSize.Large);
        }
    }

    public void DismissHm()
    {
        Debug.Log("Dismissed");
        Destroy(_currentD.gameObject);
        PinchTutorial();
        pinchTut.SetActive(true);
        hmButton.SetActive(false);
    }


    private void PinchTutorial()
    {
        var toaster = GameObject.FindGameObjectWithTag("DialogController");
        if (toaster == null) return;
        var toasterScript = toaster.GetComponent<DialogController>();
        if (toasterScript == null) return;
        _currentD = toasterScript.OpenAndGetDialog("<size=0.09>Moving Menus with the Pinch Gesture</size>",
            "<size=0.06>In the ARPort application, you are able to perform a pinch gesture to move menus around the display</size>\n\n<size=0.06><b>Locate the pinch menu and move it closer to your position. To perform this action from a distance, open your palm towards the bar beneath the menu and pinch once the line that originates from your hand ends in a circle, drag it towards you by bringing your hand back. Press the <color=orange>\"X\"</color> button on the Pinch Menu to complete the tutorial",
            DialogController.DialogSize.Large);
        _currentD.transform.position = new Vector3((float) (pinchMenu.transform.position.x - 0.25), 0, 0.6f);
    }

    public void EndPinch()
    {
        Debug.Log("Dismissed");
        Destroy(_currentD.gameObject);
        pinchTut.SetActive(false);
        EndConfirm();
    }

    private void EndConfirm()
    {
        var dialogController = GameObject.FindGameObjectWithTag("DialogController");
        if (dialogController == null) return;
        var dialogControllerScript = dialogController.GetComponent<DialogController>();
        if (dialogControllerScript != null)
        {
            dialogControllerScript.OpenOkayDialog("<size=0.09>Tutorial Completed</size>",
                "<size=0.06>You have completed the ARPort Tutorial, please inform the observers to begin testing.</size>\n\n<size=0.06><b>Once the observers give you the okay, please press the <color=orange>\"OK\"</color> button to begin testing.</b></size>",
                DialogController.DialogSize.Large, callback: (property) =>
                {
                    if (!property.ResultContext.ButtonType.Equals(DialogButtonType.OK)) return;
                    Debug.Log("Dismissed");
                    Destroy(property.TargetDialog.gameObject);
                    SceneManager.LoadScene("Scenes/" + "GroupScene031022", LoadSceneMode.Single);
                });
        }
    }
}