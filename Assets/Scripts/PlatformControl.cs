using UnityEngine;
using System.Collections;

public class PlatformControl : MonoBehaviour
{
    public Vector2 speed = new Vector2(6f, 0f);

    public Vector2 direction = new Vector2(-1, 0);

    void Update()
    {
        if (!HUD.IsExit)
        {
            var movement = new Vector3(
              speed.x * direction.x,
              speed.y * direction.y,
              0);

            if (!VelocityController.IsBoostSpeedMax)
            {
                if (VelocityController.IsBoostSpeed)
                    movement *= VelocityController.Speed;
                else
                    movement *= Time.deltaTime;
            }
            else
                movement = movement * VelocityController.Speed;

            transform.Translate(movement);

            var dist = (transform.position - Camera.main.transform.position).z;

            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;

            if (transform.position.x < leftBorder)
                Destroy(transform.gameObject, VelocityController.DestroyTime);
        }
    }
}
