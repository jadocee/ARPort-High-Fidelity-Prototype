using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace DefaultNamespace
{
    public class DistanceCalculator : MonoBehaviour
    {
        [SerializeField] private ARRaycastManager _raycastManager;
        [SerializeField] private Camera mainCamera;

        private void Update()
        {
            var headPos = mainCamera.transform.position;
            var gazeDir = mainCamera.transform.forward;
            // var gazePos = GameObject.Find("Gloab")
        }
    }
}