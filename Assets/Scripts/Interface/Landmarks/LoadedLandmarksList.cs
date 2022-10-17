using Controller;
using TMPro;
using UnityEngine;

namespace Interface.Landmarks
{
    public class LoadedLandmarksList : MonoBehaviour
    {
        [SerializeField] private LandmarkController landmarkController;
        [SerializeField] private DataItem prefab;
        [SerializeField] private TextMeshProUGUI countLabel;
        private void OnEnable()
        {
            Populate();
        }

        private void OnDisable()
        {
            Clear();
        }

        private void Clear()
        {
            foreach (Transform child in transform) Destroy(child.gameObject);
        }

        private void Populate()
        {
            var landmarks = landmarkController.GetLandmarks();
            if (landmarks == null || landmarks.Count == 0)
            {
                var dataItem = Instantiate(prefab.gameObject, transform, false);
                if (!dataItem || !dataItem.TryGetComponent(out DataItem dataItemScript))
                {
                    Debug.Log($"Error instantiating DataItem for landmark");
                    return;
                }
                dataItemScript.ToggleIcon(false);
                dataItemScript.Label = "There are no landmarks.";
            }
            else
            {
                foreach (var landmark in landmarks)
                {
                    var dataItem = Instantiate(prefab.gameObject, transform, false);
                    if (!dataItem || !dataItem.TryGetComponent(out DataItem dataItemScript))
                    {
                        Debug.Log($"Error instantiating DataItem for landmark {landmark.GetLandmarkName()}");
                        continue;
                    }

                    dataItemScript.Icon = "Icon 103";
                    dataItemScript.Label =
                        $"{landmark.GetLandmarkName()}\n<size=5><alpha=#88>{landmark.GetLandmarkType().ToString()}</size>";
                } 
            }
            countLabel.SetText($"Count: {landmarks?.Count ?? 0}");
        }

        public void Refresh()
        {
            Clear();
            Populate();
        }
    }
}