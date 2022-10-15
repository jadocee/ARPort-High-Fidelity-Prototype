using UnityEngine;

public class Tracking : MonoBehaviour
{
    [SerializeField] public Transform target;
    public GameObject spinner;

    private void Update()
    {
        if (target) transform.LookAt(target);
    }

    private void OnEnable()
    {
        spinner.SetActive(false);
    }

    public void changeTarget(GameObject newTarget)
    {
        target = newTarget.transform;
        spinner.SetActive(true);
    }
}