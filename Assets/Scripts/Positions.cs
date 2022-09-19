using System;
using System.Collections.Generic;
using UnityEngine;

public class Positions : MonoBehaviour
{
    public List<LocationMarker> markers { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void AddMarker(string name, Vector3 position)
    {
        markers.Add(new LocationMarker(name, position));
    }
    
    // [Serializable]
    // public clas
}