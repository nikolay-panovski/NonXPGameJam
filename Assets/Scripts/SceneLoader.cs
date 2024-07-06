using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadSceneSingle(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
