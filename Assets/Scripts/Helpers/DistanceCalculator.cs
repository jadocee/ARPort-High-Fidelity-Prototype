using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Helpers
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

        public float Measure(Transform target)
        {
            var dist = Vector3.Distance(mainCamera.transform.position, target.position);
            return dist;
        }
    }
}