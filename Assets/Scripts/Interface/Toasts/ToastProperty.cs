using System;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UX;

namespace Interface.Toasts
{
    public class ToastProperty
    {
        public ToastType Type { get; private set; }
        public string Message { get; private set; }
        public string IconName { get; private set; }

        public enum ToastType
        {
            Error,
            Warning,
            Info
        }

        private void SetDefaults()
        {
            switch (Type) 
            {
                case ToastType.Error:
                    Message = "Error";
                    IconName = "Icon 80";
                    break;
                case ToastType.Warning:
                    Message = "Warning";
                    IconName = "Icon 140";
                    break;
                case ToastType.Info:
                    Message = "Info";
                    IconName = "Icon 97";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ToastProperty(ToastType toastType, string message = null)
        {
            Type = toastType;
            SetDefaults();
            if (message != null) Message = message;
        }
    }
}