using System;
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
        private Guid landmarkId;

        private void Awake()
        {
            gameObject.GetComponent<PressableButton>()?.OnClicked
                      .AddListener(() => LandmarkSelectEvent?.Invoke(landmarkId));
        }

        public static event Action<Guid> LandmarkSelectEvent;

        public void SetLandmarkId(Guid landmarkId)
        {
            this.landmarkId = landmarkId;
        }

        public void SetLabelText(string text)
        {
            label.text = text;
        }

        public void SetIcon(string iconName)
        {
            icon.GetComponent<FontIconSelector>().CurrentIconName = iconName;
        }

        public void OnSelect()
        {
            EventsManager.GetInstance().OnLocationSelect(landmarkId);
        }
    }
}