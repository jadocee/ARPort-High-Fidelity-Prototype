using System;
using System.Collections.Generic;
using UnityEngine;

public class MarkLocation : MonoBehaviour
{
    public GameObject marker;
    public GameObject positions;
    private List<Marker> markedPoints;

    // Start is called before the first frame update
    private void Start()
    {
        markedPoints = new List<Marker>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void AddLocation()
    {
        // Marker newMarker = new Marker();
        // newMarker.name = Guid.NewGuid().ToString("N");
        var p = new GameObject(Guid.NewGuid().ToString("N"));
        // p.transform.SetParent(positions.transform, true);
        p.SetActive(false);
        // var newObject = Instantiate(new GameObject(Guid.NewGuid().ToString("N")), positions.transform, true);
        // var p = Instantiate(marker, positions.transform, true);
        // p.SetActive(false);
    }

    public class Marker
    {
        public string name { get; set; }
        public Vector3 position { get; set; }
    }
}