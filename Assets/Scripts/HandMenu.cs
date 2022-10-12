using System;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using Navigation;
using Navigation.Interface;
using UnityEngine;

[RequireComponent(typeof(HandBounds))]
[RequireComponent(typeof(HandConstraintPalmUp))]
public class HandMenu : MonoBehaviour
{
    [SerializeField] private Canvas content;
    [SerializeField] private GameObject navigationStatePrefab;
    [SerializeField] private GameObject panel;
    private NavigationState _navigationState;
    private bool panelIsOpen;

    public HandMenu()
    {
        panelIsOpen = false;
    }

    public bool PanelIsOpen()
    {
        return panelIsOpen;
    }

    public void SetPanelIsOpen(bool state)
    {
        panelIsOpen = state;
    }
    
    private void Awake()
    {
        content.gameObject.SetActive(false);
        panel.SetActive(false);
        gameObject.GetComponent<HandConstraintPalmUp>()?.OnFirstHandDetected.AddListener(TogglePanel);
        NavigationSystem.StartNavigationEvent += ShowNavState;
        NavigationSystem.EndNavigationEvent += ClearNavState;
        NavigationSystem.StateChangeEvent += UpdateNavState;
    }

    private void TogglePanel()
    {
        panel.SetActive(panelIsOpen);
    }

    private void ShowNavState(Landmark landmark)
    {
        _navigationState = Instantiate(navigationStatePrefab, panel.transform, false)?.GetComponent<NavigationState>();
        // navState.transform.SetAsFirstSibling();
        if (_navigationState == null) return;
        _navigationState.SetNavigatingTo(landmark.GetLandmarkName());
        _navigationState.SetTimeRemaining(0, 5);
        _navigationState.SetDistanceRemaining(0.201);
        _navigationState.gameObject.SetActive(true);
        panel.SetActive(true);
        panelIsOpen = true;
    }

    private void UpdateNavState(Landmark landmark)
    {
        if (!_navigationState) return;
        _navigationState.SetNavigatingTo(landmark.GetLandmarkName());
        _navigationState.SetTimeRemaining(0, 5);
        _navigationState.SetDistanceRemaining(0.201);
    }

    private void ClearNavState()
    {
        if (_navigationState != null) Destroy(_navigationState.gameObject);
    }
}