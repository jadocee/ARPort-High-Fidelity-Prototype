using System;

namespace Interface.Toasts
{
    public class ToastProperty
    {
        public enum ToastType
        {
            Error,
            Warning,
            Info
        }

        public ToastProperty(ToastType toastType, string message = null)
        {
            Type = toastType;
            SetDefaults();
            if (message != null) Message = message;
        }

        public ToastType Type { get; }
        public string Message { get; private set; }
        public string IconName { get; private set; }

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
    }
}