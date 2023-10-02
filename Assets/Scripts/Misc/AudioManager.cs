using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField]
    private List<AudioClip> audioClips = new List<AudioClip>();

    private Dictionary<string, AudioClip> audioClipDictionary = new Dictionary<string, AudioClip>();

    private AudioSource audioSource;

    private void Awake()
    {
        // Create singleton instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Create dictionary of audio clips
        foreach (AudioClip clip in audioClips)
        {
            audioClipDictionary.Add(clip.name, clip);
        }

        // Get audio source component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayAudio(string clipName, float volume = 1.0f)
    {
        if (audioClipDictionary.ContainsKey(clipName))
        {
            audioSource.spatialBlend = 0f; // Full 3D spatialization
            audioSource.PlayOneShot(audioClipDictionary[clipName], volume);
        }
        else
        {
            Debug.LogWarning("Audio clip " + clipName + " not found in audio manager.");
        }
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}
