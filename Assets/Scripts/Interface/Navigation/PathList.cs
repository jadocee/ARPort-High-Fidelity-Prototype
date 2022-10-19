using System.Collections.Generic;
using Controller;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.UX;
using Model;
using TMPro;
using UnityEngine;

namespace Interface.Navigation
{
    public class PathList : MonoBehaviour
    {
        public GameObject pathPrefab;
        private NavigationController _navigationController;
        private List<Path> paths;

        private void Start()
        {
            _navigationController = GameObject.FindGameObjectWithTag("NavSystem").GetComponent<NavigationController>();
            LoadList();
        }

        private void LoadList()
        {
            var collection = gameObject.GetComponent<ToggleCollection>();
            foreach (var path in _navigationController.GetPaths())
            {
                var pathToggle = Instantiate(pathPrefab, gameObject.transform, false);
                pathToggle.name = $"{path.GetPathName()} Toggle";
                pathToggle.transform.Find("Frontplate/AnimatedContent/Text").gameObject.GetComponent<TextMeshProUGUI>()
                          .text =
                    $"<size=8>{path.GetPathName()}</size>\n<size=6>{path.GetPathDesc()}</size>";
                collection.Toggles.Add(pathToggle.GetComponent<StatefulInteractable>());
            }
        }

        public void ReloadList()
        {
            var collection = gameObject.GetComponent<ToggleCollection>();
            foreach (var toggle in collection.Toggles) Destroy(toggle.gameObject);
            collection.Toggles.Clear();
            collection.Toggles.TrimExcess();
            LoadList();
        }
    }
}