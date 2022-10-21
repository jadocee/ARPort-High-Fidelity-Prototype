using System;
using System.Collections.Generic;
using Interface.Anchors;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using Model;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

namespace Controller
{
    public class LandmarkController : MonoBehaviour
    {
        private const string SaveFilename = "SavedLandmarks.json";

        [SerializeField] private AnchorController anchorController;
        [SerializeField] private Transform markerContainer;
        [SerializeField] private GameObject menuContent;
        public GameObject looseAnchorPrefab;
        private readonly List<Landmark> _landmarks;
        private readonly StorageController _storageController;


        // Member variables for getting keyboard input
        private string currentName;
        private int currentType;
        private GameObject marker;

        public LandmarkController()
        {
            _landmarks = new List<Landmark>();
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
            if (_landmarks.Count == 0)
            {
                Debug.Log("No landmarks to save.");
                return;
            }

            var jsonArray = new List<Dictionary<string, object>>();
            foreach (var landmark in _landmarks)
            {
                var dictionary = new Dictionary<string, object>
                {
                    {"landmarkId", landmark.LandmarkId},
                    {"landmarkName", landmark.LandmarkName},
                    {"landmarkType", landmark.LandmarkType},
                    {"landmarkAnchorId", landmark.LandmarkAnchorId}
                };
                jsonArray.Add(dictionary);
            }

            var jsonString = JsonConvert.SerializeObject(jsonArray, Formatting.Indented);
            if (jsonString.Length > 0) _storageController.SaveToDisk(SaveFilename, jsonString);
            else Debug.Log("Failed to save landmarks; JSON string is empty.");
        }

        private void LoadLandmarks()
        {
            try
            {
                var jsonString = _storageController.ReadFromDisk(SaveFilename);
                if (jsonString.Length == 0)
                {
                    Debug.Log("No landmarks were loaded.");
                    return;
                }

                var json =
                    JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonString);
                if (json == null)
                {
                    Debug.LogWarning($"Failed to convert JSON string {jsonString}");
                    return;
                }

                foreach (var dictionary in json)
                {
                    var anchorId = (string) dictionary["landmarkAnchorId"];
                    var anchor =
                        anchorController.FindAnchor(new TrackableId(anchorId));
                    if (anchor == null)
                    {
                        Debug.LogWarning($"Could not find ARAnchor {anchorId}");
                        continue;
                    }

                    var landmark = new Landmark(anchor, (Landmark.LandmarkTypes) (int) dictionary["landmarkType"])
                    {
                        LandmarkName = (string) dictionary["landmarkName"]
                    };
                    _landmarks.Add(landmark);
                }
            }
            catch (NullReferenceException e)
            {
                Debug.Log(e.StackTrace);
            }
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

        public List<Landmark> GetLandmarks()
        {
            return _landmarks;
        }

        public List<Landmark> GetLandmarksByType(Landmark.LandmarkTypes types)
        {
            var filteredList = new List<Landmark>();
            foreach (var landmark in _landmarks)
                if (landmark.GetLandmarkType() == types)
                    filteredList.Add(landmark);

            return filteredList;
        }

        public Landmark GetLandmark(int i)
        {
            if (i > _landmarks.Count)
                throw new IndexOutOfRangeException(
                    $"Requested index {i} exceeds list size of {_landmarks.Count}");
            return _landmarks[i];
        }

        public Landmark GetLandmarkById(Guid guid)
        {
            foreach (var landmark in _landmarks)
                if (landmark.GetId().Equals(guid))
                    return landmark;

            throw new Exception($"Failed to find landmark with id {guid.ToString()}");
        }

        public bool TryFindClosestLandmark(Vector3 position, out Landmark closest)
        {
            float dist;
            float closestDist = 0;
            closest = null;
            foreach (var landmark in _landmarks)
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
            _landmarks.Clear();
            ResetMarker();
        }

        public Landmark GetLandmarkByName(string landmarkName)
        {
            if (_landmarks.Count == 0) return null;
            foreach (var landmark in _landmarks)
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
                var landmark = new Landmark(anchor, type)
                {
                    LandmarkName = currentName
                };
                _landmarks.Add(landmark);
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