using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Transactions;
using Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using Widgets;

namespace Navigation.Interface
{
    public class NavigationWidget : Widget
    {
        private DistanceCalculator _distanceCalculator;
        private ARAnchor _target;
        
        private void Awake()
        {
            name = "NavState";
        }

        public void Init(DistanceCalculator distanceCalculator)
        {
            // Set distance calculator
            this._distanceCalculator = distanceCalculator;
            
            // Set listeners
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
                    _target = args.LocationData.Anchor;
                }
            };
        }

        private void Update()
        {
            if (!_target || !_distanceCalculator) return;
            var distance = _distanceCalculator.Measure(_target.transform);
            SetDesc($"You will arrive in <color=green><b>{1}</b> minutes</color>" +
                    $"\n<color=green><b>{distance:0}</b> m</color> to go");
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