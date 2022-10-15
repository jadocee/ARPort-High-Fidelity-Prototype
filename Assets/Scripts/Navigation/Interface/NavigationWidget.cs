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
        
        private void Awake()
        {
            name = "NavState";
            // Set Listeners
            
            // OnSecondary(() => EventSystem.OnNavigationEvent(new NavigationEventArgs
            // {
            //     State = NavigationEventArgs.EventState.Cancel
            // }));
            IsInitialized = true;
        }

        public void InitListeners()
        {
            EventSystem.NavigationEvent += args =>
            {
                var state = args.NavigationState.State;
                if (state.Equals(NavigationEventArgs.EventState.Cancel) ||
                    state.Equals(NavigationEventArgs.EventState.Finish))
                {
                    Destroy(this);
                }
                else if (state.Equals(NavigationEventArgs.EventState.Start) ||
                         state.Equals(NavigationEventArgs.EventState.Update))
                {
                    SetTitle($"Travelling to {args.LocationData.TargetLocation}");
                    SetDesc($"You will arrive in <color=green><b>{args.RemainingTime:0}</b> minutes</color>" +
                            $"\n<color=green><b>{args.RemainingDistance}</b> km</color> to go");
                }
            };
        }

        public void SecondaryAction()
        {
            EventSystem.OnNavigationEvent(new NavigationEventArgs()
            {
                NavigationState = new NavigationState()
                {
                    State = NavigationEventArgs.EventState.Cancel,
                    StateVisualizer = this
                }
            });
        }
    }
}