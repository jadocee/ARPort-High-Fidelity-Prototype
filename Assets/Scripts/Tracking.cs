using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    public void changeTarget(GameObject newTarget)
    {
        target = newTarget.transform;
    }

    private void Update()
    {
        transform.LookAt(target);
    }
}
