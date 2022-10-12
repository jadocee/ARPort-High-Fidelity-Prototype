using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DirectionIndicator : MonoBehaviour
{
    [SerializeField] private Transform spinner;
    [SerializeField] private Transform mainCamera;
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
    }

    private void Update()
    {
        if (target) transform.LookAt(target);
        if (spinner) spinner.transform.Rotate(0, 0, speed * Time.deltaTime);
        if (mainCamera)
        {
            if (Vector3.Distance(mainCamera.position, target.position) < 500)
            {
                
            }
        }
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