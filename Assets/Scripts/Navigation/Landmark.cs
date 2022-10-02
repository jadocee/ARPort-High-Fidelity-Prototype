using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Navigation
{
    public class Landmark
    {
        private const string Sid = "3f72497b-188f-4d3a-92a1-c7432cfae62a";
        private readonly Guid id;
        private readonly ARAnchor anchor;
        private readonly LandmarkType type;

        public Landmark(ARAnchor anchor, LandmarkType type)
        {
            id = new Guid(Sid);
            this.anchor = anchor;
            this.type = type;
        }

        public enum LandmarkType
        {
            Gate,
            Restaurant,
            Rental,
            Restroom
        }

        public Guid GetId()
        {
            return id;
        }

        public LandmarkType GetType()
        {
            return type;
        }

        public ARAnchor GetAnchor()
        {
            return anchor;
        }
    }
}