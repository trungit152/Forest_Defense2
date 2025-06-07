using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSettings
{
    const string MUSIC_VOLUME = "MUSIC_VOLUME";
    const string SFX_VOLUME = "SFX_VOLUME";
    const string SFX_MUTE = "SFX_MUTE";
    const string MUSIC_MUTE = "MUSIC_MUTE";
    const string UNMUTE_MUSIC_VOLUME = "UNMUTE_MUSIC_VOLUME";
    const string NEWBIE_TUTORIAL = "NEWBIE_TUTORIAL";

    public float musicVolume;
    public float sfxVolume;
    public float unmuteMusicVolume;
    public bool sfxMute;
    public bool musicMute;
    public bool newbieTutorial;
    public SaveSettings()
    {
        musicVolume = ES3.KeyExists(MUSIC_VOLUME) ? ES3.Load<float>(MUSIC_VOLUME) : 1.0f;
        sfxVolume = ES3.KeyExists(SFX_VOLUME) ? ES3.Load<float>(SFX_VOLUME) : 1.0f;
        unmuteMusicVolume = ES3.KeyExists(UNMUTE_MUSIC_VOLUME) ? ES3.Load<float>(UNMUTE_MUSIC_VOLUME) : 1.0f;
        sfxMute = ES3.KeyExists(SFX_MUTE) ? ES3.Load<bool>(SFX_MUTE) : false;
        musicMute = ES3.KeyExists(MUSIC_MUTE) ? ES3.Load<bool>(MUSIC_MUTE) : false;
        newbieTutorial = ES3.KeyExists(NEWBIE_TUTORIAL) ? ES3.Load<bool>(NEWBIE_TUTORIAL) : true;
    }

    public void Save()
    {
        ES3.Save<float>(MUSIC_VOLUME, musicVolume);
        ES3.Save<float>(SFX_VOLUME, sfxVolume);
        ES3.Save<bool>(SFX_MUTE, sfxMute);
        ES3.Save<bool>(MUSIC_MUTE, musicMute);
        ES3.Save<float>(UNMUTE_MUSIC_VOLUME, unmuteMusicVolume);
        ES3.Save<bool>(NEWBIE_TUTORIAL, newbieTutorial);
    }
}
