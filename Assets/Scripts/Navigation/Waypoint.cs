using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;

namespace Navigation
{
    [Serializable]
    public class Waypoint
    {
        private readonly Guid id;
        private readonly Landmark landmark;
        private Waypoint prev;
        private Waypoint next;

        public Waypoint(Landmark landmark, Waypoint prev = null, Waypoint next = null)
        {
            id = new Guid();
            this.landmark = landmark;
            this.prev = prev;
            this.next = next;
        }

        public Landmark GetLandmark()
        {
            return landmark;
        }

        public Guid GetId()
        {
            return id;
        }

        public Waypoint GetNext()
        {
            return next;
        }

        public void SetNext(Waypoint next)
        {
            this.next = next;
        }

        public Waypoint GetPrev()
        {
            return prev;
        }

        public void SetPrev(Waypoint prev)
        {
            this.prev = prev;
        }
    }
}