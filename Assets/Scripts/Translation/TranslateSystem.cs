using System;
using Microsoft.MixedReality.Toolkit.UX;
using UnityEditor;
using UnityEngine;

namespace Translation
{
    public class TranslateSystem : MonoBehaviour
    {
        [SerializeField] private DialogController dialogController;

        private void OnEnable()
        {
            if (!dialogController) return;
            dialogController.DispatchOkayDialog("Translation",
                "Translation has been turned on", (property =>
                {
                    if (property.ResultContext.ButtonType == DialogButtonType.OK)
                    {
                        Debug.Log("Dismissed dialog");
                    }
                    // TODO: callback
                }));
        }

        private void OnDisable()
        {
            if (!dialogController) return;
            dialogController.DispatchOkayDialog("Translation",
                "Translation has been turned off", (property =>
                {
                    if (property.ResultContext.ButtonType == DialogButtonType.OK)
                    {
                        Debug.Log("Dismissed dialog");
                    }
                    // TODO: callback
                }));
        }
    }
}