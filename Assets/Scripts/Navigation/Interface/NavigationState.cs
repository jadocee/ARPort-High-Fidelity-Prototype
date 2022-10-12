using System;
using Microsoft.MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;

namespace Navigation.Interface
{
    public class NavigationState : MonoBehaviour
    {
        private const string NavigatingToPrefix = "Travelling to ";
        [SerializeField] private TextMeshProUGUI navigatingTo;
        [SerializeField] private TextMeshProUGUI distanceRemaining;
        [SerializeField] private TextMeshProUGUI timeRemaining;
        [SerializeField] private PressableButton cancelButton;

        public static event Action CancelNavigationEvent; 

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void CancelNavigation()
        {
            // EventsManager.GetInstance().OnNavigationCancel();
            CancelNavigationEvent?.Invoke();
        }
        


        public void SetNavigatingTo(string locationName)
        {
            navigatingTo.SetText(NavigatingToPrefix + locationName);
        }

        public void SetTimeRemaining(float hours, float mins)
        {
            timeRemaining.SetText(
                $"You are expected to arrive in <align=\"center\"><color=green><size=4.15><b>{hours:0}</b></size> {(hours >= 1 ? "hours" : "hour")} <size=4.15><b>{mins:0}</b></size> {(mins >= 1 ? "minutes" : "minute")}</color></align>");
        }

        public void SetDistanceRemaining(double distance)
        {
            timeRemaining.SetText($"You have <color=green><size=4.15><b>{distance:0}</b></size> km</color> to go");
        }
    }
}