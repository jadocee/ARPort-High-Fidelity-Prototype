using Microsoft.MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Interface.Widgets
{
    public class Widget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleMesh;
        [SerializeField] private TextMeshProUGUI descMesh;
        [SerializeField] private TextMeshProUGUI extraMesh;
        [SerializeField] private PressableButton primaryButton;
        [SerializeField] private PressableButton secondaryButton;
        private string desc = "";
        private string extra = "";
        private int priority;
        private string title = "";

        private void Start()
        {
            if (title.Length > 0) titleMesh.SetText(title);
            if (desc.Length > 0) descMesh.SetText(desc);
            if (extra.Length > 0)
            {
                extraMesh.SetText(extra);
                extraMesh.gameObject.SetActive(true);
            }
        }

        public int GetPriority()
        {
            return priority;
        }

        public void SetPriority(int priority)
        {
            this.priority = priority;
        }

        protected void SetTitle(string title)
        {
            this.title = title;
            titleMesh.SetText(title);
        }

        protected void SetDesc(string desc)
        {
            this.desc = desc;
            descMesh.SetText(desc);
        }

        protected void SetExtra(string extra)
        {
            this.extra = extra;
            extraMesh.SetText(extra);
            extraMesh.gameObject.SetActive(extra.Length > 0);
        }

        protected void SetSecondaryButton(string label, string iconName = "", bool hidden = true)
        {
            var icon = secondaryButton.transform.Find("Frontplate/AnimatedContent/Icon");
            if (!icon)
            {
                Debug.Log("Could not find Icon");
                return;
            }

            if (iconName.Length > 0)
            {
                var fontIcon = icon.Find("UIButtonFontIcon")?.gameObject.GetComponent<FontIconSelector>();
                if (fontIcon) fontIcon.CurrentIconName = iconName;
            }

            var iconLabel = icon.Find("Label")?.gameObject.GetComponent<TextMeshProUGUI>();
            if (iconLabel) iconLabel.SetText(label);
            secondaryButton.gameObject.SetActive(hidden);
        }

        protected void OnPrimary(UnityAction call)
        {
            primaryButton.OnClicked.AddListener(call);
        }

        protected void OnSecondary(UnityAction call)
        {
            secondaryButton.OnClicked.AddListener(call);
        }
    }
}