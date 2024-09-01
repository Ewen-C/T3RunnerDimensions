using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    [Header ("Volume")]
    [Range(0, 1)]
    public float musicVolume = 0.5f;
    [Range(0, 1)]
    public float sfxVolume = 0.5f;
    [Range(0, 1)]

    private Bus musicBus;
    private Bus sfxBus;

    public static AudioManager instance { get; private set; }

    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogError("Plus d'un audio manager dans la scène");
        }
        instance = this;

        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }

    public void PlayOneShot (EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
    
    private void Update() 
    {
        musicBus.setVolume(musicVolume);
        sfxBus.setVolume(sfxVolume);
    }
}
