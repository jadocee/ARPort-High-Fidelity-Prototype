using System;
using UnityEngine;

namespace Events
{
    public class EventSystem : MonoBehaviour
    {
        public static EventSystem Instance { get; private set; }
        public static bool IsInitialized { get; private set; }

        private void Awake()
        {
            IsInitialized = false;
            if (Instance != null && Instance != this) Destroy(this);
            Instance = this;
            IsInitialized = true;
        }

        public event Action<NavigationEventArgs> NavigationEvent;

        public void OnNavigationEvent(NavigationEventArgs args)
        {
            NavigationEvent?.Invoke(args);
        }
    }
}