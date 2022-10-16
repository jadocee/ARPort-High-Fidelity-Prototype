using System;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Model
{
    [Serializable]
    public class Landmark
    {
        public enum LandmarkTypes
        {
            Gate,
            Restaurant,
            Rental,
            Restroom
        }

        [NonSerialized] private readonly ARAnchor _anchor;
        [NonSerialized] private TrackableId _anchorId;
        [NonSerialized] private string _anchorName;

        [NonSerialized] private Guid _id;
        [NonSerialized] private string _landmarkName;
        [NonSerialized] public LandmarkTypes _landmarkType;


        public Landmark()
        {
        }

        public Landmark(ARAnchor anchor, LandmarkTypes landmarkType)
        {
            _id = Guid.NewGuid();
            _anchor = anchor;
            _landmarkType = landmarkType;
            _anchorId = _anchor.trackableId;
            _anchorName = _anchor.name;
        }

        public string LandmarkId
        {
            get => _id.ToString();
            set => _id = Guid.Parse(value);
        }

        public string LandmarkAnchorName
        {
            get => _anchorName;
            set => _anchorName = value;
        }

        public string LandmarkName
        {
            get => _landmarkName;
            set => _landmarkName = value;
        }

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


        // public Landmark(SerializationInfo info, StreamingContext ctx)
        // {
        //     landmarkName = info.GetString("LandmarkName");
        //     id = Guid.Parse(info.GetString("LandmarkId"));
        //     landmarkType = (LandmarkTypes) info.GetInt32("LandmarkTypes");
        //     anchorId = new TrackableId(info.GetString("LandmarkAnchorId"));
        //     anchorName = info.GetString("LandmarkAnchorName");
        // }

        public void SetLandmarkName(string landmarkName)
        {
            _landmarkName = landmarkName;
        }

        public string GetLandmarkName()
        {
            return _landmarkName;
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

        // public void GetObjectData(SerializationInfo info, StreamingContext context)
        // {
        //     info.AddValue("LandmarkId", id.ToString(), typeof(string));
        //     info.AddValue("LandmarkName", landmarkName, typeof(string));
        //     info.AddValue("LandmarkAnchorId", anchor.trackableId.ToString(), typeof(string));
        //     info.AddValue("LandmarkAnchorName", anchor.name, typeof(string));
        //     info.AddValue("LandmarkTypes", (int) landmarkType, typeof(int));
        // }
    }
}