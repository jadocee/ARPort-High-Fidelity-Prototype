using Controller;
using UnityEngine;

namespace Interface.Landmarks
{
    public class LoadedLandmarksList : MonoBehaviour
    {
        [SerializeField] private LandmarkController landmarkController;
        [SerializeField] private DataItem prefab;

        private void OnEnable()
        {
            var landmarks = landmarkController.GetLandmarks();
            if (landmarks == null || landmarks.Count == 0) return;
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

        private void OnDisable()
        {
            foreach (Transform child in transform) Destroy(child.gameObject);
        }
    }
}