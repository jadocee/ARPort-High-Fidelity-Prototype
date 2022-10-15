﻿using System;
using Events;
using Microsoft.MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;

namespace Navigation.Interface
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
                      .AddListener(() => EventSystem.OnNavigationEvent(new NavigationEventArgs
                      {
                          NavigationState = new NavigationState()
                          {
                              State = NavigationEventArgs.EventState.Start,
                          },
                          LocationData = new LocationData()
                          {
                              TargetLocationId = _landmarkId
                          }
                      }));
        }
        
        public void SetLandmarkId(Guid landmarkId)
        {
            this._landmarkId = landmarkId;
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