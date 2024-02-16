using UnityEngine.SceneManagement;

public static class PurrfectSceneManager
{
    public static void LoadScene(SceneName sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}