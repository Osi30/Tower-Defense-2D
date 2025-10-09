
using UnityEngine.SceneManagement;

public static class SceneController
{
    public static void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public static void LoadRoadMap()
    {
        SceneManager.LoadScene(1);
    }

    public static void Restart()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
