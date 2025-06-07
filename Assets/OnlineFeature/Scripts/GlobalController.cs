using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GlobalController
{
    public static ModeGame CurrentModeGame;
    
    public static Action<int> OnEnemyDie;

    public enum ModeGame
    {
        ModeOffline,
        ModeOnline,
    }
}
