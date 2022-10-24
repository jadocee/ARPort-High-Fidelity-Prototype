using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

namespace Demo
{
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
            yield return StartCoroutine(OpenDialogAfterSeconds(5));
        }

        private IEnumerator<WaitForSeconds> OpenDialogAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            _dialogController.OpenOkayDialog("Your flight will begin boarding soon",
                "Flight 999 will begin boarding shortly. Please make your way to Gate A.",
                DialogController.DialogSize.Large);
        }
    }
}