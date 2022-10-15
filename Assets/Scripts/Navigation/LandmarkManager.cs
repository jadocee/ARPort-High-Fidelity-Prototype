using System;
using System.Collections.Generic;
using Controller;
using Helpers;
using Interface.Anchors;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using Model;
using UnityEngine;

namespace Navigation
{
    public class LandmarkManager : MonoBehaviour
    {
        private const string SaveFilename = "SavedLandmarks.json";

        [SerializeField] private AnchorController anchorController;
        [SerializeField] private Transform markerContainer;
        [SerializeField] private GameObject menuContent;
        public GameObject looseAnchorPrefab;
        private readonly StorageController _storageController;
        private readonly List<Landmark> landmarks;


        // Member variables for getting keyboard input
        private string currentName;
        private int currentType;
        private GameObject marker;

        public LandmarkManager()
        {
            landmarks = new List<Landmark>();
            _storageController = new StorageController();
            currentName = "";
            currentType = -1;
        }

        private void Awake()
        {
            LoadLandmarks();
        }

        private void OnEnable()
        {
            gameObject.transform.position = Vector3.zero;
            menuContent.SetActive(false);
            // TODO find landmarks from previous sessions
        }

        private void OnDisable()
        {
            if (marker == null) return;
            Destroy(marker);
            marker = null;
        }

        private void OnDestroy()
        {
            if (marker != null) Destroy(marker);
        }

        public void SaveLandmarks()
        {
            if (landmarks.Count == 0)
            {
                Debug.Log("No landmarks to save.");
                return;
            }

            var array = landmarks.ToArray();
            var json = JsonHelper.ToJson(landmarks.ToArray(), true);
            if (json != null) _storageController.SaveToDisk(SaveFilename, json);
        }

        private void LoadLandmarks()
        {
            try
            {
                var json = _storageController.ReadFromDisk(SaveFilename);
                if (json == null)
                {
                    Debug.Log("No landmarks were loaded.");
                    return;
                }

                var loadedLandmarks = JsonHelper.FromJson<Landmark>(json);
                if (loadedLandmarks == null)
                {
                    Debug.Log("Failed to load landmarks.");
                    return;
                }

                landmarks.AddRange(loadedLandmarks);
                Debug.Log($"Loaded {loadedLandmarks.Length} landmarks.");
            }
            catch (NullReferenceException e)
            {
                Debug.Log(e.StackTrace);
            }
        }

        // Events
        public static event Action LandmarkAddedEvent;

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

        public List<Landmark> GetLandmarks()
        {
            return landmarks;
        }

        public List<Landmark> GetLandmarksByType(Landmark.LandmarkTypes types)
        {
            var filteredList = new List<Landmark>();
            foreach (var landmark in landmarks)
                if (landmark.GetLandmarkType() == types)
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

        public bool TryFindClosestLandmark(Vector3 position, out Landmark closest)
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
                ResetMarker();
            else
                marker = Instantiate(looseAnchorPrefab, markerContainer.transform.position,
                    markerContainer.transform.rotation);
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
            anchorController.AnchorStoreClear();
            anchorController.ClearSceneAnchors();
            landmarks.Clear();
            ResetMarker();
            // anchorController.RemoveAllAnchors();
            // landmarks.Clear();
            // ResetMarker();
        }

        public Landmark GetLandmarkByName(string landmarkName)
        {
            if (landmarks.Count == 0) return null;
            foreach (var landmark in landmarks)
                if (landmark.GetLandmarkName().Equals(landmarkName))
                    return landmark;
            return null;
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
            var anchor = anchorController.AddAnchor(pose, currentName);
            if (anchor != null)
            {
                anchorController.ToggleAnchorPersistence(anchor);
                var type = (Landmark.LandmarkTypes) currentType;
                var landmark = new Landmark(anchor, type);
                // var landmark = anchor.gameObject.AddComponent<Landmark>();
                // landmark.SetType(types);
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