using UnityEngine;

public static class AudioPlayer
{
    public static string currentMusic = string.Empty;

    public static void GameplayMusic()
    {
        PlayMusic("Audio/Music/Gameplay", 0.55f);
    }

	public static void MenuMusic()
	{
        PlayMusic("Audio/Music/Opening", 0.75f);
	}

    private static void PlayMusic(string url, float volume)
    {
        if (currentMusic == url)
            return;
        currentMusic = url;
        AudioOut.Instance.PlayMusic(url, volume, 0.0f);
    }

    public static void FadeMusic()
    {
        if (AudioOut.Instance.CurrentMusic == null)
            return;
        var musicObject = AudioOut.Instance.CurrentMusic.AudioSource.gameObject;
        if (musicObject.GetComponent<AudioLowPassFilter>() != null)
            return;
        var filter = musicObject.AddComponent<AudioLowPassFilter>();
        filter.cutoffFrequency = 1000;
    }

    public static void UnfadeMusic()
    {
        if (AudioOut.Instance.CurrentMusic == null)
            return;
        var musicObject = AudioOut.Instance.CurrentMusic.AudioSource.gameObject;
        var filter = musicObject.GetComponent<AudioLowPassFilter>();
        if (filter == null)
            return;
        Object.Destroy(filter);
    }

    public static void Dig()
    {
        AudioOut.Instance.PlaySound("Audio/Sound/Dig", 1.0f, 0.0f);
    }

    public static void Drink(bool success)
    {
        if (success)
            AudioOut.Instance.PlaySound("Audio/Sound/Drink01", 1.0f, 0.0f);
        else
            AudioOut.Instance.PlaySound("Audio/Sound/DrinkFailure01", 1.0f, 0.0f);
    }

    public static void Drag()
    {
        AudioOut.Instance.PlaySound("Audio/Sound/Drag", 1.0f, 0.0f);
    }

    public static void Drop()
    {
        AudioOut.Instance.PlaySound("Audio/Sound/Drop", 1.0f, 0.0f);
    }

    public static void Die()
    {
        AudioOut.Instance.PlaySound("Audio/Sound/Die", 1.0f, 0.0f);
    }
}