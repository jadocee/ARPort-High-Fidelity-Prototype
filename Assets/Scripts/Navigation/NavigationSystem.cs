using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Navigation.Interface;
using UnityEngine;

namespace Navigation
{
    public class NavigationSystem : MonoBehaviour
    {
        [SerializeField] private LandmarkManager landmarkManager;

        public GameObject arrowPrefab;

        // public GameObject navStatePrefab;
        private DirectionIndicator _arrow;
        private readonly List<Path> _paths;
        private Path _currentPath;


        // Member variables for getting keyboard input
        // TODO create a new input field in Unity for the path name
        // TODO make private
        private string nameInput;
        private int selectedPathIndex;

        public static event Action<Landmark> StartNavigationEvent;
        public static event Action EndNavigationEvent;
        public static event Action<Landmark> StateChangeEvent;

        public NavigationSystem()
        {
            _arrow = null;
            selectedPathIndex = -1;
            nameInput = "";
            landmarkManager = null;
            _paths = new List<Path>();
            _currentPath = null;
        }

        private void Awake()
        {
            // EventsManager.LocationSelectEvent += StartNavigation;
            // EventsManager.ArrivedAtLandmarkEvent += NextWaypoint;
            NavigationState.CancelNavigationEvent += CancelNavigation;
        }

        public void SetNameInput(string nameInput)
        {
            this.nameInput = nameInput;
        }

        public void SetSelectedPathIndex(int selectedPathIndex)
        {
            this.selectedPathIndex = selectedPathIndex;
        }

        public List<Path> GetPaths()
        {
            return _paths;
        }

        public void CreatePath()
        {
            if (nameInput.Length == 0)
            {
                Debug.Log("Require a name to create a path");
                return;
            }

            var path = new Path(nameInput);
            _paths.Add(path);
            _currentPath = path;
        }

        public void AddWaypoint()
        {
            if (nameInput.Length == 0 || selectedPathIndex < 0) return;

            _currentPath = _paths[selectedPathIndex];
            try
            {
                // Request recently created landmark
                var landmark = landmarkManager.GetLandmark(-1);
                _currentPath.AppendWaypoint(landmark);
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.Log($"Failed to add waypoint to {_currentPath.GetPathName()}; {e}");
                return;
            }

            Debug.Log($"Created waypoint for {_currentPath.GetPathName()}");
        }

        private void FindStart()
        {
        }

        private Path CreatePath(Landmark start, Landmark finish)
        {
            var path = new Path();
            path.AppendWaypoint(start);
            // TODO find landmarks leading to finish
            // TODO algorithm to find path (maybe DFS?)
            path.AppendWaypoint(finish);
            return path;
        }

        private void NextWaypoint(Landmark landmark)
        {
            if (!_currentPath.IsEnd() && _arrow != null)
            {
                _arrow.SetTarget(_currentPath.Next().GetLandmark().GetAnchor().transform);
            }
            else
            {
                Debug.Log("Arrived at destination");
                if (_arrow != null) Destroy(_arrow.gameObject);
            }
        }

        public void StartNavigation(Guid landmarkId)
        {
            // TODO get nearest landmark
            // var mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            // if (!landmarkManager.TryFindClosestLandmark(mainCamera.transform.position, out var closestLandmark))
            // return;
            var targetLandmark = landmarkManager.GetLandmarkById(landmarkId);
            // For now, this just creates a path between the closest and target
            // var path = CreatePath(closestLandmark, targetLandmark);
            // currentPath = path;
            if (targetLandmark == null)
            {
                Debug.Log("Failed to find target landmark");
                return;
            }

            StartNavigationEvent?.Invoke(targetLandmark);
            try
            {
                var newArrow = Instantiate(arrowPrefab, null, false);
                _arrow = newArrow.GetComponent<DirectionIndicator>();
                if (_arrow != null)
                {
                    _arrow.SetTarget(targetLandmark.GetAnchor().transform);
                    _arrow.gameObject.SetActive(true);
                }
                // arrow.SetTarget(path.Start().GetLandmark().GetAnchor().transform);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        private void CancelNavigation()
        {
            if (!_arrow) Destroy(_arrow.gameObject);
            EndNavigationEvent?.Invoke();
        }
    }
}