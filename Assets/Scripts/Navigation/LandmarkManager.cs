#nullable enable
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using SpatialAnchors;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;

namespace Navigation
{
    public class LandmarkManager : MonoBehaviour
    {
        private AnchorManager anchorManager;
        private GameObject marker;
        public GameObject markerContainer;
        private Landmark currentLandmark;
        private List<Landmark> landmarks;

        // Member variables for getting keyboard input
        public string currentName { get; set; }
        public int currentType { get; set; }

        public LandmarkManager()
        {
            landmarks = new List<Landmark>();
            currentName = "";
            currentType = -1;
        }

        private void OnEnable()
        {
            anchorManager = GameObject.FindGameObjectWithTag("MRTK XR Rig").GetComponent<AnchorManager>();
            // TODO find landmarks from previous sessions
        }

        private void Start()
        {
            SpawnMarker();
        }

        public List<Landmark> GetLandmarksByType(Landmark.LandmarkType type)
        {
            List<Landmark> filteredList = new List<Landmark>();
            foreach (Landmark landmark in landmarks)
            {
                if (landmark.GetType() == type)
                {
                    filteredList.Add(landmark);
                }
            }

            return filteredList;
        }

        public Landmark GetLandmark(int i)
        {
            if (i > landmarks.Count)
                throw new IndexOutOfRangeException(
                    $"Requested index {i} exceeds list size of {landmarks.Count}");
            return landmarks[i];
        }

        public Landmark GetLandmarkById(Guid guid)
        {
            foreach (Landmark landmark in landmarks)
            {
                if (landmark.GetId().Equals(guid)) return landmark;
            }

            throw new Exception($"Failed to find landmark with id {guid.ToString()}");
        }

        private void SpawnMarker()
        {
            if (marker == null)
            {
                marker = Instantiate(anchorManager.LooseAnchorPrefab, markerContainer.transform.position,
                    markerContainer.transform.rotation);
            }

            marker.GetComponent<LooseAnchorBehaviour>().Target = markerContainer;
        }

        private void ResetMarker()
        {
            if (marker != null)
            {
                marker.GetComponent<LooseAnchorBehaviour>().Target = markerContainer;
            }
            else
            {
                SpawnMarker();
            }
        }

        public void ClearLocalLandmarks()
        {
            // TODO separate method for calling AnchorStoreClear
            anchorManager.AnchorStoreClear();
            anchorManager.ClearSceneAnchors();
            ResetMarker();
        }

        public void Cancel()
        {
            if (marker == null) return;
            Destroy(marker);
            marker = null;
        }

        public void CreateLandmark()
        {
            if (currentName.Length == 0 || currentType < 0)
            {
                Debug.Log($"Failed to create landmark; values not set");
                return;
            }

            if (marker == null || !marker.TryGetComponent(out LooseAnchorBehaviour looseAnchorBehaviour) ||
                looseAnchorBehaviour.Target != null) return;
            Pose pose = new Pose(marker.transform.position, marker.transform.rotation);
            ARAnchor anchor = anchorManager.AddAnchor(pose);
            if (anchor != null)
            {
                anchorManager.ToggleAnchorPersistence(anchor, currentName);
                Landmark.LandmarkType type = (Landmark.LandmarkType) currentType;
                Landmark landmark = new Landmark(anchor, type);
                landmarks.Add(landmark);
                currentLandmark = landmark;
                Debug.Log($"Created landmark {currentName}");
            }
            else
            {
                Debug.Log($"Failed to create landmark {currentName}");
            }

            ResetMarker();
        }
    }
}