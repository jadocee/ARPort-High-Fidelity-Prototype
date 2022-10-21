using System;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Model
{
    public class Landmark
    {
        public enum LandmarkTypes
        {
            Gate,
            Restaurant,
            Rental,
            Restroom
        }

        private readonly ARAnchor _anchor;
        private TrackableId _anchorId;
        private Guid _id;
        private LandmarkTypes _landmarkType;


        public Landmark()
        {
        }

        public Landmark(ARAnchor anchor, LandmarkTypes landmarkType)
        {
            _id = Guid.NewGuid();
            _anchor = anchor;
            _landmarkType = landmarkType;
            _anchorId = _anchor.trackableId;
        }

        public string LandmarkAnchorName { get; set; }

        public string LandmarkId
        {
            get => _id.ToString();
            set => _id = Guid.Parse(value);
        }

        public string LandmarkName { get; set; }

        public int LandmarkType
        {
            get => (int) _landmarkType;
            set => _landmarkType = (LandmarkTypes) value;
        }


        public string LandmarkAnchorId
        {
            get => _anchorId.ToString();
            set => _anchorId = new TrackableId(value);
        }

        public void SetLandmarkName(string landmarkName)
        {
            LandmarkName = landmarkName;
        }

        public string GetLandmarkName()
        {
            return LandmarkName;
        }

        public void SetType(LandmarkTypes types)
        {
            _landmarkType = types;
        }

        public Guid GetId()
        {
            return _id;
        }

        public LandmarkTypes GetLandmarkType()
        {
            return _landmarkType;
        }

        public ARAnchor GetAnchor()
        {
            return _anchor;
        }

        public override bool Equals(object other)
        {
            if (!(other is Landmark otherLandmark)) return false;

            return otherLandmark._id.Equals(_id);
        }
    }
}