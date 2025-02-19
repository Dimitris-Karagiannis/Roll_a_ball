using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementScript : MonoBehaviour
{

    private static string main_scene_name = "MiniGame";

    private void LoadScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
    public void LoadLevel()
    {
        LoadScene(main_scene_name);
    }
}
