using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public SoundEffect[] soundEffects;
    public Music[] musics;
    public static AudioManager instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        foreach (SoundEffect sound in soundEffects)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume * SaveGame.SaveSettings.sfxVolume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
        foreach (Music music in musics)
        {
            music.source = gameObject.AddComponent<AudioSource>();
            music.source.clip = music.clip;
            music.source.volume = music.volume * SaveGame.SaveSettings.musicVolume;
            music.source.pitch = music.pitch;
            music.source.loop = music.loop;
        }
        SetVolumeEffect();
        SetVolumeMusic();
    }

    public void SetVolumeEffect()
    {
        foreach (SoundEffect sound in soundEffects)
        {
            sound.source.volume = sound.volume * SaveGame.SaveSettings.sfxVolume;
        }
    }
    public void SetVolumeMusic()
    {
        foreach (Music music in musics)
        {
            music.source.volume = music.volume * SaveGame.SaveSettings.musicVolume;
        }
    }
    public void PlaySoundEffect(string name)
    {
        if(SaveGame.SaveSettings.sfxMute)  return;
        SoundEffect sound = Array.Find(soundEffects, s => s.name == name);
        if (sound != null)
        {
            sound.source.Play();
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    public void StopSoundEffect(string name)
    {
        SoundEffect sound = Array.Find(soundEffects, s => s.name == name);
        if (sound != null)
        {
            sound.source.Stop();
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    public void PlayMusic(string name)
    {
        Music music = Array.Find(musics, s => s.name == name);
        if (music != null)
        {
            music.source.Play();
            
        }
        else
        {
            Debug.LogWarning("Music: " + name + " not found!");
        }
    }

    public void FadeOutMusic(string name, float duration = 1f)
    {
        Music music = Array.Find(musics, s => s.name == name);
        if (music != null)
        {
            StartCoroutine(FadeOutCoroutine(music, duration));
        }
    }

    private IEnumerator FadeOutCoroutine(Music music, float duration)
    {
        float startVolume = music.source.volume;

        float time = 0f;
        while (time < duration)
        {
            music.source.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        music.source.volume = 0f;
        music.source.Stop();
        music.source.volume = startVolume; 
    }
}
