using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public delegate void LocationSelectAction(Guid locationId);

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

    public void OnLocationSelect(Guid locationId)
    {
        LocationSelectEvent?.Invoke(locationId);
    }
}