using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SaveQuest
{
    const string ENEMY_KILLED_IN_QUEST = "ENEMY_KILLED_IN_QUEST";

    public int _enemyKilled;
    public int _enemyKilledInGame;

    public SaveQuest()
    {
        _enemyKilled = ES3.Load(ENEMY_KILLED_IN_QUEST, 0);
    }

    public void Load()
    {
        _enemyKilled = ES3.Load(ENEMY_KILLED_IN_QUEST, 0);
    }

    public void Save()
    {
        ES3.Save(ENEMY_KILLED_IN_QUEST, _enemyKilled);
    } 

}
