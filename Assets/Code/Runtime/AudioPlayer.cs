using UnityEngine;

public static class AudioPlayer
{
    public static void GameplayMusic()
    {
        AudioOut.Instance.PlayMusic("Audio/Music/Gameplay", 0.55f, 0.0f);
    }

	public static void MenuMusic()
	{
		AudioOut.Instance.PlayMusic("Audio/Music/Opening", 0.75f, 0.0f);
	}

    public static void Dig()
    {
        AudioOut.Instance.PlaySound("Audio/Sound/Dig", 1.0f, 0.0f);
    }

    public static void Drink()
    {
        AudioOut.Instance.PlaySound("Audio/Sound/Drink01", 1.0f, 0.0f);
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