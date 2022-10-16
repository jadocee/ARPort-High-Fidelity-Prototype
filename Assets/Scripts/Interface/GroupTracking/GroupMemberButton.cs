using Controller;
using Events;
using Helpers;
using Microsoft.MixedReality.Toolkit.UX;
using Model;
using TMPro;
using UnityEngine;

namespace Interface.GroupTracking
{
    public class GroupMemberButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI memberName;
        [SerializeField] private TextMeshProUGUI memberLocation;
        [SerializeField] private TextMeshProUGUI memberDistance;
        private DistanceCalculator _distanceCalculator;
        private Landmark _landmark;

        private void Update()
        {
            if (!_distanceCalculator || _landmark == null) return;
            memberDistance.SetText($"{_distanceCalculator.Measure(_landmark.GetAnchor().transform):0.00} m");
        }

        private void OnEnable()
        {
            if (!_distanceCalculator) Debug.Log("Missing distance calculator");
        }

        public void SetDistanceCalculator(DistanceCalculator distanceCalculator)
        {
            _distanceCalculator = distanceCalculator;
        }

        public string GetMemberName()
        {
            return memberName.text;
        }

        public void SetMemberName(string memberName)
        {
            this.memberName.SetText(memberName);
        }

        public void SetMemberLocation(Landmark landmark)
        {
            _landmark = landmark;
        }

        public void Track()
        {
            if (_landmark == null)
            {
                Debug.Log("Missing location transform");
                return;
            }

            // Check if navigation is available
            var navSystem = GameObject.FindGameObjectWithTag("NavSystem");
            if (!navSystem || !navSystem.TryGetComponent<NavigationController>(out var navController))
            {
                Debug.Log("NavigationSystem unavailable");
                return;
            }

            if (navController.IsRunning)
            {
                var dialogController = GameObject.FindGameObjectWithTag("DialogController");
                if (!dialogController ||
                    !dialogController.TryGetComponent<DialogController>(out var dialogControllerScript))
                {
                    Debug.Log("Could not find DialogController");
                    return;
                }

                dialogControllerScript.OpenYesNoDialog("Are you sure you want to continue?",
                    "You are already navigating to a location. Continuing will cancel the current navigation.",
                    onClosedCallback: (
                        property =>
                        {
                            switch (property.ResultContext.ButtonType)
                            {
                                case DialogButtonType.No:
                                    return;
                                case DialogButtonType.Yes:
                                    InvokeNavigation();
                                    break;
                            }
                        }));
            }
            else
            {
                InvokeNavigation();
            }
        }

        private void InvokeNavigation()
        {
            EventSystem.Instance.OnNavigationEvent(new NavigationEventArgs
            {
                NavigationState = new NavigationState
                {
                    State = NavigationEventArgs.EventState.Start
                },
                LocationData = new LocationData
                {
                    TargetLocationId = _landmark.GetId()
                }
            });
        }
    }
}