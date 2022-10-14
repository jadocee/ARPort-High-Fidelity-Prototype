using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Navigation
{
    public class Landmark
    {
        public enum LandmarkType
        {
            Gate,
            Restaurant,
            Rental,
            Restroom
        }

        private const string Sid = "3f72497b-188f-4d3a-92a1-c7432cfae62a";


        private readonly ARAnchor anchor;
        private Guid id;
        private string landmarkName;
        private LandmarkType type;

        public Landmark(ARAnchor anchor, LandmarkType type)
        {
            id = new Guid(Sid);
            this.anchor = anchor;
            this.type = type;
        }

        public void SetLandmarkName(string landmarkName)
        {
            this.landmarkName = landmarkName;
        }

        public string GetLandmarkName()
        {
            return landmarkName;
        }

        public void SetType(LandmarkType type)
        {
            this.type = type;
        }

        public Guid GetId()
        {
            return id;
        }

        public LandmarkType GetLandmarkType()
        {
            return type;
        }

        public ARAnchor GetAnchor()
        {
            return anchor;
        }

        public override bool Equals(object other)
        {
            if (!(other is Landmark otherLandmark)) return false;

            return otherLandmark.id.Equals(id);
        }
    }
}