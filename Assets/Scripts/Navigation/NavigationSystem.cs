using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace Navigation
{
    public class NavigationSystem : MonoBehaviour
    {
        public GameObject arrowPrefab;
        [SerializeField] private LandmarkManager landmarkManager;
        private List<Path> paths;
        private Path currentPath;


        // Member variables for getting keyboard input
        // TODO create a new input field in Unity for the path name
        // TODO make private
        public string nameInput { get; set; }
        public int selectedPathIndex { get; set; }

        public NavigationSystem()
        {
            selectedPathIndex = -1;
            nameInput = "";
            landmarkManager = null;
            paths = new List<Path>();
            currentPath = null;
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
                Debug.Log($"Require a name to create a path");
                return;
            }

            ;
            Path path = new Path(nameInput);
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
                Landmark landmark = landmarkManager.GetLandmark(-1);
                currentPath.AppendWaypoint(landmark);
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.Log($"Failed to add waypoint to {currentPath.GetPathName()}; {e}");
                return;
            }

            Debug.Log($"Created waypoint for {currentPath.GetPathName()}");
        }

        private void StartNavigation(Guid landmarkId)
        {
            try
            {
                Landmark targetLandmark = landmarkManager.GetLandmarkById(landmarkId);
                // TODO implement path finding
                GameObject arrow = Instantiate(arrowPrefab, null, false);
                
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return;
            }
        }
    }
}