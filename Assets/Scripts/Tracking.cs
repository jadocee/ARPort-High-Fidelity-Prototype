using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Tracking : MonoBehaviour
{
    [SerializeField] public Transform target;
    public GameObject spinner;
    private void OnEnable()
    {
        spinner.SetActive(false);
    }

    private void Update()
    {
        if (target) transform.LookAt(target);
    }

    public void changeTarget(GameObject newTarget)
    {
        target = newTarget.transform;
        spinner.SetActive(true);
    }
}