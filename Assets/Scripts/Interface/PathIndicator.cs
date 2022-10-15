using UnityEngine;

namespace Interface
{
    public class PathIndicator : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform spinner;
        [SerializeField] private float speed;

        // Start is called before the first frame update
        private void Start()
        {
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        private void Update()
        {
            if (target) transform.LookAt(target);
            if (spinner) spinner.transform.Rotate(0, 0, speed * Time.deltaTime);
        }

        public Transform GetTarget()
        {
            return target;
        }

        public Transform GetSpinner()
        {
            return spinner;
        }

        public void SetTarget(Transform target = null)
        {
            this.target = target;
        }
    }
}