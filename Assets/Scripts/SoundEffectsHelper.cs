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

    private AudioSource audioSource;

    public bool IsMute;

    void Awake()
    {
        // Register the singleton
        if (Instance != null)
            Debug.LogError("Multiple instances of SoundEffectsHelper!");

        this.IsMute = PlayerPrefs.GetString("IsMute").Equals("Off");

        Instance = this;
    }

    public void SetMute()
    {
        IsMute = !IsMute;

        Debug.Log("SetMute - PlayerPrefs.GetString: " + PlayerPrefs.GetString("IsMute"));

        if (!SoundEffectsHelper.Instance.IsMute)
            PlayerPrefs.SetString("IsMute", "Off");
        else
            PlayerPrefs.SetString("IsMute", "On");

        Debug.Log("SetMute - PlayerPrefs.GetString: " + PlayerPrefs.GetString("IsMute"));

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.mute = IsMute;
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
        audioSource.volume = 1f;
        audioSource.loop = true;
        audioSource.mute = IsMute;
        audioSource.PlayDelayed(0.5f);
    }
}