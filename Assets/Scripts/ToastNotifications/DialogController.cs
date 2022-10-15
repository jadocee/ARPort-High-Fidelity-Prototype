﻿using System;
using Microsoft.MixedReality.Toolkit.UX;
using UnityEngine;

namespace ToastNotifications
{
    public class DialogController : MonoBehaviour
    {
        [SerializeField] private Dialog dialogPrefabLarge;
        [SerializeField] private Dialog dialogPrefabMedium;
        [SerializeField] private Dialog dialogPrefabSmall;

        public enum DialogSize
        {
            Small, Medium, Large
        }

        private void Awake()
        {

            if (!dialogPrefabLarge || !dialogPrefabMedium || !dialogPrefabSmall)
            {
                Debug.Log("Missing Dialog Prefab");
                return;
            }
        }

        public void OpenOkayDialog(string title, string desc, DialogSize dialogSize = DialogSize.Medium, Action<DialogProperty> callback = null)
        {
            Dialog dialogPrefab = dialogSize switch
            {
                DialogSize.Large => dialogPrefabLarge,
                DialogSize.Medium => dialogPrefabMedium,
                DialogSize.Small => dialogPrefabSmall,
                _ => throw new ArgumentOutOfRangeException(nameof(dialogSize), dialogSize, null)
            };
            var newDialog = Dialog.InstantiateFromPrefab(dialogPrefab,
                new DialogProperty(title, desc, DialogButtonHelpers.OK), true, true);
            if (newDialog != null)
            {
                if (callback != null) newDialog.OnClosed += callback;
            }
        }
    }
}