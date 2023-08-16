using UnityEngine;

public class SpawnControls : MonoBehaviour
{
    public static GameObject SpawnEnemy(GameObject enemy, Vector2 location)
    {
        return Instantiate(enemy, location, new Quaternion());
    }
}
