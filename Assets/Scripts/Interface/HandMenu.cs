using Interface.Widgets;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using UnityEngine;

namespace Interface
{
    [RequireComponent(typeof(HandBounds))]
    [RequireComponent(typeof(HandConstraintPalmUp))]
    public class HandMenu : MonoBehaviour
    {
        [SerializeField] private Canvas content;
        [SerializeField] private WidgetPanel panel;

        private void Awake()
        {
            content.gameObject.SetActive(false);
        }

        public void ExitApp()
        {
            Application.Quit();
        }
    }
}