using UnityEngine;
using static Interface.Toasts.Toast;

namespace Interface.Toasts
{
    public class Toaster : MonoBehaviour
    {
        [SerializeField] private Toast toastPrefab;
        
        public void ToastError(string message, Transform transform = null)
        {
            Toast toast = Toast.Instantiate(toastPrefab, new ToastProperty(ToastProperty.ToastType.Error, message));
            if (transform != null)
            {
                toast.transform.SetPositionAndRotation(transform.position + Vector3.back, transform.rotation);
            }
        }
        
        public void ToastWarning(string message, Transform transform = null)
        {
            Toast toast = Toast.Instantiate(toastPrefab, new ToastProperty(ToastProperty.ToastType.Warning, message));
            if (transform != null)
            {
                toast.transform.SetPositionAndRotation(transform.position + Vector3.back, transform.rotation);
            }
        }
        
        public void ToastInfo(string message, Transform transform = null)
        {
            Toast toast = Toast.Instantiate(toastPrefab, new ToastProperty(ToastProperty.ToastType.Info, message));
            if (transform != null)
            {
                toast.transform.SetPositionAndRotation(transform.position + Vector3.back, transform.rotation);
            }
        }
    }
}