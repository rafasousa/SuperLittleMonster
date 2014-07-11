using System;
using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour
{
    HUD hud;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            hud = GameObject.Find("Main Camera").GetComponent<HUD>();

            var type = (PowerupType)Enum.Parse(typeof(PowerupType), transform.tag);

            switch (type)
            {
                case PowerupType.Coin:
                    hud.IncreaseCoin(1);

                    SoundEffectsHelper.Instance.MakeSound(SoundType.Coin);

                    break;
                case PowerupType.DoubleScore:
                    hud.IncreaseScore((int)(hud.playerScore * 2));
                    break;
                case PowerupType.Monster:
                    hud.IncreaseMonster(1);
                    break;
            }

            Destroy(transform.gameObject);
        }
    }

    public enum PowerupType
    {
        Coin,
        DoubleScore,
        Monster
    }
}
