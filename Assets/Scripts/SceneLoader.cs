using UnityEngine;
using UnityEngine.SceneManagement;

// ~~imagine making a static class in Unity when you want to use it in Inspector events
public class SceneLoader : MonoBehaviour
{
    public static void LoadSceneSingle(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
