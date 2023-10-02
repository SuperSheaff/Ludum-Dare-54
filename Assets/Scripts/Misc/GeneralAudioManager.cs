using UnityEngine.Audio;
using System;
using UnityEngine;

public class GeneralAudioManager : MonoBehaviour
{

    public GameSound[] gameSounds;

    public static GeneralAudioManager instance;

    void Awake() 
    {
        
        instance = this;

        DontDestroyOnLoad(gameObject);

        foreach (GameSound sound in gameSounds)
        {
            sound.source                = gameObject.AddComponent<AudioSource>();
            sound.source.clip           = sound.clip;
            sound.source.volume         = sound.volume;
            sound.source.pitch          = sound.pitch;
            sound.source.loop           = sound.loop;
            sound.source.spatialBlend   = sound.spatialBlend;
        }
    }

    public void PlaySound(string soundName)
    {
        GameSound sound = Array.Find(gameSounds, sound => sound.name == soundName);
        if (sound == null) 
        {
            Debug.LogWarning("Sound (start): " + soundName + " not found!");
            return;
        }
        sound.source.Play();
    }

    public void StopSound(string soundName)
    {
        GameSound sound = Array.Find(gameSounds, sound => sound.name == soundName);
        if (sound == null) 
        {
            Debug.LogWarning("Sound (stop): " + soundName + " not found!");
            return;
        }
        sound.source.Stop();
    }

}
