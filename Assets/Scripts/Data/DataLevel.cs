using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataLevel
{
    public int Level;
    public float Exp;

    public static List<DataLevel> listData;
    public static Dictionary<int, float> levelInformation;
    public static List<DataLevel> GetListData()
    {
        if (listData == null || listData.Count == 0)
        {
            listData = new List<DataLevel>();
            //get data
            TextAsset jsonFile = Resources.Load<TextAsset>("DataLevel");
            if (jsonFile != null)
            {
                listData = new List<DataLevel>(JsonHelper.FromJson<DataLevel>(jsonFile.text));
            }
            else
            {
                Debug.Log("not found level file");
            }
        }
        StandardlizeValue();
        return listData;
    }
    public static DataLevel GetData(int level)
    {
        List<DataLevel> list = GetListData();
        foreach (DataLevel d in list)
            if (d.Level == level)
                return d;
        return null;
    }

    private static void StandardlizeValue()
    {
        levelInformation = new Dictionary<int, float>();
        foreach (var data in listData)
        {
            if (!levelInformation.ContainsKey(data.Level))
            {
                levelInformation.Add(data.Level, data.Exp);
            }
        }
    }
}
