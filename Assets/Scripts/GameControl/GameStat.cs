using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStat
{
    public static float gameTimeScale = 1;

    public static void ChangeGameTimeScale(float t)
    {
        gameTimeScale = t;
    }
}
