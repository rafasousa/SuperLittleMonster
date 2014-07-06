using UnityEngine;
using System.Collections;

public class PlatformControl : MonoBehaviour
{
    public Vector2 speed = new Vector2(6f, 0f);

    public Vector2 direction = new Vector2(-1, 0);

    void Update()
    {
        var movement = new Vector3(
          speed.x * direction.x,
          speed.y * direction.y,
          0);

        movement *= Time.deltaTime;

        transform.Translate(movement);

        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;

        if (transform.position.x < leftBorder)
            Destroy(transform.gameObject, 5f);
    }
}
