using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.MixedReality.OpenXR;
using Microsoft.MixedReality.OpenXR.ARFoundation;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Debug = UnityEngine.Debug;

namespace SpatialAnchors
{
    [RequireComponent(typeof(ARAnchorManager))]
    [RequireComponent(typeof(ARRaycastManager))]
    public class AnchorCreator : MonoBehaviour
    {
        private ARAnchorManager anchorManager;
        private ARRaycastManager raycastManager;
        private ARSessionOrigin arSessionOrigin;
        [SerializeField] private GameObject prefab;

        private ARAnchor currentAnchor;

        private List<ARAnchor> anchors;
        private List<ARRaycastHit> hits;
        private XRAnchorStore anchorStore;
        private readonly Dictionary<TrackableId, string> incomingPersistedAnchors;

        public AnchorCreator()
        {
            currentAnchor = null;
            prefab = null;
            arSessionOrigin = null;
            anchorManager = null;
            anchors = new List<ARAnchor>();
            hits = new List<ARRaycastHit>();
            anchorStore = null;
            incomingPersistedAnchors = new Dictionary<TrackableId, string>();
        }

        protected async void OnEnable()
        {
            // anchorManager = GetComponent<ARAnchorManager>();
            arSessionOrigin = GetComponent<ARSessionOrigin>();
            if (!TryGetComponent(out anchorManager) || !anchorManager.enabled || anchorManager.subsystem == null)
            {
                Debug.Log(
                    "ARAnchorManager not enabled or available; sample anchor functionality will not be enabled.");
                return;
            }
            if (!TryGetComponent(out raycastManager))
            {
                Debug.Log("ARRaycastManager unavailable or not enabled");
            }

            anchorManager.anchorsChanged += AnchorsChanged;
            anchorStore = await anchorManager.LoadAnchorStoreAsync();
            if (anchorStore == null)
            {
                Debug.Log("XRAnchorStore not available, sample anchor persistence functionality will not be enabled.");
                return;
            }

            if (anchorManager.anchorPrefab == null) anchorManager.anchorPrefab = prefab;


            // TODO load anchor when HMD is in range
            foreach (var value in anchorStore.PersistedAnchorNames)
            {
                TrackableId trackableId = anchorStore.LoadAnchor(value);
                incomingPersistedAnchors.Add(trackableId, value);
            }
        }

        protected void OnDisable()
        {
            if (anchorManager == null) return;
            anchorManager.anchorsChanged -= AnchorsChanged;
            anchorStore = null;
            incomingPersistedAnchors.Clear();
        }

        public List<ARAnchor> GetAnchors()
        {
            return anchors;
        }

        private void AnchorsChanged(ARAnchorsChangedEventArgs eventArgs)
        {
            foreach (var added in eventArgs.added)
            {
                Debug.Log(
                    $"Anchor added from ARAnchorsChangedEvent: {added.trackableId}, OpenXR Handle: {added.GetOpenXRHandle()}");
                ProcessAddedAnchor(added);
            }

            foreach (var updated in eventArgs.updated)
                if (updated.TryGetComponent(out PersistableAnchorVisuals sampleAnchorVisuals))
                    sampleAnchorVisuals.TrackingState = updated.trackingState;

            foreach (var removed in eventArgs.removed)
            {
                Debug.Log($"Anchor removed: {removed.trackableId}");
                anchors.Remove(removed);
            }
        }

        private void ProcessAddedAnchor(ARAnchor anchor)
        {
            // If this anchor being added was requested from the anchor store, it is recognized here
            if (incomingPersistedAnchors.TryGetValue(anchor.trackableId, out var name))
            {
                if (anchor.TryGetComponent(out PersistableAnchorVisuals sampleAnchorVisuals))
                {
                    sampleAnchorVisuals.Name = name;
                    sampleAnchorVisuals.Persisted = true;
                    sampleAnchorVisuals.TrackingState = anchor.trackingState;
                }

                incomingPersistedAnchors.Remove(anchor.trackableId);
            }

            anchors.Add(anchor);
        }

        public ARAnchor AddAnchor(Pose pose, string anchorName)
        {
            var newAnchor = anchorManager.AddAnchor(pose);
            if (newAnchor != null)
            {
                Debug.Log($"Anchor created: {newAnchor.trackableId}");
            }
            else
            {
                Debug.Log("Anchor creation failed");
            }
            return newAnchor;
        }

        public void PersistAnchor(TrackableId trackableId, string anchorName)
        {
            if (anchorStore.PersistedAnchorNames.Contains(anchorName))
            {
                anchorStore.UnpersistAnchor(anchorName);
            }

            if (anchorStore.TryPersistAnchor(trackableId, anchorName))
            {
            }
        }

        public void ToggleAnchorPersistence(ARAnchor anchor)
        {
            if (anchorStore == null)
            {
                Debug.Log("Anchor Store was not available.");
                return;
            }

            var sampleAnchorVisuals = anchor.GetComponent<PersistableAnchorVisuals>();
            if (!sampleAnchorVisuals.Persisted)
            {
                var newName = $"anchor/{Guid.NewGuid().ToString().Substring(0, 4)}";

                var succeeded = anchorStore.TryPersistAnchor(anchor.trackableId, newName);
                if (!succeeded)
                {
                    Debug.Log($"Anchor could not be persisted: {anchor.trackableId}");
                    return;
                }

                ChangeAnchorVisuals(anchor, newName, true);
            }
            else
            {
                anchorStore.UnpersistAnchor(sampleAnchorVisuals.Name);
                ChangeAnchorVisuals(anchor, "", false);
            }
        }

        public void AnchorStoreClear()
        {
            foreach (var anchorName in anchorStore.PersistedAnchorNames)
            {
                anchorStore.UnpersistAnchor(anchorName);
            }

            // anchorStore.Clear();
            foreach (var anchor in anchors)
            {
                // Change visual for every anchor in the scene
                ChangeAnchorVisuals(anchor, "", false);
            }
            // anchorStore.Clear();
        }

        public void ClearSceneAnchors()
        {
            // Remove every anchor in the scene. This does not affect their persistence
            foreach (var anchor in anchors)
            {
                anchorManager.subsystem.TryRemoveAnchor(anchor.trackableId);
            }

            anchors.Clear();
        }

        public void RemoveAllAnchors()
        {
            anchorStore.Clear();
            foreach (ARAnchor anchor in anchors)
            {
                Debug.Log($"Destroying anchor {anchor.trackableId}");
                // ToggleAnchorPersistence(anchor);
                ChangeAnchorVisuals(anchor, "", false);
                anchorManager.RemoveAnchor(anchor);
            }
            anchors.Clear();
        }

        private void ChangeAnchorVisuals(ARAnchor anchor, string newName, bool isPersisted)
        {
            var sampleAnchorVisuals = anchor.GetComponent<PersistableAnchorVisuals>();
            Debug.Log(isPersisted
                ? $"Anchor {anchor.trackableId} with name {newName} persisted"
                : $"Anchor {anchor.trackableId} with name {sampleAnchorVisuals.Name} unpersisted");
            sampleAnchorVisuals.Name = newName;
            sampleAnchorVisuals.Persisted = isPersisted;
        }
    }
}