using System;
using System.Collections;
using Microsoft.MixedReality.GraphicsTools;
using Microsoft.MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;

namespace Interface.Toasts
{
    public class Toast : MonoBehaviour
    {
        [SerializeField] private Material errorMaterial;
        [SerializeField] private Material infoMaterial;
        [SerializeField] private Material warningMaterial;
        [SerializeField] private FontIconSelector fontIconSelector;
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private CanvasElementRoundedRect roundedRect;

        public static Toast Instantiate(Toast toastPrefab, ToastProperty toastProperty)
        {
            if (toastProperty == null) return null;
            var toastGameObject = Instantiate(toastPrefab.gameObject);
            var toast = toastGameObject.GetComponent<Toast>();
            toast.textMesh.SetText(toastProperty.Message);
            toast.fontIconSelector.CurrentIconName = toastProperty.IconName;
            toast.roundedRect.material = toastProperty.Type switch
            {
                ToastProperty.ToastType.Error => toast.errorMaterial,
                ToastProperty.ToastType.Warning => toast.warningMaterial,
                ToastProperty.ToastType.Info => toast.infoMaterial,
                _ => throw new ArgumentOutOfRangeException()
            };
            return toast;
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }
    }
}