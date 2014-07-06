using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SpawnPlatform : MonoBehaviour
{
    public GameObject[] platforms;

    public float spawnTime = 5f;

    public float spawnPositionY = -4.5f;

    private bool spawn = true;

    Vector3 oldSpawnPos = Vector3.zero;

    GameObject platformClone;

    GameObject coinClone;

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

        yield return new WaitForSeconds(spawnTime);

        spawn = true;
    }
}