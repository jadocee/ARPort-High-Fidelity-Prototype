using System;
using TMPro;
using UnityEngine;

namespace Navigation.Interface
{
    public class NavigationMenu : MonoBehaviour
    {
        public GameObject buttonPrefab;
        public Transform buttonContainer;
        public GameObject menuCanvas;
        private LandmarkManager landmarkManager;
        private int prevTabIndex;

        // input variables
        private int tabIndex;

        public NavigationMenu()
        {
            prevTabIndex = tabIndex = -1;
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

        public void SetTabIndex(int tabIndex)
        {
            this.tabIndex = tabIndex;
            if (tabIndex > -1 && tabIndex != prevTabIndex) DisplayLandmarks();
        }

        private void DisplayLandmarks()
        {
            var landmarks = landmarkManager.GetLandmarksByType((Landmark.LandmarkType) tabIndex);
            prevTabIndex = tabIndex;
            foreach (var landmark in landmarks)
            {
                var landmarkButton = Instantiate(buttonPrefab, buttonContainer, false);
                landmarkButton.GetComponent<SelectLocationTrigger>().SetLandmarkId(landmark.GetId());
                var iconPar = landmarkButton.transform.Find("Frontplate/AnimatedContent/Icon");
                iconPar.Find("Label").GetComponent<TextMeshProUGUI>().text = landmark.GetAnchor().name;
            }
        }

        // TODO remove param
        private void Close(Guid id)
        {
            menuCanvas.SetActive(false);
        }
    }
}