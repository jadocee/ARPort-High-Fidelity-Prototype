using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace SpatialAnchors
{
    [RequireComponent(typeof(ARAnchor))]
    [RequireComponent(typeof(BoxCollider))]
    public class CollidableAnchor : MonoBehaviour
    {
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = gameObject.GetComponent<BoxCollider>();
            _boxCollider.enabled = false;
            _boxCollider.isTrigger = true;
            // _boxCollider.name = "Collider\\" + id;
        }
        
        private void OnEnable()
        {
            if (!_boxCollider)
            {
                Debug.Log("Missing BoxCollider");
                return;
            }
            _boxCollider.enabled = false;
        }

        
        private void OnTriggerEnter(Collider other)
        {
            // EventsManager.GetInstance().OnArrivedAtLandmark(this);
            DisableCollider();
        }

        public void EnableCollider()
        {
            _boxCollider.enabled = true;
        }

        public void DisableCollider()
        {
            _boxCollider.enabled = true;
        }
    }
}