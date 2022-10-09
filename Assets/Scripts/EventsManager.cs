using System;
using Navigation;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

public class EventsManager : MonoBehaviour
{
    public delegate void LocationSelectAction(Guid locationId);

    public delegate void ArrivedAtLandmarkAction(Landmark landmark);

    // singleton pattern
    private static EventsManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public static EventsManager GetInstance()
    {
        return instance;
    }

    public static event LocationSelectAction LocationSelectEvent;

    public static event ArrivedAtLandmarkAction ArrivedAtLandmarkEvent;

    public void OnLocationSelect(Guid locationId)
    {
        LocationSelectEvent?.Invoke(locationId);
    }

    public void OnArrivedAtLandmark(Landmark landmark)
    {
        ArrivedAtLandmarkEvent?.Invoke(landmark);
    }

    public delegate void DisplayContentAction(GameObject content);

    public event DisplayContentAction DisplayContentEvent;

    public void OnDisplayContent(GameObject content)
    {
        DisplayContentEvent.Invoke(content);
    }
}