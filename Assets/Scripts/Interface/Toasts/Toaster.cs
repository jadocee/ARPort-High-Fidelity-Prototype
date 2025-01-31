﻿using UnityEngine;

namespace Interface.Toasts
{
    public class Toaster : MonoBehaviour
    {
        [SerializeField] private Toast toastPrefab;

        public void ToastError(string message, Transform transform = null)
        {
            var toast = Toast.Instantiate(toastPrefab, new ToastProperty(ToastProperty.ToastType.Error, message));
            if (transform)
                toast.transform.SetPositionAndRotation(transform.position + Vector3.back, transform.rotation);
        }

        public void ToastWarning(string message, Transform transform = null)
        {
            var toast = Toast.Instantiate(toastPrefab, new ToastProperty(ToastProperty.ToastType.Warning, message));
            if (transform)
                toast.transform.SetPositionAndRotation(transform.position + Vector3.back, transform.rotation);
        }

        public void ToastInfo(string message, Transform transform = null)
        {
            var toast = Toast.Instantiate(toastPrefab, new ToastProperty(ToastProperty.ToastType.Info, message));
            if (transform)
                toast.transform.SetPositionAndRotation(transform.position + Vector3.back, transform.rotation);
        }
    }
}