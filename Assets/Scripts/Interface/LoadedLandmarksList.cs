using Navigation;
using UnityEngine;

namespace Interface
{
    public class LoadedLandmarksList : MonoBehaviour
    {
        [SerializeField] private LandmarkManager _landmarkManager;
        [SerializeField] private DataItem prefab;

        private void Start()
        {
            var landmarks = _landmarkManager.GetLandmarks();
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
    }
}