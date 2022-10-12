using System;
using Navigation;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public delegate void ArrivedAtLandmarkAction(Landmark landmark);

    public delegate void CancelNavigationAction();
    
    public delegate void EndNavigationAction(Landmark landmark);

    public delegate void LocationSelectAction(Guid locationId);

    public delegate void StartNavigationAction(Landmark landmark);

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

    public static event StartNavigationAction NavigationStartEvent;

    public static event EndNavigationAction NavigationEndEvent;
    public static event CancelNavigationAction NavigationCancelEvent;

    public void OnNavigationStart(Landmark landmark)
    {
        NavigationStartEvent?.Invoke(landmark);
    }

    public void OnNavigationCancel()
    {
        NavigationCancelEvent?.Invoke();
    }

    public void OnNavigationEnd(Landmark landmark)
    {
        NavigationEndEvent?.Invoke(landmark);
    }
}