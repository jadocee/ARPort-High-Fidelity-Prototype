using System;
using System.Collections.Generic;
using Controller;
using Events;
using Microsoft.MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;

namespace Interface.Landmarks
{
    [RequireComponent(typeof(PressableButton))]
    public class LandmarkButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private TextMeshProUGUI icon;
        private Guid _landmarkId;

        private void Awake()
        {
            gameObject.GetComponent<PressableButton>()?.OnClicked
                      .AddListener(() =>
                      {
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
                                  onClosedCallback: property =>
                                  {
                                      switch (property.ResultContext.ButtonType)
                                      {
                                          case DialogButtonType.No:
                                              return;
                                          case DialogButtonType.Yes:
                                              InvokeNavigation();
                                              break;
                                      }
                                  });
                          }
                          else
                          {
                              InvokeNavigation();
                          }
                      });
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
                    TargetLocationId = _landmarkId
                }
            });
        }

        private IEnumerator<WaitUntil> WaitForEventSystem()
        {
            yield return new WaitUntil(() => EventSystem.IsInitialized);
        }

        public void SetLandmarkId(Guid landmarkId)
        {
            _landmarkId = landmarkId;
        }

        public void SetLabelText(string text)
        {
            label.text = text;
        }

        public void SetIcon(string iconName)
        {
            icon.GetComponent<FontIconSelector>().CurrentIconName = iconName;
        }
    }
}