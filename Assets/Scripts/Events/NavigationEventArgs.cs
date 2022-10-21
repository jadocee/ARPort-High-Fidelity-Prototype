using System;
using Interface.Widgets;
using UnityEngine.XR.ARFoundation;

namespace Events
{
    public struct NavigationEventArgs
    {
        public enum EventState
        {
            Start,
            Cancel,
            Update,
            Finish
        }

        public NavigationState NavigationState { get; set; }

        public LocationData LocationData { get; set; }
        // public double RemainingDistance { get; set; }
        // public float RemainingTime { get; set; }
    }

    public struct NavigationState
    {
        public NavigationEventArgs.EventState State { get; set; }
        public NavigationWidget StateVisualizer { get; set; }
    }

    public struct LocationData
    {
        public Guid TargetLocationId { get; set; }
        public string TargetLocation { get; set; }
        public ARAnchor Anchor { get; set; }
    }
}