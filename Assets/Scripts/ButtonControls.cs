using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControls : MonoBehaviour
{
    public static bool Pausable = false;
    public static bool Paused = false;

    public static void PlayGame()
    {
        Pausable = true;
        SceneManager.LoadScene("TestScene");
    }

    public static void MainMenu()
    {
        Pausable = false;
        SceneManager.LoadScene("MainMenu");
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void TogglePauseMenu()
    {
        if (Paused) { ClosePauseMenu();  }
        else { OpenPauseMenu(); }
    }

    public static void OpenPauseMenu()
    {
        if (Pausable) {
            ResourceControls.Player.PauseMenu.enabled = true;
            Time.timeScale = 0f;
            Paused = true;
        }
    }

    public static void ClosePauseMenu()
    {
        if (Paused)
        {
            ResourceControls.Player.PauseMenu.enabled = false;
            ResourceControls.Player.QuitWarning.enabled = false;
            Time.timeScale = 1f;
            Paused = false;
        }
    }

    public static void OpenQuitWarning()
    {
        ResourceControls.Player.QuitWarning.enabled = true;
    }

    public static void CloseQuitWarning()
    {
        ResourceControls.Player.QuitWarning.enabled = false;
    }

    public static void OpenLevelUpMenu()
    {
        ResourceControls.Player.LevelupMenu.enabled = true;
        ResourceControls.Player.HUD.enabled = false;
        Time.timeScale = 0f;
        Pausable = false;
    }

    public static void CloseLevelUpMenu()
    {
        ResourceControls.Player.LevelupMenu.enabled = false;
        ResourceControls.Player.HUD.enabled = true;
        Time.timeScale = 1f;
        Pausable = true;
    }

    public static void OpenDeathMenu()
    {
        ResourceControls.Player.DeathScreen.enabled = true;
    }

    public static void CloseDeathMenu()
    {

    }
}
