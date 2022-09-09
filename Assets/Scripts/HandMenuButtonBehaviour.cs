using System;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using UnityEngine;

public class HandMenuButtonBehaviour : MonoBehaviour
{
    public GameObject menu;
    private RadialView script;

    // Start is called before the first frame update
    private void Start()
    {
        script = menu.GetComponent<RadialView>();
    }

    // Update is called once per frame
    private void Update()
    {
    }
    
    public void ToggleMenu(bool state)
    {
        if (menu == null) return;
        menu.SetActive(state);
        menu.GetComponent<RadialView>().enabled = state;
    }
}