using UnityEngine;

namespace SpatialAnchors
{
    public class LooseAnchorBehaviour : MonoBehaviour
    {
        public LooseAnchorBehaviour()
        {
            Target = null;
        }

        public GameObject Target { get; set; }

        // Start is called before the first frame update
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
            if (Target != null)
                gameObject.transform.SetPositionAndRotation(Target.transform.position, Target.transform.rotation);
        }

        public void Free()
        {
            Target = null;
        }
    }
}