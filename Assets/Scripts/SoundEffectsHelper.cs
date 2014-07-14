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

    public AudioClip backgroundSound;

    public AudioClip playerJumpSound;

    public AudioClip playerRunSound;

    public AudioClip gameOverSound;

    public AudioClip menuSound;

    public AudioClip coinSound;

    public float volume = 1;

    public float playDelayed = 1;

    private AudioSource audioSource;

    public bool IsMute;

    public bool IsSound;

    void Awake()
    {
        // Register the singleton
        if (Instance != null)
            Debug.LogError("Multiple instances of SoundEffectsHelper!");

        this.IsMute = PlayerPrefs.GetString("IsMute").Equals("Off");
        this.IsSound = PlayerPrefs.GetString("IsSound").Equals("On");

        Instance = this;
    }

    public void SetMute()
    {
        Instance.IsMute = !Instance.IsMute;

        if (Instance.IsMute)
            PlayerPrefs.SetString("IsMute", "On");
        else
            PlayerPrefs.SetString("IsMute", "Off");
    }

    public void SetSound()
    {
        Instance.IsSound = !Instance.IsSound;

        if (Instance.IsSound)
            PlayerPrefs.SetString("IsSound", "On");
        else
            PlayerPrefs.SetString("IsSound", "Off");

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.mute = Instance.IsSound;
    }

    public void SetPitchBackgroundSound(float pitch)
    {
        if (!SoundEffectsHelper.Instance.IsMute)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.pitch = pitch;
        }
    }

    public void MakeSound(SoundType type)
    {
        AudioClip originalClip = null;

        switch (type)
        {
            case SoundType.Jump:
                originalClip = playerJumpSound;
                break;
            case SoundType.Run:
                originalClip = playerRunSound;
                break;
            case SoundType.Coin:
                originalClip = coinSound;
                break;
        }

        if (!IsMute && originalClip != null)
            AudioSource.PlayClipAtPoint(originalClip, transform.position);// As it is not 3D audio clip, position doesn't matter.
    }

    public void MakeBackgroundSound(SoundType type)
    {
        AudioClip originalClip = null;

        switch (type)
        {
            case SoundType.Menu:
                originalClip = menuSound;
                break;
            case SoundType.GameOver:
                originalClip = gameOverSound;
                break;
            case SoundType.Background:
                originalClip = backgroundSound;
                break;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = originalClip;
        audioSource.priority = 110;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.mute = IsSound;
        audioSource.PlayDelayed(playDelayed);
    }
}