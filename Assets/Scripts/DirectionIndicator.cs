using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    [SerializeField] private Transform spinner;
    private Transform mainCamera;
    public float speed;
    private Transform target;

    public DirectionIndicator()
    {
        target = null;
        speed = 0;
    }

    private void Awake()
    {
        gameObject.SetActive(false);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera")?.transform;
    }

    private void Update()
    {
        if (target)
        {
            transform.LookAt(target);
        }

        if (spinner)
        {
            spinner.transform.Rotate(0, 0, speed * Time.deltaTime);
            // spinner.transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        /*if (mainCamera)
            if (Vector3.Distance(mainCamera.position, target.position) < 500)
            {
            }*/
    }

    public Transform GetTarget()
    {
        return target;
    }

    public Transform GetSpinner()
    {
        return spinner;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}