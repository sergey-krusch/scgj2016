using System.Collections.Generic;
using UnityEngine;

public class AudioOut: MonoBehaviour
{
    private const float defaultFadeOutSpeed = 1.0f;
    private const int manuallyManagedSoundsLimit = 8;

    private static bool appPaused = false;
    private static AudioOut instance;
    private HashSet<AudioSourceHandle> manuallyManagedAudioSources;
    private List<AudioSourceHandle> autoManagedAudioSources;
    private List<FadingOutAudioSource> fadingOutAudioSources;
    private Queue<GameObject> vacantGameObjects;
    private int gameObjectCount;

    private static AudioClip GetAudioClip(string url, bool forceReload)
    {
        return Resources.Load<AudioClip>(url);
    }

    public float SoundVolume = 1.0f;
    public float MusicVolume = 1.0f;

    private AudioSourceHandle currentMusic;
    public AudioSourceHandle CurrentMusic
    {
        get
        {
            return currentMusic;
        }
    }

    private AudioSourceHandle currentAmbient;
    public AudioSourceHandle CurrentAmbient
    {
        get
        {
            return currentAmbient;
        }
    }

    public static AudioOut Instance
    {
        get
        {
            if (instance == null)
            {
                var gameObject = new GameObject("AudioOut");
                DontDestroyOnLoad(gameObject);
                gameObject.AddComponent<AudioListener>();
                instance = gameObject.AddComponent<AudioOut>();
            }
            return instance;
        }
    }

    public void Awake()
    {
        manuallyManagedAudioSources = new HashSet<AudioSourceHandle>();
        autoManagedAudioSources = new List<AudioSourceHandle>();
        fadingOutAudioSources = new List<FadingOutAudioSource>();
        vacantGameObjects = new Queue<GameObject>();
        instance = this;
    }

    public void OnDestroy()
    {
        instance = null;
    }

    public void FadeOutAndStop(AudioSourceHandle audioSource, float fadeOutSpeed)
    {
        if (!manuallyManagedAudioSources.Remove(audioSource))
            return;
        fadingOutAudioSources.Add(new FadingOutAudioSource(audioSource.AudioSource, fadeOutSpeed));
    }

    public void FadeOutAndStop(AudioSourceHandle audioSource)
    {
        FadeOutAndStop(audioSource, defaultFadeOutSpeed);
    }

    public void FadeOutAndStopCurrentMusic(float fadeOutSpeed = defaultFadeOutSpeed)
    {
        if (currentMusic == null || !currentMusic.AudioSource.isPlaying)
            return;
        fadingOutAudioSources.Add(new FadingOutAudioSource(currentMusic.AudioSource, fadeOutSpeed));
        currentMusic = null;
    }

    public void FadeOutAndStopCurrentAmbient(float fadeOutSpeed = defaultFadeOutSpeed)
    {
        if (currentAmbient == null || !currentAmbient.AudioSource.isPlaying)
            return;
        fadingOutAudioSources.Add(new FadingOutAudioSource(currentAmbient.AudioSource, fadeOutSpeed));
        currentAmbient = null;
    }

    public void Stop(AudioSourceHandle audioSource)
    {
        if (!manuallyManagedAudioSources.Remove(audioSource))
            return;
        audioSource.AudioSource.Stop();
        DestructAudioSource(audioSource.AudioSource);
    }

    public AudioSourceHandle PlaySoundManual(string url, float volume, float pan, bool loop)
    {
        if (manuallyManagedAudioSources.Count > manuallyManagedSoundsLimit)
        {
            Debug.LogWarning("Limit for manually managed sounds reached!");
            return null;
        }
        AudioClip audioClip = GetAudioClip(url, false);
        AudioSource audioSource = ConstructAudioSource();
        audioSource.clip = audioClip;
        audioSource.panStereo = pan;
        audioSource.loop = loop;
        audioSource.volume = 0.0f;
        audioSource.Play();
        AudioSourceHandle result = new AudioSourceHandle(audioSource);
        result.Volume = volume;
        manuallyManagedAudioSources.Add(result);
        return result;
    }

