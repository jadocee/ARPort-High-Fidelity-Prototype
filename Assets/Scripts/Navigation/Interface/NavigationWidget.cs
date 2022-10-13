using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Transactions;
using Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Widgets;

namespace Navigation.Interface
{
    public class NavigationWidget : Widget
    {
        public bool IsInitialized { get; private set; }
        
        private void OnEnable()
        {
            name = "NavState";
            // Set Listeners
            EventSystem.NavigationEvent += args =>
            {
                if (args.State.Equals(NavigationEventArgs.EventState.Cancel) ||
                    args.State.Equals(NavigationEventArgs.EventState.Finish))
                {
                    Destroy(this);
                }
                else if (args.State.Equals(NavigationEventArgs.EventState.Start) ||
                         args.State.Equals(NavigationEventArgs.EventState.Update))
                {
                    SetTitle($"Travelling to {args.TargetLocation}");
                    SetDesc($"You will arrive in <color=green><b>{args.RemainingTime:0}</b> minutes</color>" +
                            $"\n<color=green><b>{args.RemainingDistance}</b> km</color> to go");
                }
            };
            OnSecondary(() => EventSystem.OnNavigationEvent(new NavigationEventArgs
            {
                State = NavigationEventArgs.EventState.Cancel
            }));
            IsInitialized = true;
        }
    }
}