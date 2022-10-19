using UnityEngine;

namespace Interface.Anchors
{
    public class LooseAnchor : MonoBehaviour
    {
        private Transform targetPosition;

        public LooseAnchor()
        {
            targetPosition = null;
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

        // Start is called before the first frame update
        public void SetTargetPosition(Transform transform)
        {
            targetPosition = transform;
        }

        public bool hasTarget()
        {
            return targetPosition != null;
        }

        private void UpdatePosition()
        {
            if (targetPosition)
                gameObject.transform.SetPositionAndRotation(targetPosition.position, targetPosition.rotation);
        }

        public void Free()
        {
            targetPosition = null;
        }
    }
}