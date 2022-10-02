using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    // singleton pattern
    private static EventsManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static EventsManager GetInstance()
    {
        return instance;
    }

    public delegate void LocationSelectAction(Guid locationId);
    public static event LocationSelectAction LocationSelectEvent;

    public void OnLocationSelect(Guid locationId)
    {
        LocationSelectEvent?.Invoke(locationId);
    }
}