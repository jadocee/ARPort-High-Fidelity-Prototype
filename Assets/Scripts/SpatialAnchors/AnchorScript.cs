using System;
using System.Collections.Generic;
using Microsoft.MixedReality.OpenXR;
using Microsoft.MixedReality.OpenXR.ARFoundation;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace SpatialAnchors
{
    public class AnchorScript : MonoBehaviour
    {
        [SerializeField] private GameObject looseAnchor;
        [SerializeField] private GameObject anchorPrefab;
        private ARSessionOrigin arSessionOrigin;
        private ARAnchorManager anchorManager;
        private List<ARAnchor> anchors;
        private XRAnchorStore anchorStore;
        private Dictionary<TrackableId, string> incomingPersistedAnchors;
        
        

        public AnchorScript()
        {
            looseAnchor = null;
            arSessionOrigin = null;
            anchorManager = null;
            anchors = new List<ARAnchor>();
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
                    $"ARAnchorManager not enabled or available; sample anchor functionality will not be enabled.");
                return;
            }

            anchorManager.anchorsChanged += AnchorsChanged;
            anchorStore = await anchorManager.LoadAnchorStoreAsync();
            if (anchorStore == null)
            {
                Debug.Log("XRAnchorStore not available, sample anchor persistence functionality will not be enabled.");
                return;
            }

            if (anchorManager.anchorPrefab == null) anchorManager.anchorPrefab = anchorPrefab;

            foreach (var value in anchorStore.PersistedAnchorNames)
            {
                // ReSharper disable once SuggestVarOrType_SimpleTypes
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

        private void AnchorsChanged(ARAnchorsChangedEventArgs eventArgs)
        {
            foreach (var added in eventArgs.added)
            {
                Debug.Log(
                    $"Anchor added from ARAnchorsChangedEvent: {added.trackableId}, OpenXR Handle: {added.GetOpenXRHandle()}");
                ProcessAddedAnchor(added);
            }

            foreach (ARAnchor updated in eventArgs.updated)
            {
                if (updated.TryGetComponent(out PersistableAnchorVisuals sampleAnchorVisuals))
                {
                    sampleAnchorVisuals.TrackingState = updated.trackingState;
                }
            }

            foreach (var removed in eventArgs.removed)
            {
                Debug.Log($"Anchor removed: {removed.trackableId}");
                anchors.Remove(removed);
            }
        }

        private void ProcessAddedAnchor(ARAnchor anchor)
        {
            // If this anchor being added was requested from the anchor store, it is recognized here
            if (incomingPersistedAnchors.TryGetValue(anchor.trackableId, out string name))
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

        public void AddAnchor(Pose pose, string name)
        {
#pragma warning disable CS0618
            ARAnchor newAnchor = anchorManager.AddAnchor(pose);
#pragma warning restore CS0618
            if (newAnchor == null)
            {
                Debug.Log($"Anchor creation failed");
            }
            else
            {
                Debug.Log($"Anchor created: {newAnchor.trackableId}");
            }
            ToggleAnchorPersistence(newAnchor, name);
        }
        
        public void ToggleAnchorPersistence(ARAnchor anchor, string name)
        {
            if (anchorStore == null)
            {
                Debug.Log($"Anchor Store was not available.");
                return;
            }
            PersistableAnchorVisuals sampleAnchorVisuals = anchor.GetComponent<PersistableAnchorVisuals>();
            if (!sampleAnchorVisuals.Persisted)
            {
                if (!anchorStore.TryPersistAnchor(anchor.trackableId, name))
                {
                    Debug.Log($"Anchor could not be persisted: {anchor.trackableId}");
                    return;
                }
                ChangeAnchorVisuals(anchor, name, true);
            }
            else
            {
                anchorStore.UnpersistAnchor(sampleAnchorVisuals.Name);
                ChangeAnchorVisuals(anchor, "", false);
            }
        }
        
        public void AnchorStoreClear()
        {
            anchorStore.Clear();
            // Change visual for every anchor in the scene
            foreach (ARAnchor anchor in anchors)
            {
                ChangeAnchorVisuals(anchor, "", false);
            }
        }
        
        public void ClearSceneAnchors()
        {
            // Remove every anchor in the scene. This does not affect their persistence
            foreach (ARAnchor anchor in anchors)
            {
                anchorManager.subsystem.TryRemoveAnchor(anchor.trackableId);
            }

            anchors.Clear();
        }
        
        private void ChangeAnchorVisuals(ARAnchor anchor, string newName, bool isPersisted)
        {
            PersistableAnchorVisuals sampleAnchorVisuals = anchor.GetComponent<PersistableAnchorVisuals>();
            Debug.Log(isPersisted
                ? $"Anchor {anchor.trackableId} with name {newName} persisted"
                : $"Anchor {anchor.trackableId} with name {sampleAnchorVisuals.Name} unpersisted");
            sampleAnchorVisuals.Name = newName;
            sampleAnchorVisuals.Persisted = isPersisted;
        }
    }
}