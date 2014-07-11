using UnityEngine;
using System.Collections;

public class PlatformControl : MonoBehaviour
{
    public Vector2 speed = new Vector2(6f, 0f);

    public Vector2 direction = new Vector2(-1, 0);

    public float timeDestroy = 25f;

    public float speedMin = 0.140875f;

    void Update()
    {
        if (!HUD.pause)
        {
            var movement = new Vector3(
              speed.x * direction.x,
              speed.y * direction.y,
              0);
            
            if (!VelocityController.IsBoostSpeedMax)
            {
                if (VelocityController.IsBoostSpeed)
                {
                    movement *= VelocityController.Speed;
                    timeDestroy += VelocityController.Speed / 50;
                }
                else
                    movement *= Time.deltaTime;
            }
            else
                movement = movement * VelocityController.Speed;

            transform.Translate(movement);

            var dist = (transform.position - Camera.main.transform.position).z;

            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;

            if (transform.position.x < leftBorder)
                Destroy(transform.gameObject, timeDestroy);
        }
    }
}
