using UnityEngine.SceneManagement;

public static class Loader
{
    // enum for the names of all 
    // the game scenes
    public enum Scene
    {
        MainmenuScene,
        LoadingScene,
        JoinRoom,
        DemoGameScene
    }

    private static Scene targetScene;   // Store scene to load after loading scene

    public static void LoadScene(Scene scene)
    {
        targetScene = scene;                                    // Sets scene to load later
        SceneManager.LoadScene(Scene.LoadingScene.ToString());  // Load the loading scene
    }

    public static void LoadAdditiveScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString()); // Load the target scene that was set
    }
}
