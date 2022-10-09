using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

namespace Navigation
{
    public class NavigationSystem : MonoBehaviour
    {
        public GameObject arrowPrefab;
        [CanBeNull] private DirectionIndicator arrow;
        [SerializeField] private LandmarkManager landmarkManager;
        private Path currentPath;
        private readonly List<Path> paths;

        public NavigationSystem()
        {
            arrow = null;
            selectedPathIndex = -1;
            nameInput = "";
            landmarkManager = null;
            paths = new List<Path>();
            currentPath = null;
        }


        // Member variables for getting keyboard input
        // TODO create a new input field in Unity for the path name
        // TODO make private
        private string nameInput;
        private int selectedPathIndex;

        public void SetNameInput(string nameInput)
        {
            this.nameInput = nameInput;
        }

        public void SetSelectedPathIndex(int selectedPathIndex)
        {
            this.selectedPathIndex = selectedPathIndex;
        }

        private void Awake()
        {
            EventsManager.LocationSelectEvent += StartNavigation;
            EventsManager.ArrivedAtLandmarkEvent += NextWaypoint;
        }

        public List<Path> GetPaths()
        {
            return paths;
        }

        public void CreatePath()
        {
            if (nameInput.Length == 0)
            {
                Debug.Log("Require a name to create a path");
                return;
            }

            ;
            var path = new Path(nameInput);
            paths.Add(path);
            currentPath = path;
        }

        public void AddWaypoint()
        {
            if (nameInput.Length == 0 || selectedPathIndex < 0) return;

            currentPath = paths[selectedPathIndex];
            try
            {
                // Request recently created landmark
                var landmark = landmarkManager.GetLandmark(-1);
                currentPath.AppendWaypoint(landmark);
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.Log($"Failed to add waypoint to {currentPath.GetPathName()}; {e}");
                return;
            }

            Debug.Log($"Created waypoint for {currentPath.GetPathName()}");
        }

        private void FindStart()
        {
        }

        private Path CreatePath(Landmark start, Landmark finish)
        {
            Path path = new Path();
            path.AppendWaypoint(start);
            // TODO find landmarks leading to finish
            // TODO algorithm to find path (maybe DFS?)
            path.AppendWaypoint(finish);
            return path;
        }

        private void NextWaypoint(Landmark landmark)
        {
            if (!currentPath.IsEnd() && arrow != null)
            {
                arrow.SetTarget(currentPath.Next().GetLandmark().GetAnchor().transform);
            }
            else
            {
                Debug.Log($"Arrived at destination");
                if (arrow != null) Destroy(arrow.gameObject);
            }
        }

        private void StartNavigation(Guid landmarkId)
        {
            // TODO get nearest landmark
            var mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            if (!landmarkManager.TryFindClosestLandmark(mainCamera.transform.position, out Landmark closestLandmark))
                return;
            var targetLandmark = landmarkManager.GetLandmarkById(landmarkId);
            // For now, this just creates a path between the closest and target
            // var path = CreatePath(closestLandmark, targetLandmark);
            // currentPath = path;
            try
            {
                var newArrow = Instantiate(arrowPrefab, null, false);
                arrow = newArrow.GetComponent<DirectionIndicator>();
                if (arrow != null)
                {
                    // arrow.SetTarget(path.Start().GetLandmark().GetAnchor().transform);
                    arrow.SetTarget(targetLandmark.GetAnchor().transform);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }






        }
    }
}