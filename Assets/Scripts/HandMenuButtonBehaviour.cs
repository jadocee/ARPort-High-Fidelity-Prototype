using System;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using UnityEngine;

public class HandMenuButtonBehaviour : MonoBehaviour
{
    public GameObject navigateMenu;
    public GameObject myGroupMenu;
    public GameObject notificationsMenu;

    private GameObject active;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void CloseAll()
    {
        if (navigateMenu && navigateMenu.activeSelf)
        {
            navigateMenu.SetActive(false);
            navigateMenu.GetComponent<RadialView>().enabled = false;
        }

        if (myGroupMenu && myGroupMenu.activeSelf)
        {
            myGroupMenu.SetActive(false);
            myGroupMenu.GetComponent<RadialView>().enabled = false;
        }

        if (notificationsMenu && notificationsMenu.activeSelf)
        {
            notificationsMenu.SetActive(false);
            notificationsMenu.GetComponent<RadialView>().enabled = false;
        }
    }

    public void NavigateMenu(bool state)
    {
        if (navigateMenu == null) return;
        if (state) CloseAll();
        navigateMenu.SetActive(state);
        navigateMenu.GetComponent<RadialView>().enabled = state;
    }

    public void ShowMenu(GameObject menu)
    {
        if (menu == null) return;
        if (active != null)
        {
            active.SetActive(false);
            active.GetComponent<RadialView>().enabled = false;
        }

        active = menu;
        menu.SetActive(true);
        menu.GetComponent<RadialView>().enabled = true;
    }

    public void HideMenu()
    {
        if (active == null) return;
        active.SetActive(false);
        active.GetComponent<RadialView>().enabled = false;
        active = null;
    }
}