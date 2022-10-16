using System;
using System.Collections.Generic;
using Events;
using Helpers;
using Interface.Navigation;
using Interface.Widgets;
using Model;
using UnityEngine;

namespace Controller
{
    public class NavigationController : MonoBehaviour
    {
        [SerializeField] private LandmarkController landmarkController;
        [SerializeField] private GameObject navigationStatePrefab;
        [SerializeField] private Transform widgetParent;
        [SerializeField] private DistanceCalculator distanceCalculator;
        public GameObject arrowPrefab;
        private readonly List<Path> _paths;
        private DirectionIndicator _arrow;
        private Path _currentPath;
        private NavigationWidget _navStateVisualizer;

        // Member variables for getting keyboard input
        // TODO create a new input field in Unity for the path name
        // TODO make private
        private string nameInput;
        private int selectedPathIndex;

        public NavigationController()
        {
            selectedPathIndex = -1;
            nameInput = "";
            _paths = new List<Path>();
            _currentPath = null;
            IsRunning = false;
        }

        public bool IsRunning { get; private set; }


        private void OnEnable()
        {
            StartCoroutine(WaitForEventSystem());
        }

        private IEnumerator<WaitUntil> WaitForEventSystem()
        {
            yield return new WaitUntil(() => EventSystem.IsInitialized);
            // TODO: on finish
            EventSystem.Instance.NavigationEvent += args =>
            {
                switch (args.NavigationState.State)
                {
                    case NavigationEventArgs.EventState.Start:
                        InitStateVisualizer();
                        StartNavigation(args.LocationData.TargetLocationId);
                        break;
                    case NavigationEventArgs.EventState.Cancel:
                        if (_arrow) Destroy(_arrow.gameObject);
                        if (args.NavigationState.StateVisualizer)
                            Destroy(args.NavigationState.StateVisualizer.gameObject);
                        IsRunning = false;
                        break;
                    case NavigationEventArgs.EventState.Update:
                        break;
                    case NavigationEventArgs.EventState.Finish:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };
        }

        private void InitStateVisualizer()
        {
            if (_navStateVisualizer) Destroy(_navStateVisualizer.gameObject);
            // Create widget
            var widget = Instantiate(navigationStatePrefab.gameObject, widgetParent, false);
            if (widget != null)
            {
                _navStateVisualizer = widget.GetComponent<NavigationWidget>();
                _navStateVisualizer.Init(distanceCalculator);
            }
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
                var landmark = landmarkController.GetLandmark(-1);
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

        private void StartNavigation(Guid landmarkId)
        {
            if (IsRunning) CancelNavigation();

            IsRunning = true;
            // TODO get nearest landmark
            var targetLandmark = landmarkController.GetLandmarkById(landmarkId);
            // For now, this just creates a path between the closest and target
            // var path = CreatePath(closestLandmark, targetLandmark);
            // currentPath = path;
            if (targetLandmark == null)
            {
                Debug.Log("Failed to find target landmark");
                return;
            }

            try
            {
                var newArrow = Instantiate(arrowPrefab, null, false);
                newArrow.SetActive(false);
                _arrow = newArrow.GetComponent<DirectionIndicator>();
                if (_arrow != null)
                {
                    _arrow.SetTarget(targetLandmark.GetAnchor().transform);
                    _arrow.gameObject.SetActive(true);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
                CancelNavigation();
                return;
            }

            // Ping listeners
            // TODO move into NextWidget()
            // TODO calculate distance and remaining time
            EventSystem.Instance.OnNavigationEvent(new NavigationEventArgs
            {
                NavigationState = new NavigationState
                {
                    State = NavigationEventArgs.EventState.Update
                },
                LocationData = new LocationData
                {
                    TargetLocation = targetLandmark.GetLandmarkName(),
                    TargetLocationId = targetLandmark.GetId(),
                    Anchor = targetLandmark.GetAnchor()
                }
            });
        }

        private void CancelNavigation()
        {
            if (_arrow) Destroy(_arrow.gameObject);
            EventSystem.Instance.OnNavigationEvent(new NavigationEventArgs
            {
                NavigationState = new NavigationState
                {
                    State = NavigationEventArgs.EventState.Cancel
                }
            });
            IsRunning = false;
        }

        private void CancelNavigation(NavigationEventArgs args)
        {
            if (_arrow) Destroy(_arrow.gameObject);
            if (args.NavigationState.StateVisualizer)
                Destroy(args.NavigationState.StateVisualizer.gameObject);
            IsRunning = false;
        }
    }
}