using System;
using TMPro;
using UnityEngine;

namespace GroupTracking
{
    public class GroupMemberButton : MonoBehaviour
    {
        public static event Action<GroupMemberButton> OnMemberSelectedEvent;
    
        [SerializeField] private TextMeshProUGUI memberName;
        [SerializeField] private TextMeshProUGUI memberLocation;
        [SerializeField] private TextMeshProUGUI memberDistance;
        private DistanceCalculator distanceCalculator;
        private Transform locationTransform;

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
            OnMemberSelectedEvent?.Invoke(this);
        }

    }
}