    public void PlaySound(string url, float volume, float pan)
    {
        AudioClip audioClip = GetAudioClip(url, false);
        AudioSource audioSource = ConstructAudioSource();
        audioSource.clip = audioClip;
        audioSource.panStereo = pan;
        audioSource.loop = false;
        audioSource.volume = GetRealSoundVolume(volume);
        audioSource.Play();
        AudioSourceHandle handle = new AudioSourceHandle(audioSource);
        handle.Volume = volume;
        autoManagedAudioSources.Add(handle);
    }

    public void PlayMusic(string url, float volume, float pan, bool loop = true)
    {
        FadeOutAndStopCurrentMusic();
        AudioClip audioClip = GetAudioClip(url, true);
        AudioSource audioSource = ConstructAudioSource();
        audioSource.clip = audioClip;
        audioSource.panStereo = pan;
        audioSource.loop = loop;
        audioSource.volume = 0.0f;
        audioSource.Play();
        currentMusic = new AudioSourceHandle(audioSource);
        currentMusic.Volume = volume;
    }

    public void PlayAmbient(string url, float volume, float pan, bool loop = false)
    {
        FadeOutAndStopCurrentAmbient();
        AudioClip audioClip = GetAudioClip(url, true);
        AudioSource audioSource = ConstructAudioSource();
        audioSource.clip = audioClip;
        audioSource.panStereo = pan;
        audioSource.loop = loop;
        audioSource.volume = 0.0f;
        audioSource.Play();
        currentAmbient = new AudioSourceHandle(audioSource);
        currentAmbient.Volume = volume;
    }

    public void Update()
    {
        for (int i = 0; i < fadingOutAudioSources.Count; ++i)
            if (fadingOutAudioSources[i].AudioSource != null)
                fadingOutAudioSources[i].AudioSource.volume -= fadingOutAudioSources[i].FadeOutSpeed * Time.deltaTime;
        if (appPaused)
            return;
        for (int i = fadingOutAudioSources.Count - 1; i >= 0; --i)
            if (fadingOutAudioSources[i].AudioSource == null || fadingOutAudioSources[i].AudioSource.volume <= 0.0f)
            {
                DestructAudioSource(fadingOutAudioSources[i].AudioSource);
                fadingOutAudioSources.RemoveAt(i);
            }
        for (int i = autoManagedAudioSources.Count - 1; i >= 0; --i)
        {
            if (autoManagedAudioSources[i] == null)
            {
                autoManagedAudioSources.RemoveAt(i);
                continue;
            }
            if (!autoManagedAudioSources[i].AudioSource.isPlaying)
            {
                DestructAudioSource(autoManagedAudioSources[i].AudioSource);
                autoManagedAudioSources.RemoveAt(i);
                continue;
            }
            autoManagedAudioSources[i].AudioSource.volume = GetRealSoundVolume(autoManagedAudioSources[i].Volume);
        }
        foreach (AudioSourceHandle handle in manuallyManagedAudioSources)
            if (handle != null && handle.AudioSource != null)
                handle.AudioSource.volume = GetRealSoundVolume(handle.Volume);
        if (currentMusic != null && currentMusic.AudioSource != null)
        {
            currentMusic.AudioSource.volume = currentMusic.Volume * MusicVolume;
        }
        if (currentAmbient != null && currentAmbient.AudioSource != null)
            currentAmbient.AudioSource.volume = currentAmbient.Volume * MusicVolume;
    }

    public void OnApplicationFocus(bool focus)
    {
        appPaused = !focus;
    }

    public void OnApplicationPause(bool pauseStatus)
    {
        appPaused = pauseStatus;
    }

    private float GetRealSoundVolume(float volume)
    {
        return volume * SoundVolume;
    }

    private AudioSource ConstructAudioSource()
    {
        if (vacantGameObjects.Count == 0)
        {
            ++gameObjectCount;
            var newObj = new GameObject(gameObjectCount.ToString("D3"));
            newObj.transform.SetParent(transform, false);
            vacantGameObjects.Enqueue(newObj);
        }
        var obj = vacantGameObjects.Dequeue();
        return obj.AddComponent<AudioSource>();
    }

    private void DestructAudioSource(AudioSource audioSource)
    {
        vacantGameObjects.Enqueue(audioSource.gameObject);
        Object.Destroy(audioSource);
    }
}