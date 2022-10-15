using System;
using Microsoft.MixedReality.Toolkit.UX;
using UnityEngine;

namespace Controller
{
    public class DialogController : MonoBehaviour
    {
        public enum DialogSize
        {
            Small,
            Medium,
            Large
        }

        [SerializeField] private Dialog dialogPrefabLarge;
        [SerializeField] private Dialog dialogPrefabMedium;
        [SerializeField] private Dialog dialogPrefabSmall;

        private static DialogButtonContext[] None { get; } = {new DialogButtonContext()};

        private void Awake()
        {
            if (!dialogPrefabLarge || !dialogPrefabMedium || !dialogPrefabSmall) Debug.Log("Missing Dialog Prefab");
        }

        public void OpenOkayDialog(string title, string desc, DialogSize dialogSize = DialogSize.Medium,
                                   Action<DialogProperty> callback = null)
        {
            var dialogPrefab = dialogSize switch
            {
                DialogSize.Large => dialogPrefabLarge,
                DialogSize.Medium => dialogPrefabMedium,
                DialogSize.Small => dialogPrefabSmall,
                _ => throw new ArgumentOutOfRangeException(nameof(dialogSize), dialogSize, null)
            };
            var newDialog = Dialog.InstantiateFromPrefab(dialogPrefab,
                new DialogProperty(title, desc, DialogButtonHelpers.OK), true, true);
            if (newDialog != null)
                if (callback != null)
                    newDialog.OnClosed += callback;
        }

        public void OpenDialog(string title, string desc, DialogSize dialogSize = DialogSize.Medium)
        {
            var dialogPrefab = dialogSize switch
            {
                DialogSize.Large => dialogPrefabLarge,
                DialogSize.Medium => dialogPrefabMedium,
                DialogSize.Small => dialogPrefabSmall,
                _ => throw new ArgumentOutOfRangeException(nameof(dialogSize), dialogSize, null)
            };
            var newDialog = Dialog.InstantiateFromPrefab(dialogPrefab,
                new DialogProperty(title, desc, None), true, true);
            if (newDialog != null)
            {
                var buttonPar = newDialog.transform.Find("ButtonParent");
                if (buttonPar != null)
                    foreach (Transform child in buttonPar)
                        Destroy(child.gameObject);
            }
        }
    }
}