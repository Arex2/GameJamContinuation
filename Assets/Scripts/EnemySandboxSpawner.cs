using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySandboxSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPos;
    [SerializeField]
    GameObject[] enemies;

    public void SpawnEnemy()
    {
        Instantiate(RandomEnemy(), spawnPos.position, transform.rotation);
    }

    private GameObject RandomEnemy()
    {
        return enemies[Random.Range(0, enemies.Length)];

    }
}
