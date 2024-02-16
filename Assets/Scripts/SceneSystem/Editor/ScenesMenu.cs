using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class ScenesMenu
{
    private const string EditorPrefPreviousScene = "ScenesMenu.PreviousScene";

    private static string PreviousScene
    {
        get => EditorPrefs.HasKey(EditorPrefPreviousScene)
            ? EditorPrefs.GetString(EditorPrefPreviousScene)
            : null;
        set
        {
            if (value != null) EditorPrefs.SetString(EditorPrefPreviousScene, value);
            else EditorPrefs.DeleteKey(EditorPrefPreviousScene);
        }
    }

    static ScenesMenu()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange change)
    {
        if (change != PlayModeStateChange.EnteredEditMode) return;

        var previousScene = PreviousScene;
        if (previousScene == null) return;

        EditorSceneManager.OpenScene(previousScene);
        PreviousScene = null;
    }

    [MenuItem("Scenes/Play from main menu %l", priority = 0)]
    private static void PlayFromMainMenu()
    {
        PreviousScene = SceneManager.GetActiveScene().path;
        OpenMainMenuScene();
        EditorApplication.isPlaying = true;
    }

    [MenuItem("Scenes/MainMenuScene")]
    private static void OpenMainMenuScene()
    {
        EditorSceneManager.OpenScene($"Assets/Scenes/{SceneName.MainMenuScene.ToString()}.unity");
    }

    [MenuItem("Scenes/WhispererScene")]
    private static void OpenWhispererScene()
    {
        EditorSceneManager.OpenScene($"Assets/Scenes/{SceneName.WhispererScene.ToString()}.unity");
    }

    [MenuItem("Scenes/ComedianScene")]
    private static void OpenComedianScene()
    {
        EditorSceneManager.OpenScene($"Assets/Scenes/{SceneName.ComedianScene.ToString()}.unity");
    }
}