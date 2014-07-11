using UnityEngine;
using System.Collections;

public class VelocityController : MonoBehaviour
{
    public static float Speed;

    public static float SpeedMin = 0.015f;

    public static float SpeedMax = 0.025f;

    public static float SpawnTime;

    public static bool IsBoostSpeed;

    public static bool IsBoostSpeedMax;

    void Start()
    {
        Speed = Time.deltaTime / 3000;
        
        Speed = SpeedMin;

        SpawnTime = 10f;
    }

    void Update()
    {
        if (Speed > SpeedMin)
            IsBoostSpeed = true;

        if (Speed <= SpeedMax)
        {
            Speed += Time.deltaTime / 3000;

            SpawnTime -= Speed / 10;

            //Debug.Log("Speed: " + Speed + ", SpawnTime: " + SpawnTime);
        }
        else
            IsBoostSpeedMax = true;
    }
}