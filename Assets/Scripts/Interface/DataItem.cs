using Microsoft.MixedReality.Toolkit.UX;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Interface
{
    public class DataItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private FontIconSelector icon;

        public string Label
        {
            get => label.text;
            set => label.SetText(value);
        }

        public string Icon
        {
            get => icon.CurrentIconName;
            set => icon.CurrentIconName = value;
        }

        public void ToggleIcon()
        {
            icon.gameObject.SetActive(!icon.gameObject.activeSelf);
        }

        public void ToggleIcon(bool state)
        {
            icon.gameObject.SetActive(state);
        }
    }
}