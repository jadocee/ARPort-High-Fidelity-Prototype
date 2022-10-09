using System;
using System.Collections.Generic;
using UnityEngine;

namespace Navigation
{
    public class NavigationSystem : MonoBehaviour
    {
        public GameObject arrowPrefab;
        [SerializeField] private LandmarkManager landmarkManager;
        private Path currentPath;
        private readonly List<Path> paths;

        public NavigationSystem()
        {
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

        private void CreatePath(Landmark start, Landmark finish)
        {
            Path path = new Path();
        }

        private void StartNavigation(Guid landmarkId)
        {
            // TODO get nearest landmark

            // TODO algorithm to find path (maybe DFS?)


            var mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            if (landmarkManager.TryFindClosestLandmark(mainCamera.transform.position, out Landmark landmark))
            {
                
            }

            try
            {
                var targetLandmark = landmarkManager.GetLandmarkById(landmarkId);
                // TODO implement path finding
                var arrow = Instantiate(arrowPrefab, null, false);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}