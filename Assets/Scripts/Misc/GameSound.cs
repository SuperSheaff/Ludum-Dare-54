using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class GameSound
{
    public string name;
    public AudioClip clip;
    public bool loop;

    [HideInInspector]
    public AudioSource source;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    [Range(0f, 1f)]
    public float spatialBlend = 0.5f;
}
