using UnityEngine;

public class LocationMarker : MonoBehaviour
{
    public string name { get; set; }
    public Vector3 position { get; set; }

    public LocationMarker(string name, Vector3 position)
    {
        this.name = name;
        this.position = position;
    }

    // [SerializeField] private Vect
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
    
    
}