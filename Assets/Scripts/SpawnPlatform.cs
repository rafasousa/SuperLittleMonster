using UnityEngine;
using System.Collections;

public class SpawnPlatform : MonoBehaviour
{
    public GameObject[] platforms;

    public float spawnPositionMaxY = -1.5f;

    public float spawnPositionMinY = -4.5f;

    private float spawnTime;

    private bool spawn = true;

    public Vector3 oldSpawnPos = Vector3.zero;

    private GameObject platformClone;

    void Start()
    {
        spawnTime = VelocityController.SpawnTimeMin;
    }

    void Update()
    {
        if (platformClone != null)
        {
            oldSpawnPos = platformClone.transform.position;

            if (!VelocityController.IsBoostSpeedMax && VelocityController.IsBoostSpeed)
                oldSpawnPos.x += 8f;
            else
                oldSpawnPos.x += 11f;
        }

        if (spawn && (platformClone == null || platformClone.transform.position.x <= 30))
            StartCoroutine(spawnPlatforms());
    }

    IEnumerator spawnPlatforms()
    {
        spawn = false;

        var randomPlatform = Random.Range(0, platforms.Length);

        var randomPositionY = Random.Range(spawnPositionMinY, spawnPositionMaxY);

        // New platform
        platformClone = Instantiate(platforms[randomPlatform], Vector3.zero, Quaternion.identity) as GameObject;

        platformClone.transform.position = new Vector3(oldSpawnPos.x + (platformClone.transform.localScale.x / 2) + 5f, randomPositionY, 0f);

        platformClone.AddComponent<PlatformControl>();

        oldSpawnPos = platformClone.transform.position;

        if (!VelocityController.IsBoostSpeedMax && VelocityController.IsBoostSpeed)
            spawnTime = VelocityController.SpawnTime;

        yield return new WaitForSeconds(spawnTime);

        spawn = true;
    }
};