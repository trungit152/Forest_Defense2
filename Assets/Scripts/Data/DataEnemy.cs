using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataEnemy
{
    public int EnemyId;
    public int[] MergeLevel;
    public string Name;
    public int EnemyType;
    public float[] HP;
    public float[] ATK;
    public float[] ATKS;
    public float[] CD;
    public float[] EXP;
    public float MS;
    public int Coin;
    public string Note;

    public static List<DataEnemy> listData;
    public static List<DataEnemy> GetListData()
    {
        if (listData == null || listData.Count == 0)
        {
            listData = new List<DataEnemy>();
            //get data
            TextAsset jsonFile = Resources.Load<TextAsset>("DataEnemy");
            if (jsonFile != null)
            {
                listData = new List<DataEnemy>(JsonHelper.FromJson<DataEnemy>(jsonFile.text));
            }
            else
            {
                Debug.Log("not found enemy file");
            }
        }
        StandardlizeData();
        return listData;
    }
    public static void StandardlizeData()
    {
        foreach (var data in listData)
        {
            data.CD = new float[data.ATKS.Length];
            for (int i = 0; i < data.ATKS.Length; i++)
            {
                data.CD[i] = 1 / (data.ATKS[i] > 0 ? 1 / data.ATKS[i] : 0.1f);
            }
        }
    }
    public static DataEnemy GetData(int id)
    {
        List<DataEnemy> list = GetListData();
        foreach (DataEnemy d in list)
            if (d.EnemyId == id)
                return d;
        return null;
    }
}
