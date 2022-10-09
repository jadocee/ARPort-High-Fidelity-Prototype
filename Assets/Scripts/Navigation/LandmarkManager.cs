using System;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using SpatialAnchors;
using UnityEngine;

namespace Navigation
{
    public class LandmarkManager : MonoBehaviour
    {
        [SerializeField] private AnchorCreator anchorCreator;
        [SerializeField] private Transform markerContainer;
        [SerializeField] private GameObject menuContent;
        public GameObject looseAnchorPrefab;
        private readonly List<Landmark> landmarks;
        private GameObject marker;

        // Member variables for getting keyboard input
        private string currentName;
        private int currentType;


        public LandmarkManager()
        {
            landmarks = new List<Landmark>();
            currentName = "";
            currentType = -1;
        }

        private void OnEnable()
        {
            gameObject.transform.position = Vector3.zero;
            menuContent.SetActive(false);
            // TODO find landmarks from previous sessions
        }

        public void DisplayMenu()
        {
            menuContent.SetActive(true);
            gameObject.GetComponent<RadialView>().enabled = true;
            SpawnMarker();
        }

        public void CloseMenu()
        {
            menuContent.SetActive(false);
            gameObject.GetComponent<RadialView>().enabled = false;
            if (marker == null) return;
            Destroy(marker);
            marker = null;
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
            if (marker != null)
            {
                ResetMarker();
            }
            else
            {
                marker = Instantiate(looseAnchorPrefab, markerContainer.transform.position,
                    markerContainer.transform.rotation);
            }
            marker.GetComponent<LooseAnchor>().SetTargetPosition(markerContainer);
        }

        private void ResetMarker()
        {
            if (marker != null)
                marker.GetComponent<LooseAnchor>().SetTargetPosition(markerContainer);
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

            if (marker == null || !marker.TryGetComponent(out LooseAnchor looseAnchorBehaviour) ||
                looseAnchorBehaviour.hasTarget()) return;

            var pose = new Pose(marker.transform.position,
                Quaternion.LookRotation(marker.transform.rotation * Vector3.forward, Vector3.up));
            var anchor = anchorCreator.AddAnchor(pose, currentName);
            if (anchor != null)
            {
                anchorCreator.ToggleAnchorPersistence(anchor);
                var type = (Landmark.LandmarkType) currentType;
                var landmark = new Landmark(anchor, type);
                // var landmark = anchor.gameObject.AddComponent<Landmark>();
                // landmark.SetType(type);
                landmark.SetLandmarkName(currentName);
                landmarks.Add(landmark);
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