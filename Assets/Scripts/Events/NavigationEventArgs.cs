using System;

namespace Events
{
    public class NavigationEventArgs
    {
        
        public enum EventState
        {
            Start, Cancel, Update, Finish
        }
        
        public EventState State { get; set; }
        public Guid TargetLocationId { get; set; }
        public string TargetLocation { get; set; }
        public double RemainingDistance { get; set; }
        public float RemainingTime { get; set; }
    }
}