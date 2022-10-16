using System;
using System.Collections.Generic;
using Controller;
using Events;
using UnityEngine;

namespace Interface.GroupTracking
{
    public class GroupTrackingMenu : ARPortMenu
    {
        private void Awake()
        {
            StartCoroutine(WaitForEventSystem());
        }

        private IEnumerator<WaitUntil> WaitForEventSystem()
        {
            yield return new WaitUntil((() => EventSystem.IsInitialized));
            EventSystem.Instance.NavigationEvent += args =>
            {
                if (!args.NavigationState.State.Equals(NavigationEventArgs.EventState.Start)) return;
                HideMenu();
            };
        }

        private void OnEnable()
        {
            HideMenu();
        }
    }
}