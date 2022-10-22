using System;
using Microsoft.MixedReality.Toolkit.UX;
using UnityEditor.Experimental;
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

        private int i = 1;
        private String newMessage;
        [SerializeField] private Dialog dialogPrefabLarge;
        [SerializeField] private Dialog dialogPrefabMedium;
        [SerializeField] private Dialog dialogPrefabSmall;

        private static DialogButtonContext[] None { get; } = {new DialogButtonContext()};

        private void Awake()
        {
            if (!dialogPrefabLarge || !dialogPrefabMedium || !dialogPrefabSmall) Debug.Log("Missing Dialog Prefab");
            
        }
        

        private void Start()
        {
            
            InvokeRepeating("alertMessage",5f,100000f);
            
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

        public void alertMessage()
        {
            OpenOkayDialog("Alert", "Attention Gate A is now boarding. Please make your way to the boarding area");
        }
        public void MakeDialog()
        {
            
            if (i < 5)
            {
                OpenOkayDialog("Alert", descriptionMaker(i));
                i++;
            }
            
        }

        public void MakeConfirm()
        {
            OpenYesNoDialog("Confirmation", "Please confirm if you agree to the terms of service");
        }

        private string descriptionMaker(int i)
        {
            
            if (i == 1)
            {
                newMessage = " This is the first alert to tell you about some warning";
            }
            if (i == 2)
            {
                newMessage = " This is the second alert ";
            }
            if (i == 3)
            {
                newMessage = " This is the third alert but its fine";
            }
            if (i == 4)
            {
                newMessage = " This is the fourth alert but Gate C is closed";
            }
            return newMessage;
        
        }
    }
}