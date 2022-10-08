using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;

namespace Navigation.Interface
{
    public class PathList : MonoBehaviour
    {
        public GameObject pathPrefab;
        private NavigationSystem navigationSystem;
        private List<Path> paths;

        private void Start()
        {
            navigationSystem = GameObject.FindGameObjectWithTag("Waypoint System").GetComponent<NavigationSystem>();
            LoadList();
        }

        private void LoadList()
        {
            var collection = gameObject.GetComponent<ToggleCollection>();
            foreach (var path in navigationSystem.GetPaths())
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