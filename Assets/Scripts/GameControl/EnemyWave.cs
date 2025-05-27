using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyWave
{
    public float totalTime;
    [Header("Turn amount of wave")]
    public List<WaveData> waveData;

    public void CaculateTotalTime()
    {
        if (waveData.Count > 0) { totalTime = waveData[waveData.Count - 1].turnTime; }
    }
}

[Serializable]
public class WaveData
{
    public float turnTime;
    public int enemyAmount;
    public Enemy enemy;
    public bool isSpawned = false;
    public bool isBoss = false;
}