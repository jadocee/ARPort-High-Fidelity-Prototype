using UnityEngine;

public class Tracking : MonoBehaviour
{
    [SerializeField] public Transform target;

    private void Update()
    {
        transform.LookAt(target);
    }

    public void changeTarget(GameObject newTarget)
    {
        target = newTarget.transform;
    }
}