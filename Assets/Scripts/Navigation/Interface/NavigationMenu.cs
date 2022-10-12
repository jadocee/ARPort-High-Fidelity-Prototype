using System;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using UnityEngine;

namespace Navigation.Interface
{
    public class NavigationMenu : MonoBehaviour
    {
        [SerializeField] private LandmarkManager landmarkManager;
        [SerializeField] private NavigationSystem _navigationSystem;
        [SerializeField] private Transform buttonContainer;
        public GameObject buttonPrefab;
        [SerializeField] private GameObject menuContent;
        private int prevTabIndex;

        // input variables
        private int tabIndex;

        public NavigationMenu()
        {
            prevTabIndex = tabIndex = -1;
        }

        private void Awake()
        {
            // EventsManager.LocationSelectEvent += Close;
            menuContent.SetActive(false);
            LandmarkButton.LandmarkSelectEvent += OnLandmarkSelected;
        }

        private void Start()
        {
            if (tabIndex > -1 && tabIndex != prevTabIndex) DisplayLandmarks();
        }

        private void OnEnable()
        {
            gameObject.transform.position = Vector3.zero;
            menuContent.SetActive(false);
        }

        public void DisplayMenu()
        {
            menuContent.SetActive(true);
            gameObject.GetComponent<RadialView>().enabled = true;
        }

        public void CloseMenu()
        {
            menuContent.SetActive(false);
            gameObject.GetComponent<RadialView>().enabled = false;
        }

        public void SetTabIndex(int tabIndex)
        {
            this.tabIndex = tabIndex;
            if (tabIndex > -1 && tabIndex != prevTabIndex) DisplayLandmarks();
        }

        private void DisplayLandmarks()
        {
            foreach (Transform child in buttonContainer.transform) Destroy(child.gameObject);

            var landmarks = landmarkManager.GetLandmarksByType((Landmark.LandmarkType) tabIndex);
            prevTabIndex = tabIndex;
            foreach (var landmark in landmarks)
            {
                var landmarkButton = Instantiate(buttonPrefab, buttonContainer, false);
                var selectLocationTrigger = landmarkButton.GetComponent<LandmarkButton>();
                selectLocationTrigger.SetLandmarkId(landmark.GetId());
                selectLocationTrigger.SetLabelText(landmark.GetLandmarkName());
            }
        }

        private void OnLandmarkSelected(Guid landmarkId)
        {
            menuContent.SetActive(false);
            gameObject.GetComponent<RadialView>().enabled = false;
            _navigationSystem.StartNavigation(landmarkId);
        }

        // TODO remove param
        private void Close(Guid id)
        {
            menuContent.SetActive(false);
            gameObject.GetComponent<RadialView>().enabled = false;
        }
    }
}