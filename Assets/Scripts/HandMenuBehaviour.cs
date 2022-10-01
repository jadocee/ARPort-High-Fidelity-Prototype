using System;
using UnityEngine;

public class HandMenuBehaviour : MonoBehaviour
{
    public GameObject contentCanvas;
    public GameObject createAnchorContainer;

    // Start is called before the first frame update
    private void Start()
    {
        contentCanvas.SetActive(false);
        createAnchorContainer.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}