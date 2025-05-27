using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class DataWave
{
    public int Level;
    public int WaveId;
    public int WaveType;
    public int[] TurnId;
    public int Delay;

    static List<DataWave> listData;
    public static List<DataWave> GetListData()
    {
        if (listData == null || listData.Count == 0)
        {
            listData = new List<DataWave>();
            //get data
            TextAsset jsonFile = Resources.Load<TextAsset>("WaveData");
            if (jsonFile != null)
            {
                listData = new List<DataWave>(JsonHelper.FromJson<DataWave>(jsonFile.text));
            }
            else
            {
                Debug.LogError("not found wave");
            }
            //
        }
        return listData;
    }
    public static List<DataWave> GetListData(int waveType)
    {
        List<DataWave> list = GetListData();
        List<DataWave> result = new List<DataWave>();
        foreach (DataWave d in list)
            if (d.WaveType == waveType)
                result.Add(d);
        return result;
    }
    public static DataWave GetData(int id)
    {
        List<DataWave> list = GetListData();
        foreach (DataWave d in list)
            if (d.WaveId == id)
                return d;
        return null;
    }
    public static int GetLevelCount()
    {
        List<DataWave> list = GetListData();
        return list.Select(d => d.Level).Distinct().Count();
    }
    public static List<DataWave> GetWaveByLevel(int level)
    {
        List<DataWave> list = GetListData();
        List<DataWave> result = new List<DataWave>();
        foreach (DataWave d in list)
        {
            if (d.Level == level)
                result.Add(d);
        }
        return result;
    }

}
