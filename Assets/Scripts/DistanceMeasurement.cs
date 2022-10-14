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
    public float dist;

    void Update()
    {
        if (UCamera)
        {
            float dist = Vector3.Distance(UCamera.position, Target.position);
            string distance = dist.ToString("F2");
            Text.text=(distance + "m");
        }
    }
}
