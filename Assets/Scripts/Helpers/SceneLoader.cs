using UnityEngine;
using UnityEngine.SceneManagement;

namespace Helpers
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene("Scenes/" + sceneName);
            // SceneManager.UnloadSceneAsync()
        }
    }
}