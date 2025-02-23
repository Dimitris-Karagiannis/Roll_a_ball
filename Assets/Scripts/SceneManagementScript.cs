using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementScript : MonoBehaviour
{

    private static string main_scene_name = "MiniGame";
    private static string main_menu_name = "MainMenu";
    
    public GameObject settings_canvas;
    public GameObject main_menu_canvas;

    public SettingsScript settingsScript;

    private void LoadScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
    public void LoadLevel()
    {
        LoadScene(main_scene_name);
    }

    public void LoadMenu()
    {
        LoadScene(main_menu_name);
    }

    public void ApplyChanges()
    {
        settingsScript.SaveChanges();

        settings_canvas.gameObject.SetActive(false);
        main_menu_canvas.gameObject.SetActive(true);
    }

    public void MenuToSettings()
    {
        settings_canvas.gameObject.SetActive(true);
        main_menu_canvas.gameObject.SetActive(false);
    }
}
