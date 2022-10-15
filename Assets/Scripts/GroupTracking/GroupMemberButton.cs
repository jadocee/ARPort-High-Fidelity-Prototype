using System;
using TMPro;
using UnityEngine;
using Events;
using Navigation;
using ToastNotifications;
using Microsoft.MixedReality.Toolkit.UX;

namespace GroupTracking
{
    public class GroupMemberButton : MonoBehaviour
    {
        public static event Action<GroupMemberButton> OnMemberSelectedEvent;
    
        [SerializeField] private TextMeshProUGUI memberName;
        [SerializeField] private TextMeshProUGUI memberLocation;
        [SerializeField] private TextMeshProUGUI memberDistance;
        private GameObject checkArrow;
        private DistanceCalculator distanceCalculator;
        private Transform locationTransform;
        private Guid _landmarkId;

        private void OnEnable()
        {
            if (!distanceCalculator) Debug.Log("Missing distance calculator");
        }

        private void Update()
        {
            if (!locationTransform || !distanceCalculator) return;
            memberDistance.SetText($"{distanceCalculator.Measure(locationTransform):0.00} m");
        }

        public void SetDistanceCalculator(DistanceCalculator distanceCalculator)
        {
            this.distanceCalculator = distanceCalculator;
        }

        public Transform GetLocationTransform()
        {
            return locationTransform;
        }

        public string GetMemberName()
        {
            return memberName.text;
        }

        public void SetLocationTransform(Transform location)
        {
            locationTransform = location;
        }
    
        public void SetMemberName(string memberName)
        {
            this.memberName.SetText(memberName);
        }

        public void SetMemberLocation(string location)
        {
            this.memberLocation.SetText(location);
        }

        public void SetMemberDistance(float distance)
        {
            memberDistance.SetText($"{distance:0.00} m");
        }

        public void Track()
        {
            if (!locationTransform)
            {
                Debug.Log("Missing location transform");
                return;
            }

            //checkArrow = GameObject.Find("Arrow");
            if (NavigationSystem.IsRunning() == false)
            {
                EventSystem.OnNavigationEvent(new NavigationEventArgs
                {
                    NavigationState = new NavigationState()
                    {
                        State = NavigationEventArgs.EventState.Start,
                    },
                    LocationData = new LocationData()
                    {
                        // TODO - LocationID Anchor 
                        // TargetLocationId = _landmarkId
                    }
                });
            }
            else
            {
                var toaster = GameObject.FindGameObjectWithTag("DialogController");
                if (toaster != null)
                {
                    var toasterScript = toaster.GetComponent<DialogController>();
                    if (toasterScript != null)
                    {
                        toasterScript.OpenYesNoDialog("<size=0.08>Navigation is already in process</size>", "<size=0.06>The application is already assisting in navigation to a chosen destination.</size>\n\n<size=0.06><b>Would you like to end the current navigation and start this one?.</b></size>", DialogController.DialogSize.Large, callback: (property) =>
                        {
                            if (property.ResultContext.ButtonType.Equals(DialogButtonType.No))
                            {
                                Debug.Log("Dismissed");
                                Destroy(property.TargetDialog.gameObject);
                            }
                            if (property.ResultContext.ButtonType.Equals(DialogButtonType.Yes))
                            {
                                EventSystem.OnNavigationEvent(new NavigationEventArgs
                                {
                                    NavigationState = new NavigationState()
                                    {
                                        State = NavigationEventArgs.EventState.Start,
                                    },
                                    LocationData = new LocationData()
                                    {
                                        // TODO - LocationID Anchor 
                                        // TargetLocationId = _landmarkId
                                    }
                                });
                            }
                        });
                    }
                }
            }
        }

    }
}