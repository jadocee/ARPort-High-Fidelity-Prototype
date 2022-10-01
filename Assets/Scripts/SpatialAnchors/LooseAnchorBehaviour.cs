using UnityEngine;

namespace SpatialAnchors
{
    public class LooseAnchorBehaviour : MonoBehaviour
    {
        private GameObject target;
    
        public LooseAnchorBehaviour()
        {
            target = null;
        }

        // Start is called before the first frame update
        void Start()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            if (target != null)
            {
                this.gameObject.transform.SetPositionAndRotation(target.transform.position, target.transform.rotation);
            }
        }

        public GameObject Target
        {
            get => target;
            set => target = value;
        }

        // Update is called once per frame
        void Update()
        {
            UpdatePosition();
        }

        public void Free()
        {
            target = null;
        }
    }
}
