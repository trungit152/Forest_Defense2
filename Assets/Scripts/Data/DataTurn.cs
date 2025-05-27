using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataTurn
{
    public int TurnId;
    public int[] EnemyId;
    public int[] DelayTime;
    public string[] Pos;

    public static List<DataTurn> listData;
    public static List<DataTurn> GetListData()
    {
        if (listData == null || listData.Count == 0)
        {
            listData = new List<DataTurn>();
            //get data
            TextAsset jsonFile = Resources.Load<TextAsset>("TurnData");
            if (jsonFile != null)
            {
                listData = new List<DataTurn>(JsonHelper.FromJson<DataTurn>(jsonFile.text));
            }
            else
            {
                Debug.LogError("not found turn");
            }
            //
        }
        return listData;
    }

    public static DataTurn GetData(int id)
    {
        List<DataTurn> list = GetListData();
        foreach (DataTurn d in list)
            if (d.TurnId == id)
                return d;
        return null;
    }
}
