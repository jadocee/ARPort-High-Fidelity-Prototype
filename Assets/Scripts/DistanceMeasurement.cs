using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class DistanceMeasurement : MonoBehaviour
{

    [SerializeField]
    public Transform Target;
    [SerializeField]
    public Transform UCamera;
    [SerializeField]
    TextMeshPro Text;

    void Update()
    {
        if (UCamera)
        {
            float dist = Vector3.Distance(UCamera.position, Target.position);
            Debug.Log(dist.ToString("F1"));
            Text.text=(dist + "m");
        }
    }
}
