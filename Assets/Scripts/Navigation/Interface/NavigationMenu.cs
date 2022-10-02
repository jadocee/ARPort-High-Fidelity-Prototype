using System;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;

namespace Navigation.Interface
{
    public class NavigationMenu : MonoBehaviour
    {
        private LandmarkManager landmarkManager;
        public GameObject buttonPrefab;
        public Transform buttonContainer;
        public GameObject menuCanvas;
        
        // input variables
        private int tabIndex;
        private int prevTabIndex;

        public NavigationMenu()
        {
            prevTabIndex = tabIndex = -1;
        }

        public void SetTabIndex(int tabIndex)
        {
            this.tabIndex = tabIndex;
            if (tabIndex > -1 && tabIndex != prevTabIndex) DisplayLandmarks();
        }

        private void DisplayLandmarks()
        {
            List<Landmark> landmarks = landmarkManager.GetLandmarksByType((Landmark.LandmarkType) tabIndex);
            prevTabIndex = tabIndex;
            foreach (Landmark landmark in landmarks)
            {
                GameObject landmarkButton = Instantiate(buttonPrefab, buttonContainer, false);
                landmarkButton.GetComponent<SelectLocationTrigger>().SetLandmarkId(landmark.GetId());
                Transform iconPar = landmarkButton.transform.Find("Frontplate/AnimatedContent/Icon");
                iconPar.Find("Label").GetComponent<TextMeshProUGUI>().text = landmark.GetAnchor().name;
            }
        }

        private void Awake()
        {
            EventsManager.LocationSelectEvent += Close;
            landmarkManager = GameObject.FindGameObjectWithTag("Landmark Manager").GetComponent<LandmarkManager>();
        }

        private void Start()
        {
            if (tabIndex > -1 && tabIndex != prevTabIndex) DisplayLandmarks();
        }

        // TODO remove param
        private void Close(Guid id)
        {
            menuCanvas.SetActive(false);
        }
    }
}