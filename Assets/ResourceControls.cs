using UnityEngine;

public class ResourceControls : MonoBehaviour
{
    public static Player Player;

    private void Awake()
    {
        Player = FindObjectOfType<Player>();
    }
}
