using UnityEngine;

public class AudioSourceHandle
{
    private readonly AudioSource audioSource;
    internal AudioSource AudioSource
    {
        get
        {
            return audioSource;
        }
    }
        
    private float volume;
    public float Volume
    {
        get
        {
            return volume;
        }
        set
        {
            volume = Mathf.Clamp01(value);
        }
    }

    public int Position
    {
        get
        {
            return audioSource.timeSamples;
        }
        set
        {
            audioSource.timeSamples = value;
        }
    }

    public float Pitch
    {
        get
        {
            return audioSource.pitch;
        }
        set
        {
            audioSource.pitch = value;
        }
    }

    public AudioSourceHandle(AudioSource audioSource)
    {
        this.audioSource = audioSource;
    }
}