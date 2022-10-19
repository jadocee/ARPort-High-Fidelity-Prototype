using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using Microsoft.MixedReality.Toolkit.UX;
using UnityEngine;

[RequireComponent(typeof(Follow))]
[RequireComponent(typeof(RadialView))]
public abstract class ARPortMenu : MonoBehaviour
{
    [SerializeField] private Canvas menuContent;
    [SerializeField] private PressableButton pinButton;

    public void HideMenu()
    {
        if (!menuContent)
        {
            Debug.Log("Missing menu content Canvas");
            return;
        }

        menuContent.gameObject.SetActive(false);
        gameObject.GetComponent<RadialView>().enabled = false;
        gameObject.GetComponent<Follow>().enabled = true;
    }

    public void ShowMenu()
    {
        if (!menuContent)
        {
            Debug.Log("Missing menu content Canvas");
            return;
        }

        gameObject.GetComponent<Follow>().enabled = false;
        gameObject.GetComponent<RadialView>().enabled = true;
        menuContent.gameObject.SetActive(true);
    }

    public void PinMenu()
    {
        gameObject.GetComponent<RadialView>().enabled = false;
        gameObject.GetComponent<Follow>().enabled = false;
        if (!pinButton.IsToggled) pinButton.ForceSetToggled(true, false);
    }
}