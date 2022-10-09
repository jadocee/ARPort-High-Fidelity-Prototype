using System;
using Microsoft.MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;

namespace Navigation.Interface
{
    public class SelectLocationTrigger : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private TextMeshProUGUI icon;
        private Guid landmarkId;

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