using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementScript : MonoBehaviour
{

    private static string main_scene_name = "MiniGame";
    
    public GameObject settings_canvas;
    public GameObject main_menu_canvas;

    private void LoadScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
    public void LoadLevel()
    {
        LoadScene(main_scene_name);
    }

    public void ApplyChanges()
    {


        settings_canvas.gameObject.SetActive(false);
        main_menu_canvas.gameObject.SetActive(true);
    }

    public void MenuToSettings()
    {
        settings_canvas.gameObject.SetActive(true);
        main_menu_canvas.gameObject.SetActive(false);
    }
}
