using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace SpatialAnchors
{
    public class AnchorTool : MonoBehaviour
    {
        [SerializeField] private GameObject container;
        private AnchorManager anchorManager;
        private GameObject currentLooseAnchor;

        // for testing
        private int count;

        public AnchorTool()
        {
            container = currentLooseAnchor = null;
            count = 0;
        }

        // Start is called before the first frame update
        void Start()
        {
            anchorManager = GameObject.FindGameObjectWithTag("MRTK XR Rig").GetComponent<AnchorManager>();
        }

        public void SpawnLooseAnchor()
        {
            if (currentLooseAnchor == null)
            {
                currentLooseAnchor = Instantiate(anchorManager.LooseAnchorPrefab, container.transform.position,
                    container.transform.rotation);
            }

            currentLooseAnchor.GetComponent<LooseAnchorBehaviour>().Target = container;
        }

        public void Cancel()
        {
            if (currentLooseAnchor == null) return;
            Destroy(currentLooseAnchor);
            currentLooseAnchor = null;
        }

        private void ResetPrefab()
        {
            if (currentLooseAnchor == null)
            {
                SpawnLooseAnchor();
            }
            else
            {
                currentLooseAnchor.GetComponent<LooseAnchorBehaviour>().Target = container;
            }
        }

        public void Create()
        {
            if (currentLooseAnchor == null ||
                !currentLooseAnchor.TryGetComponent(out LooseAnchorBehaviour looseAnchorBehaviour) ||
                looseAnchorBehaviour.Target != null) return;
            Pose pose = new Pose(currentLooseAnchor.transform.position, currentLooseAnchor.transform.rotation);
            ARAnchor anchor = anchorManager.AddAnchor(pose);
            ResetPrefab();
        }

        public void Clear()
        {
            anchorManager.AnchorStoreClear();
            anchorManager.ClearSceneAnchors();
            ResetPrefab();
        }

        // Update is called once per frame
        void Update()
        {
            // if (current == null) return;
            // current.transform.SetPositionAndRotation(container.transform.position, container.transform.rotation);
        }
    }
}