using UnityEngine;

namespace SpatialAnchors
{
    public class AnchorTool : MonoBehaviour
    {
        public GameObject looseAnchorPrefab;

        [SerializeField] private GameObject container;

        private GameObject current;
    
        // for testing
        private int count;

        public AnchorTool()
        {
            looseAnchorPrefab = container = current = null;
            count = 0;
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        public void SpawnLooseAnchor()
        {
            if (current == null)
            {
                current = Instantiate(looseAnchorPrefab, container.transform.position, container.transform.rotation);

            }
            current.GetComponent<LooseAnchorBehaviour>().Target = container;
        }

        public void Cancel()
        {
            if (current == null) return;
            Destroy(current);
            current = null;
        }

        private void ResetPrefab()
        {
            if (current == null)
            {
                SpawnLooseAnchor();
            }
            else
            {
                current.GetComponent<LooseAnchorBehaviour>().Target = container;
            }
        }

        public void Create()
        {
            if (current == null || 
                !current.TryGetComponent(out LooseAnchorBehaviour looseAnchorBehaviour) ||
                looseAnchorBehaviour.Target != null) return;
            var anchorScript = GameObject.FindGameObjectWithTag("MRTK XR Rig").GetComponent<AnchorScript>();
            Pose pose = new Pose(current.transform.position, current.transform.rotation);
            anchorScript.AddAnchor(pose, "Location " + (++count).ToString());
            ResetPrefab();
        }

        public void Clear()
        {
            var anchorScript = GameObject.FindGameObjectWithTag("MRTK XR Rig").GetComponent<AnchorScript>();
            anchorScript.AnchorStoreClear();
            anchorScript.ClearSceneAnchors();
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
