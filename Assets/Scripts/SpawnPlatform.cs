using UnityEngine;
using System.Collections;

public class SpawnPlatform : MonoBehaviour
{
    public GameObject[] platforms;

    private float spawnPositionY = -4.5f;

    private float spawnTime = 10f;

    private bool spawn = true;

    private Vector3 oldSpawnPos = Vector3.zero;

    private GameObject platformClone;

    void Update()
    {
        if (spawn)
            StartCoroutine(spawnPlatforms());
    }

    IEnumerator spawnPlatforms()
    {
        spawn = false;

        var randomPlatform = Random.Range(0, platforms.Length);

        // New platform
        platformClone = Instantiate(platforms[randomPlatform], Vector3.zero, Quaternion.identity) as GameObject;

        platformClone.transform.position = new Vector3(oldSpawnPos.x + (platformClone.transform.localScale.x / 2) + 5f, spawnPositionY, 0f);

        oldSpawnPos = platformClone.transform.position;

        if (!VelocityController.IsBoostSpeedMax && VelocityController.IsBoostSpeed)
            spawnTime = VelocityController.SpawnTime;

        yield return new WaitForSeconds(spawnTime);

        spawn = true;
    }
};