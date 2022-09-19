using UnityEngine;

public class NavigationArrowBehaviour : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform spinner;
    [SerializeField] private float speed;
    private bool _istargetNotNull;


    // Start is called before the first frame update
    private void Start()
    {
        _istargetNotNull = target != null;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (GetTarget()) transform.LookAt(target);
        if (GetSpinner()) spinner.transform.Rotate(0, 0, speed * Time.deltaTime);
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
        _istargetNotNull = target != null;
        this.target = target;
    }
}