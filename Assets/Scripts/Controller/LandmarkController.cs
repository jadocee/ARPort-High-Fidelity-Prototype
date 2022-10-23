using System;
using System.Collections;
using System.Collections.Generic;
using Interface.Anchors;
using Interface.Toasts;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using Model;
using Newtonsoft.Json;
using UnityEngine;

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

        // Member variables for getting keyboard input
        private string currentName;
        private int currentType;
        private GameObject marker;
        private Toaster toaster;

        public LandmarkController()
        {
            _landmarks = new List<Landmark>();
            currentName = "";
            currentType = -1;
        }

        private void Awake()
        {
            toaster = GameObject.FindGameObjectWithTag("DialogController")?.GetComponent<Toaster>();
        }

        private IEnumerator Start()
        {
            // TODO: find method that works; this doesn't always run when the scene is loaded, for now, a workaround will be implemented
            yield return StartCoroutine(WaitAndLoadLandmarks());
        }

        private void OnEnable()
        {
            gameObject.transform.position = Vector3.zero;
            menuContent.SetActive(false);
        }

        private void OnDisable()
        {
            if (marker == null) return;
            Destroy(marker);
            marker = null;
        }

        private IEnumerator<WaitUntil> WaitAndLoadLandmarks()
        {
            yield return new WaitUntil(() => anchorController.IsReady);
            if (_landmarks.Count == 0) LoadLandmarks();
        }

        // Called by the hand menu as a temporary solution for loading landmarks from JSON file
        public void Init()
        {
            if (_landmarks.Count > 0) return;
            LoadLandmarks();
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
                    {"landmarkAnchorId", landmark.LandmarkAnchorId},
                    {"landmarkAnchorName", landmark.LandmarkAnchorName}
                };
                jsonArray.Add(dictionary);
            }

            var jsonString = JsonConvert.SerializeObject(jsonArray, Formatting.Indented);
            if (jsonString.Length > 0) StorageController.SaveToDisk(SaveFilename, jsonString);
            else Debug.Log("Failed to save landmarks; JSON string is empty.");
        }

        public void LoadLandmarks()
        {
            try
            {
//                 const string jsonString = @"[
//   {
//     'landmarkId': '3644280d-6d9b-4052-9793-46ca76a427ac',
//     'landmarkName': 'McDonald\'s',
//     'landmarkType': 1,
//     'landmarkAnchorId': '452760E148B38DAB-F162BBFB73E6D5AA',
//     'landmarkAnchorName': 'anchor/9555'
//   },
//   {
//     'landmarkId': 'bcdf8693-9aed-4cd9-9036-e83360b8cb95',
//     'landmarkName': 'Gate A',
//     'landmarkType': 0,
//     'landmarkAnchorId': '49FD5FF223BBCB18-4ED9043044015B94',
//     'landmarkAnchorName': 'anchor/f29a'
//   },
//   {
//     'landmarkId': '0a6240f5-1f2b-4e74-af6a-0f82f1471365',
//     'landmarkName': 'Muffin Munch',
//     'landmarkType': 1,
//     'landmarkAnchorId': '417F5C9E30444B00-3A78EFF70F825C86',
//     'landmarkAnchorName': 'anchor/0f18'
//   },
//   {
//     'landmarkId': '0695dc46-0559-4cd0-abfd-59a922d56626',
//     'landmarkName': 'Oporto',
//     'landmarkType': 1,
//     'landmarkAnchorId': '4DA7B79225CF878F-9C8E7041256FF981',
//     'landmarkAnchorName': 'anchor/fc88'
//   },
//   {
//     'landmarkId': '69409f2e-7233-40d1-8482-183843d8943b',
//     'landmarkName': 'Gate C',
//     'landmarkType': 0,
//     'landmarkAnchorId': '4BEED9048F936C35-A79445D481195F9D',
//     'landmarkAnchorName': 'anchor/14de'
//   },
//   {
//     'landmarkId': '5d5528cf-3270-47b7-9a18-218ceac8524a',
//     'landmarkName': '2F Toilets',
//     'landmarkType': 3,
//     'landmarkAnchorId': '424EA71E5DE80F90-1708743636DE078D',
//     'landmarkAnchorName': 'anchor/321b'
//   },
//   {
//     'landmarkId': '1c5b6d5c-3afb-4386-9e39-22101b20b4e2',
//     'landmarkName': 'G9 Toilets',
//     'landmarkType': 3,
//     'landmarkAnchorId': '40D055BE5467B342-030819AE5F502CBA',
//     'landmarkAnchorName': 'anchor/6e20'
//   },
//   {
//     'landmarkId': 'e4743a9f-6d7c-4521-9360-a87a66c57cbf',
//     'landmarkName': 'King Coffee',
//     'landmarkType': 1,
//     'landmarkAnchorId': '444C31CBD84145D4-C94C5C2849BCC995',
//     'landmarkAnchorName': 'anchor/67db'
//   }
// ]";
                var jsonString = StorageController.ReadFromDisk(SaveFilename);
                if (jsonString.Length == 0)
                {
                    Debug.Log("No landmarks were loaded.");
                    toaster.ToastInfo("You have not saved any landmarks");
                    return;
                }

                var json =
                    JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonString);
                if (json == null)
                {
                    Debug.LogWarning($"Failed to convert JSON string {jsonString}");
                    toaster.ToastWarning("There may be something wrong with the save file");
                    return;
                }

                foreach (var dictionary in json)
                {
                    var landmarkId = (string) dictionary["landmarkId"];
                    if (GetLandmark(landmarkId) != null)
                    {
                        Debug.Log($"Landmark {landmarkId} was not loaded, it already exists.");
                        continue;
                    }

                    var landmarkName = (string) dictionary["landmarkName"];
                    var anchorId = (string) dictionary["landmarkAnchorId"];
                    var anchorName = (string) dictionary["landmarkAnchorName"];
                    var anchor = anchorController.FindAnchor(anchorName);
                    if (!anchor)
                    {
                        toaster.ToastError($"Could not load landmark {landmarkName}, it may be missing or corrupted");
                        Debug.LogWarning($"Failed to load landmark {landmarkName}; could not find ARAnchor {anchorId}");
                        continue;
                    }

                    // Haven't checked if can cast straight to int
                    var landmarkType = (long) dictionary["landmarkType"];
                    var landmarkTypeInt = (int) landmarkType;
                    var landmark = new Landmark(anchor, (Landmark.LandmarkTypes) landmarkTypeInt)
                    {
                        LandmarkName = landmarkName,
                        LandmarkId = landmarkId,
                        LandmarkAnchorName = anchorName
                    };
                    _landmarks.Add(landmark);
                }
            }
            catch (Exception e)
            {
                toaster.ToastError("Something went wrong when loading your landmarks. Please contact support.");
                Debug.LogWarning(e.StackTrace);
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

        public List<Landmark> FilterLandmarks(Landmark.LandmarkTypes type)
        {
            var filteredList = new List<Landmark>();
            foreach (var landmark in _landmarks)
                if (landmark.GetLandmarkType() == type)
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

        public Landmark GetLandmark(Guid landmarkId)
        {
            foreach (var landmark in _landmarks)
                if (landmark.GetId().Equals(landmarkId))
                    return landmark;
            return null;
        }

        public Landmark GetLandmark(string landmarkId)
        {
            var guid = Guid.Parse(landmarkId);
            return GetLandmark(guid);
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

        public void ResetMarker()
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
            StorageController.DeleteFileFromDisk(SaveFilename);
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
                    LandmarkName = currentName,
                    LandmarkAnchorName = anchor.GetComponent<PersistableAnchorVisuals>()?.Name
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