using UnityEngine;

public class ButtonControls : MonoBehaviour
{
    protected Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public static void OpenLevelUpMenu()
    {
        Player l_player = FindObjectOfType<Player>();
        l_player.LevelupMenu.enabled = true;
        l_player.HUD.enabled = false;
        Time.timeScale = 0f;
    }

    public void CloseLevelUpMenu()
    {
        player.LevelupMenu.enabled = false;
        player.HUD.enabled = true;
        Time.timeScale = 1f;
    }
}
