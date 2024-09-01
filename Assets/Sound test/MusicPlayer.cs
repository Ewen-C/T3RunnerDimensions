using UnityEngine;
using FMOD.Studio;

public class MusicPlayer : MonoBehaviour
{
    private EventInstance musicLoop;
    public bool isMusicPlaying = true;

    [SerializeField] [Range(0f, 1f)]
    private float hyperMod;

    void Start()
    {
        musicLoop = AudioManager.instance.CreateInstance(FMODEvents.instance.music);
    }

    void Update()
    {
        if (isMusicPlaying)
        {
            musicLoop.getPlaybackState(out var playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) musicLoop.start();
        }
        else
        {
            musicLoop.stop(STOP_MODE.IMMEDIATE);
        }
        
        musicLoop.setParameterByName("Gauge", hyperMod);
    }
}
