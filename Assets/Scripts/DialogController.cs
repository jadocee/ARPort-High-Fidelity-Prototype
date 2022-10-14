using System;
using Microsoft.MixedReality.Toolkit.UX;
using UnityEngine;
using UnityEngine.Events;


public class DialogController : MonoBehaviour
{
    [SerializeField] private Dialog dialogPrefab;
    [SerializeField] private Transform parent;

    public void DispatchOkayDialog(string title, string desc, Action<DialogProperty> callback)
    {
        Dialog newDialog = Dialog.InstantiateFromPrefab(dialogPrefab, new DialogProperty(title, desc, DialogButtonHelpers.OK), false, true);
        if (newDialog != null)
        {
            newDialog.OnClosed += callback;
            newDialog.transform.SetParent(parent, true);
        }
    }
}