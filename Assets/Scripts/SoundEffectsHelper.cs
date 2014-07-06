using UnityEngine;
using System.Collections;

/// <summary>
/// Creating instance of sounds from code with no effort
/// </summary>
public class SoundEffectsHelper : MonoBehaviour
{
    /// <summary>
    /// Singleton
    /// </summary>
    public static SoundEffectsHelper Instance;

    public AudioClip playerJumpSound;
    public AudioClip playerRunSound;
    public AudioClip coinSound;

    void Awake()
    {
        // Register the singleton
        if (Instance != null)
            Debug.LogError("Multiple instances of SoundEffectsHelper!");

        Instance = this;
    }

    public void MakeJumpSound()
    {
        MakeSound(playerJumpSound);
    }

    public void MakeRunSound()
    {
        MakeSound(playerRunSound);
    }

    public void MakeCoinSound()
    {
        MakeSound(coinSound);
    }

    /// <summary>
    /// Play a given sound
    /// </summary>
    /// <param name="originalClip"></param>
    private void MakeSound(AudioClip originalClip)
    {
        // As it is not 3D audio clip, position doesn't matter.
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}
