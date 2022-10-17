using System.Collections.Generic;
using Controller;
using Events;
using Interface.Landmarks;
using Model;
using UnityEngine;

namespace Interface.Navigation
{
    public class NavigationMenu : ARPortMenu
    {
        [SerializeField] private LandmarkController landmarkController;
        [SerializeField] private NavigationController navigationController;
        [SerializeField] private Transform buttonContainer;

        public GameObject buttonPrefab;

        // [SerializeField] private GameObject menuContent;
        private int prevTabIndex;

        // input variables
        private int tabIndex;

        public NavigationMenu()
        {
            prevTabIndex = tabIndex = -1;
        }

        private void Awake()
        {
            StartCoroutine(WaitForEventSystem());
        }

        private void Start()
        {
            if (tabIndex > -1 && tabIndex != prevTabIndex) DisplayLandmarks();
        }

        protected void OnEnable()
        {
            HideMenu();
        }

        private IEnumerator<WaitUntil> WaitForEventSystem()
        {
            yield return new WaitUntil(() => EventSystem.IsInitialized);
            EventSystem.Instance.NavigationEvent += args =>
            {
                if (!args.NavigationState.State.Equals(NavigationEventArgs.EventState.Start)) return;
                HideMenu();
            };
        }

        public void SetTabIndex(int tabIndex)
        {
            this.tabIndex = tabIndex;
            if (tabIndex > -1 && tabIndex != prevTabIndex) DisplayLandmarks();
        }

        private void DisplayLandmarks()
        {
            foreach (Transform child in buttonContainer.transform) Destroy(child.gameObject);

            var landmarks = landmarkController.FilterLandmarks((Landmark.LandmarkTypes) tabIndex);
            prevTabIndex = tabIndex;
            foreach (var landmark in landmarks)
            {
                var landmarkButton = Instantiate(buttonPrefab, buttonContainer, false);
                var selectLocationTrigger = landmarkButton.GetComponent<LandmarkButton>();
                selectLocationTrigger.SetLandmarkId(landmark.GetId());
                selectLocationTrigger.SetLabelText(landmark.GetLandmarkName());
            }
        }
    }
}