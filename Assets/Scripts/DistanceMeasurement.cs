using TMPro;
using UnityEngine;

public class DistanceMeasurement : MonoBehaviour
{
    [SerializeField] public Transform Target;

    [SerializeField] public Transform UCamera;

    [SerializeField] private TextMeshPro Text;

    public float dist;

    private void Update()
    {
        if (UCamera)
        {
            var dist = Vector3.Distance(UCamera.position, Target.position);
            var distance = dist.ToString("F2");
            Text.text = distance + "m";
        }
    }
}