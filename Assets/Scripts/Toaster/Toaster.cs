using System;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.UX;
using UnityEngine;

namespace Toaster
{
    public class Toaster : MonoBehaviour
    {
        [SerializeField] private Dialog dialogPrefab;

        public Dialog GetDialogPrefab()
        {
            return dialogPrefab;
        }

        public void SetDialogPrefab(Dialog dialogPrefab)
        {
            this.dialogPrefab = dialogPrefab;
        }
        
        public void OpenOkayDialog(string title, string desc, Action<DialogProperty> callback)
        {
            var newDialog = Dialog.InstantiateFromPrefab(dialogPrefab, new DialogProperty(title, desc, DialogButtonHelpers.OK), true, true);
            if (newDialog != null)
            {
                newDialog.OnClosed += callback;
            }
        }


    }
}