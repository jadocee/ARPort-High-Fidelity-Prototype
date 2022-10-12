using System;
using System.Security.Cryptography;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using TMPro;
using UnityEngine;

namespace Navigation.Interface
{
    public class NavigationMenu : MonoBehaviour
    {
        [SerializeField] private LandmarkManager landmarkManager;
        [SerializeField] private Transform buttonContainer;
        public GameObject buttonPrefab;
        [SerializeField] private GameObject menuContent;

        // input variables
        private int tabIndex;
        private int prevTabIndex;

        public NavigationMenu()
        {
            prevTabIndex = tabIndex = -1;
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

        private void Awake()
        {
            EventsManager.LocationSelectEvent += Close;
            menuContent.SetActive(false);
        }

        private void OnEnable()
        {
            gameObject.transform.position = Vector3.zero;
            menuContent.SetActive(false);
        }

        private void Start()
        {
            if (tabIndex > -1 && tabIndex != prevTabIndex) DisplayLandmarks();
        }

        public void SetTabIndex(int tabIndex)
        {
            this.tabIndex = tabIndex;
            if (tabIndex > -1 && tabIndex != prevTabIndex) DisplayLandmarks();
        }

        private void DisplayLandmarks()
        {
            foreach (Transform child in buttonContainer.transform)
            {
                Destroy(child.gameObject);
            }
            
            var landmarks = landmarkManager.GetLandmarksByType((Landmark.LandmarkType) tabIndex);
            prevTabIndex = tabIndex;
            foreach (var landmark in landmarks)
            {
                var landmarkButton = Instantiate(buttonPrefab, buttonContainer, false);
                var selectLocationTrigger = landmarkButton.GetComponent<SelectLocationTrigger>();
                selectLocationTrigger.SetLandmarkId(landmark.GetId());
                selectLocationTrigger.SetLabelText(landmark.GetLandmarkName());
            }
        }

        // TODO remove param
        private void Close(Guid id)
        {
            menuContent.SetActive(false);
            menuContent.GetComponent<RadialView>().enabled = false;
        }
    }
}