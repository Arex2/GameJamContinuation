using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelingPlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private float spawnTimer = 7f;
    private float spawnCountdownTimer;

    private void Start()
    {
        spawnCountdownTimer = spawnTimer;
    }

    private void Update()
    {
        if (spawnCountdownTimer > 0)
            spawnCountdownTimer -= Time.deltaTime;

        if (spawnCountdownTimer <= 0)
        {
            spawnCountdownTimer = spawnTimer;
            SpawnPlatform();
        }
    }

    private void SpawnPlatform()
    {
        Instantiate(platform, transform.position, platform.transform.localRotation);
    }
}
