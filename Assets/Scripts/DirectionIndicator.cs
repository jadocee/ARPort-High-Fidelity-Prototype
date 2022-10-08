using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    [SerializeField] private Transform spinner;
    public float speed;
    private Transform target;

    public DirectionIndicator()
    {
        target = null;
        speed = 0;
    }

    private void Update()
    {
        if (target) transform.LookAt(target);
        if (spinner) spinner.transform.Rotate(0, 0, speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        gameObject.SetActive(false);
    }

    public Transform GetTarget()
    {
        return target;
    }

    public Transform GetSpinner()
    {
        return spinner;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target.transform;
    }
}