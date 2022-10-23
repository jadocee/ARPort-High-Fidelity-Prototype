using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

public class DialogDemo : MonoBehaviour
{
    private DialogController _dialogController;

    private void Awake()
    {
        var dialogControllerObject = GameObject.FindGameObjectWithTag("DialogController");
        if (dialogControllerObject == null) return;
        _dialogController = dialogControllerObject.GetComponent<DialogController>();
    }

    private IEnumerator Start()
    {
        yield return StartCoroutine(OpenDialogAfterSeconds(8));
    }

    private IEnumerator<WaitForSeconds> OpenDialogAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _dialogController.OpenOkayDialog("Your flight will boarding soon",
            "Flight 999 will begin boarding shortly. Please make your way to Gate A.",
            DialogController.DialogSize.Large);
    }
}