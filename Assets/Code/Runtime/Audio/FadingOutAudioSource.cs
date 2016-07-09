using UnityEngine;

public struct FadingOutAudioSource
{
    public readonly AudioSource AudioSource;
    public readonly float FadeOutSpeed;

    public FadingOutAudioSource(AudioSource audioSource, float fadeOutSpeed)
    {
        AudioSource = audioSource;
        FadeOutSpeed = fadeOutSpeed;
    }
}