using UnityEngine;
using System.Collections;

public class VelocityController : MonoBehaviour
{
    public static float Speed;

    public static float SpeedMin = 0.017f;

    public static float SpeedMax = 0.035f;

    public static float SpawnTime;

    public static float SpawnTimeMin = 1.2f;

    public static float DestroyTime = 10;

    public static float DestroyTimeMin = 3;

    public static bool IsBoostSpeed;

    public static bool IsBoostSpeedMax;

    void Start()
    {
        Speed = Time.deltaTime / 2500;

        Speed = SpeedMin;

        SpawnTime = SpawnTimeMin;
    }

    void Update()
    {
        if (Speed > SpeedMin)
            IsBoostSpeed = true;

        if (Speed <= SpeedMax)
        {
            Speed += Time.deltaTime / 3500;

            SpawnTime -= Speed / 500;

            var destroyTime = Speed / 5;

            if (DestroyTime >= DestroyTimeMin)
                DestroyTime -= destroyTime;

            Debug.Log(string.Format("Speed: {0}, SpawnTime: {1}, DestroyTime: {2}", Speed, SpawnTime, DestroyTime));
        }
        else
            IsBoostSpeedMax = true;
    }
}