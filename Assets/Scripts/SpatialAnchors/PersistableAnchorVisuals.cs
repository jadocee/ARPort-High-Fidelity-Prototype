// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace SpatialAnchors
{
    /// <summary>
    ///     A component to be used in various anchor sample scenarios, providing visuals
    ///     to indicate this anchor's name, tracking state, and persistence status.
    /// </summary>
    [RequireComponent(typeof(ARAnchor))]
    public class PersistableAnchorVisuals : MonoBehaviour
    {
        [SerializeField] private TextMeshPro textMesh;
        [SerializeField] private MeshRenderer meshRenderer;

        [SerializeField] private Material persistentAnchorMaterial;
        [SerializeField] private Material transientAnchorMaterial;
        [SerializeField] private Material untrackedAnchorMaterial;
        private ARAnchor m_arAnchor;

        private string m_name = "";

        private bool m_persisted;

        private bool m_textChanged = true;

        private TrackingState m_trackingState;

        private PersistableAnchorVisuals()
        {
            // Initialize this before Awake() and Start() so other scripts can consistently overwrite it
            AnchorTextFormatter = DefaultAnchorTextFormatter;
        }

        /// <summary>
        ///     Whether this script should manage the TrackingState property.
        ///     This helps keep some sample scenarios more simple, at the cost of suboptimal performance.
        /// </summary>
        public bool ManageOwnTrackingState { get; set; } = false;

        /// <summary>
        ///     A function which will format the text on this anchor. Can be overridden for use in various samples.
        /// </summary>
        public Func<PersistableAnchorVisuals, string> AnchorTextFormatter { get; set; }

        public string Name
        {
            get => m_name;
            set
            {
                if (m_name == value) return;
                m_name = value;
                m_textChanged = true;
            }
        }

        public bool Persisted
        {
            get => m_persisted;
            set
            {
                if (m_persisted != value)
                {
                    m_persisted = value;
                    m_textChanged = true;
                    meshRenderer.material = m_trackingState == TrackingState.Tracking
                        ? m_persisted ? persistentAnchorMaterial : transientAnchorMaterial
                        : untrackedAnchorMaterial;
                }
            }
        }

        public TrackingState TrackingState
        {
            get => m_trackingState;
            set
            {
                if (m_trackingState == value) return;
                m_trackingState = value;
                m_textChanged = true;
                meshRenderer.material = m_trackingState == TrackingState.Tracking
                    ? m_persisted ? persistentAnchorMaterial : transientAnchorMaterial
                    : untrackedAnchorMaterial;
            }
        }

        private void Start()
        {
            m_arAnchor = GetComponent<ARAnchor>();
            TrackingState = m_arAnchor.trackingState;
        }

        private void Update()
        {
            if (ManageOwnTrackingState) TrackingState = m_arAnchor.trackingState;

            if (!m_textChanged || textMesh == null) return;
            var info = AnchorTextFormatter != null ? AnchorTextFormatter(this) : "";

            if (textMesh.text != info) textMesh.text = info;

            m_textChanged = false;
        }

        private string DefaultAnchorTextFormatter(PersistableAnchorVisuals visuals)
        {
            return
                $"{visuals.m_arAnchor.trackableId}\n{(visuals.Persisted ? $"Name: \"{visuals.Name}\", " : "")}Tracking State: {visuals.TrackingState}";
        }
    }
}