using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMenuTalent
{
    const string RABBIT_CURRENT_LEVEL_KEY = "RABBIT_CURRENT_LEVEL";
    const string CROC_CURRENT_LEVEL_KEY = "CROC_CURRENT_LEVEL";
    const string FOX_CURRENT_LEVEL_KEY = "FOX_CURRENT_LEVEL";
    const string PENGUIN_CURRENT_LEVEL_KEY = "PENGUIN_CURRENT_LEVEL";
    const string DUCK_CURRENT_LEVEL_KEY = "DUCK_CURRENT_LEVEL";
    const string THUNDERBIRD_CURRENT_LEVEL_KEY = "THUNDERBIRD_CURRENT_LEVEL";
    const string BADGER_CURRENT_LEVEL_KEY = "BADGER_CURRENT_LEVEL";
    const string SPIKE_CURRENT_LEVEL_KEY = "SPIKE_CURRENT_LEVEL";
    const string FIRE_CURRENT_LEVEL_KEY = "FIRE_CURRENT_LEVEL";
    const string SWAMP_CURRENT_LEVEL_KEY = "SWAMP_CURRENT_LEVEL";
    const string PLAYER_CURRENT_LEVEL_KEY = "PLAYER_CURRENT_LEVEL";
    
    public static int maxLevel = 5;
    
    public int rabbitLevel;
    public int crocLevel;
    public int foxLevel;
    public int penguinLevel;
    public int duckLevel;
    public int thunderbirdLevel;
    public int badgerLevel;
    public int spikeLevel;
    public int fireLevel;
    public int swampLevel;

    public SaveMenuTalent()
    {
        if (ES3.KeyExists(RABBIT_CURRENT_LEVEL_KEY))
        {
            rabbitLevel = ES3.Load<int>(RABBIT_CURRENT_LEVEL_KEY);
        }
        else
        {
            rabbitLevel = 1;
        }

        if (ES3.KeyExists(CROC_CURRENT_LEVEL_KEY))
        {
            crocLevel = ES3.Load<int>(CROC_CURRENT_LEVEL_KEY);
        }
        else
        {
            crocLevel = 1;
        }
        
        if (ES3.KeyExists(FOX_CURRENT_LEVEL_KEY))
        {
            foxLevel = ES3.Load<int>(FOX_CURRENT_LEVEL_KEY);
        }
        else
        {
            foxLevel = 1;
        }

        if (ES3.KeyExists(PENGUIN_CURRENT_LEVEL_KEY))
        {
            penguinLevel = ES3.Load<int>(PENGUIN_CURRENT_LEVEL_KEY);
        }
        else
        {
            penguinLevel = 1;
        }

        if (ES3.KeyExists(DUCK_CURRENT_LEVEL_KEY))
        {
            duckLevel = ES3.Load<int>(DUCK_CURRENT_LEVEL_KEY);
        }
        else
        {
            duckLevel = 1;
        }

        if (ES3.KeyExists(THUNDERBIRD_CURRENT_LEVEL_KEY))
        {
            thunderbirdLevel = ES3.Load<int>(THUNDERBIRD_CURRENT_LEVEL_KEY);
        }
        else
        {
            thunderbirdLevel = 1;
        }

        if (ES3.KeyExists(SPIKE_CURRENT_LEVEL_KEY))
        {
            spikeLevel = ES3.Load<int>(SPIKE_CURRENT_LEVEL_KEY);
        }
        else
        {
            spikeLevel = 1;
        }

        if (ES3.KeyExists(FIRE_CURRENT_LEVEL_KEY))
        {
            fireLevel = ES3.Load<int>(FIRE_CURRENT_LEVEL_KEY);
        }
        else
        {
            fireLevel = 1;
        }

        if (ES3.KeyExists(SWAMP_CURRENT_LEVEL_KEY))
        {
            swampLevel = ES3.Load<int>(SWAMP_CURRENT_LEVEL_KEY);
        }
        else
        {
            swampLevel = 1;
        }
    }

    public void Save()
    {
        ES3.Save(RABBIT_CURRENT_LEVEL_KEY, rabbitLevel);
        ES3.Save(CROC_CURRENT_LEVEL_KEY, crocLevel);
        ES3.Save(FOX_CURRENT_LEVEL_KEY, foxLevel);
        ES3.Save(PENGUIN_CURRENT_LEVEL_KEY, penguinLevel);
        ES3.Save(DUCK_CURRENT_LEVEL_KEY, duckLevel);
        ES3.Save(THUNDERBIRD_CURRENT_LEVEL_KEY, thunderbirdLevel);
        ES3.Save(BADGER_CURRENT_LEVEL_KEY, badgerLevel);
        ES3.Save(SPIKE_CURRENT_LEVEL_KEY, spikeLevel);
        ES3.Save(FIRE_CURRENT_LEVEL_KEY, fireLevel);
        ES3.Save(SWAMP_CURRENT_LEVEL_KEY, swampLevel);
    }

    public void Load()
    {
        ES3.Load(RABBIT_CURRENT_LEVEL_KEY, rabbitLevel);    
        ES3.Load(CROC_CURRENT_LEVEL_KEY, crocLevel);
        ES3.Load(FOX_CURRENT_LEVEL_KEY, foxLevel);
        ES3.Load(PENGUIN_CURRENT_LEVEL_KEY, penguinLevel);
        ES3.Load(DUCK_CURRENT_LEVEL_KEY, duckLevel);
        ES3.Load(THUNDERBIRD_CURRENT_LEVEL_KEY, thunderbirdLevel);
        ES3.Load(BADGER_CURRENT_LEVEL_KEY, badgerLevel);
        ES3.Load(SPIKE_CURRENT_LEVEL_KEY, spikeLevel);
        ES3.Load(FIRE_CURRENT_LEVEL_KEY, fireLevel);
        ES3.Load(SWAMP_CURRENT_LEVEL_KEY, swampLevel);  
    }
}
