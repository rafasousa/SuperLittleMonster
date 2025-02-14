﻿using UnityEngine;
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

    public bool HasMusic;

    public bool HasSound;

    void Awake()
    {
        // Register the singleton
        if (Instance != null)
            Debug.LogError("Multiple instances of SoundEffectsHelper!");

        this.HasMusic = PlayerPrefs.GetString("HasMusic").Equals("On");

        this.HasSound = PlayerPrefs.GetString("HasSound").Equals("On");

        Instance = this;
    }

    public void SetMusic()
    {
        Instance.HasMusic = !Instance.HasMusic;

        if (Instance.HasMusic)
            PlayerPrefs.SetString("HasMusic", "On");
        else
            PlayerPrefs.SetString("HasMusic", "Off");

        SetMute(!Instance.HasMusic);
    }

    public void SetMute(bool mute)
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.mute = mute;
    }

    public void SetSound()
    {
        Instance.HasSound = !Instance.HasSound;

        if (Instance.HasSound)
            PlayerPrefs.SetString("HasSound", "On");
        else
            PlayerPrefs.SetString("HasSound", "Off");
    }

    public void SetPitchBackgroundSound(float pitch)
    {
        if (!SoundEffectsHelper.Instance.HasMusic)
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

        if (HasSound && originalClip != null)
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
        audioSource.mute = !HasMusic;
        audioSource.PlayDelayed(playDelayed);
    }
}