using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnOverseer : MonoBehaviour
{
    public GameObject Skeleton;

    private IEnumerator WaveCountdown(float seconds)
    {
        while(true)
        {
            yield return new WaitForSeconds(seconds);
            CreateWave();
        }
    }

    public void Start()
    {
        StartCoroutine(WaveCountdown(15));
        CreateWave();
    }

    public GameObject[] CreateWave()
    {
        GameObject[] enemies = new GameObject[4];

        enemies[0] = SpawnControls.SpawnEnemy(Skeleton, (Vector2)ResourceControls.Player.transform.position + (Vector2.one * 10));
        enemies[1] = SpawnControls.SpawnEnemy(Skeleton, (Vector2)ResourceControls.Player.transform.position + new Vector2(10, -10));
        enemies[2] = SpawnControls.SpawnEnemy(Skeleton, (Vector2)ResourceControls.Player.transform.position - (Vector2.one * 10));
        enemies[3] = SpawnControls.SpawnEnemy(Skeleton, (Vector2)ResourceControls.Player.transform.position + new Vector2(-10, 10));

        return enemies;
    }
}
