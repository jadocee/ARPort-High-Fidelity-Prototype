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

        private Dialog GetDialogPrefab(DialogSize dialogSize)
        {
            var prefab = dialogSize switch
            {
                DialogSize.Large => dialogPrefabLarge,
                DialogSize.Medium => dialogPrefabMedium,
                DialogSize.Small => dialogPrefabSmall,
                _ => throw new ArgumentOutOfRangeException(nameof(dialogSize), dialogSize, null)
            };
            return prefab;
        }

        public void OpenOkayDialog(string title, string desc, DialogSize dialogSize = DialogSize.Medium,
                                   Action<DialogProperty> callback = null)
        {
            var dialogPrefab = GetDialogPrefab(dialogSize);
            var newDialog = Dialog.InstantiateFromPrefab(dialogPrefab,
                new DialogProperty(title, desc, DialogButtonHelpers.OK), true, true);
            if (newDialog != null)
                if (callback != null)
                    newDialog.OnClosed += callback;
        }

        public void OpenDialog(string title, string desc, DialogSize dialogSize = DialogSize.Medium)
        {
            var dialogPrefab = GetDialogPrefab(dialogSize);
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

        public Dialog OpenAndGetDialog(string title, string desc, DialogSize dialogSize = DialogSize.Medium)
        {
            var dialogPrefab = GetDialogPrefab(dialogSize);
            var newDialog = Dialog.InstantiateFromPrefab(dialogPrefab,
                new DialogProperty(title, desc, None), true, true);
            if (newDialog != null)
            {
                var buttonPar = newDialog.transform.Find("ButtonParent");
                if (buttonPar != null)
                    foreach (Transform child in buttonPar)
                        Destroy(child.gameObject);
            }

            return newDialog;
        }

        public void OpenYesNoDialog(string title, string desc, DialogSize dialogSize = DialogSize.Medium,
                                    Action<DialogProperty> onClosedCallback = null, Transform appearInFrontOf = null)
        {
            try
            {
                var dialogPrefab = GetDialogPrefab(dialogSize);
                var newDialog = Dialog.InstantiateFromPrefab(dialogPrefab,
                    new DialogProperty(title, desc, DialogButtonHelpers.YesNo), true, true);
                if (appearInFrontOf)
                    newDialog.transform.SetPositionAndRotation(appearInFrontOf.position - Vector3.back,
                        appearInFrontOf.rotation);
                if (newDialog == null) return;
                if (onClosedCallback != null)
                    newDialog.OnClosed += onClosedCallback;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.Log(e.StackTrace);
            }
        }
    }
}