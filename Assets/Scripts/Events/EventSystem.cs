using System;
using Navigation;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class EventSystem : MonoBehaviour
    {
        public static event Action<NavigationEventArgs> NavigationEvent;
        // public static event Action<Landmark> NavigationEvent;

        public static void OnNavigationEvent(NavigationEventArgs args)
        {
            NavigationEvent?.Invoke(args);
        }
    }
}