using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class TestMusic : MonoBehaviour
{
    
    private EventInstance musicLoop;
    public bool isMusicPlaying;

    [SerializeField] [Range(0f, 1f)]
    private float hyperMod;

    void Start()
    {
        musicLoop = AudioManager.instance.CreateInstance(FMODEvents.instance.music);
    }

    void Update()
    {
        if (isMusicPlaying == true)
        {
            PLAYBACK_STATE playbackState;
            musicLoop.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                musicLoop.start();
            }
        }
        else
        {
            musicLoop.stop(STOP_MODE.IMMEDIATE);
        }
        musicLoop.setParameterByName("Gauge", hyperMod);
    }
}
