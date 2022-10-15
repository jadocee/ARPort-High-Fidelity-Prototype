using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("Scenes/" + sceneName,LoadSceneMode.Single);
        // SceneManager.UnloadSceneAsync()
    }
}