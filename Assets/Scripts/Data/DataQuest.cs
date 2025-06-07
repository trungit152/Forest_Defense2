using DamageNumbersPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataQuest
{
    public int QuestId;
    public int KillAmonnt;
    public int Reward;

    public static List<DataQuest> listData;
    public static List<DataQuest> GetListData()
    {
        if(listData == null || listData.Count == 0)
        {
            listData = new List<DataQuest>();
            //get data
            TextAsset jsonFile = Resources.Load<TextAsset>("DataQuest");
            if (jsonFile != null)
            {
                listData = new List<DataQuest>(JsonHelper.FromJson<DataQuest>(jsonFile.text));
            }
            else
            {
                Debug.Log("not found quest file");
            }
        }
        return listData;
    }

    public static DataQuest GetData(int id)
    {
        List<DataQuest> list = GetListData();
        foreach (DataQuest d in list)
            if (d.QuestId == id)
                return d;
        return null;
    }
}
