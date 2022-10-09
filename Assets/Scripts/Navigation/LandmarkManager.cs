#nullable enable
using System;
using System.Collections.Generic;
using SpatialAnchors;
using UnityEngine;

namespace Navigation
{
    public class LandmarkManager : MonoBehaviour
    {
        [SerializeField] private AnchorCreator anchorCreator;
        [SerializeField] private GameObject markerContainer;
        private readonly List<Landmark> landmarks;
        private Landmark currentLandmark;
        private GameObject? marker;

        // Member variables for getting keyboard input
        private string currentName;
        private int currentType;


        public LandmarkManager()
        {
            landmarks = new List<Landmark>();
            currentName = "";
            currentType = -1;
            marker = null;
        }

        private void Start()
        {
            SpawnMarker();
        }

        private void OnEnable()
        {
            // TODO find landmarks from previous sessions
        }

        public string GetCurrentName()
        {
            return currentName;
        }

        public int GetCurrentType()
        {
            return currentType;
        }

        public void SetCurrentName(string currentName)
        {
            this.currentName = currentName;
        }

        public void SetCurrentType(int currentType)
        {
            this.currentType = currentType;
        }

        public List<Landmark> GetLandmarksByType(Landmark.LandmarkType type)
        {
            var filteredList = new List<Landmark>();
            foreach (var landmark in landmarks)
                if (landmark.GetLandmarkType() == type)
                    filteredList.Add(landmark);

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
            foreach (var landmark in landmarks)
                if (landmark.GetId().Equals(guid))
                    return landmark;

            throw new Exception($"Failed to find landmark with id {guid.ToString()}");
        }

        public bool TryFindClosestLandmark(Vector3 position, out Landmark? closest)
        {
            float dist;
            float closestDist = 0;
            closest = null;
            foreach (var landmark in landmarks)
                if ((dist = Vector3.Distance(landmark.GetAnchor().transform.position, position)) < closestDist)
                    closestDist = dist;

            return closest != null;
        }

        private void SpawnMarker()
        {
            if (marker == null)
                marker = Instantiate(anchorCreator.GetLooseAnchorPrefab(), markerContainer.transform.position,
                    markerContainer.transform.rotation);

            marker.GetComponent<LooseAnchorBehaviour>().Target = markerContainer;
        }

        private void ResetMarker()
        {
            if (marker != null)
                marker.GetComponent<LooseAnchorBehaviour>().Target = markerContainer;
            else
                SpawnMarker();
        }

        public void ClearLocalLandmarks()
        {
            // TODO separate method for calling AnchorStoreClear
            anchorCreator.AnchorStoreClear();
            anchorCreator.ClearSceneAnchors();
            landmarks.Clear();
            ResetMarker();
            // anchorCreator.RemoveAllAnchors();
            // landmarks.Clear();
            // ResetMarker();
        }

        public void Cancel()
        {
            if (marker == null) return;
            Destroy(marker);
            marker = null;
        }

        private void OnDestroy()
        {
            if (marker != null)
            {
                Destroy(marker);
            }
        }

        private void OnDisable()
        {
            if (marker == null) return;
            Destroy(marker);
            marker = null;
        }

        public void CreateLandmark()
        {
            if (currentName.Length == 0 || currentType < 0)
            {
                Debug.Log("Failed to create landmark; values not set");
                return;
            }

            if (marker == null || !marker.TryGetComponent(out LooseAnchorBehaviour looseAnchorBehaviour) ||
                looseAnchorBehaviour.Target != null) return;

            var pose = new Pose(marker.transform.position,
                Quaternion.LookRotation(marker.transform.rotation * Vector3.forward, Vector3.up));
            var anchor = anchorCreator.AddAnchor(pose, currentName);
            if (anchor != null)
            {
                anchorCreator.ToggleAnchorPersistence(anchor);
                var type = (Landmark.LandmarkType) currentType;
                var landmark = new Landmark(anchor, type);
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