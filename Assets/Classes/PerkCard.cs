using UnityEngine;

public abstract class PerkCard : MonoBehaviour
{
    protected Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public abstract void OnSelect();

    public abstract void OnUpgrade();
}
