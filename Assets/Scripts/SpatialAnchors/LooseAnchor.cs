using UnityEngine;

namespace SpatialAnchors
{
    public class LooseAnchor : MonoBehaviour
    {
        private Transform targetPosition;

        public LooseAnchor()
        {
            targetPosition = null;
        }
        
        // Start is called before the first frame update
        public void SetTargetPosition(Transform transform)
        {
            targetPosition = transform;
        }

        public bool hasTarget()
        {
            return targetPosition != null;
        }
        
        private void Start()
        {
            UpdatePosition();
        }

        // Update is called once per frame
        private void Update()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            if (targetPosition)
            {
                gameObject.transform.SetPositionAndRotation(targetPosition.position, targetPosition.rotation);
            }
        }

        public void Free()
        {
            targetPosition = null;
        }
    }
}