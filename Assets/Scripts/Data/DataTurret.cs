using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataTurret
{
    public int TurretId;
    public string Name;
    public int[] MergeLevel;
    public string Size;
    public Vector2 SizeStandard;
    public int ATKType;
    public int Target;
    public float Radius;
    public float Range;
    public float[] ATK;
    public float[] ATKS;
    public float[] CD;

    public static List<DataTurret> listData;
    public static List<DataTurret> GetListData()
    {
        if (listData == null || listData.Count == 0)
        {
            listData = new List<DataTurret>();
            //get data
            TextAsset jsonFile = Resources.Load<TextAsset>("StatTurret");
            if (jsonFile != null)
            {
                listData = new List<DataTurret>(JsonHelper.FromJson<DataTurret>(jsonFile.text));
            }
            else
            {
                Debug.LogError("not found turn");
            }
            //
        }
        StandardlizeStat(listData);
        return listData;
    }

    public static DataTurret GetData(int id)
    {
        List<DataTurret> list = GetListData();
        foreach (DataTurret d in list)
            if (d.TurretId == id)
                return d;
        return null;
    }
    public static void StandardlizeStat(List<DataTurret> list)
    {
        foreach (var turret in list)
        {
            //size
            float x = turret.Size[0];
            float y = turret.Size[2];
            turret.SizeStandard = new Vector2(x, y);
            //ATKS
            turret.CD = new float[turret.ATKS.Length];
            for (int i = 0; i < turret.ATKS.Length; i++)
            {
                turret.CD[i] = 1/turret.ATKS[i];
            }
        }
    }
}
