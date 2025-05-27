using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataTalent
{
    public int TalentId;
    public int TurretId;
    public int[] eId;
    public float[] eValue;
    public string Description;

    public static List<DataTalent> listData;

    public static List<DataTalent> GetListData()
    {
        if (listData == null || listData.Count == 0)
        {
            listData = new List<DataTalent>();
            //get data
            TextAsset jsonFile = Resources.Load<TextAsset>("ObjectTalent");
            if (jsonFile != null)
            {
                listData = new List<DataTalent>(JsonHelper.FromJson<DataTalent>(jsonFile.text));
            }   
            else
            {
                Debug.Log("not found talent file");
            }
        }
        StandardlizeData();
        return listData;
    }

    public static DataTalent GetData(int id)
    {
        List<DataTalent> list = GetListData();
        foreach (DataTalent d in list)
            if (d.TalentId == id)
                return d;
        return null;
    }

    public static List<DataTalent> GetListDataByTurret(int[] id)
    {
        List<DataTalent> inputList = new List<DataTalent>();
        List<DataTalent> resList = new List<DataTalent>();
        if (listData == null)
        {
            inputList = GetListData();
        }
        else
        {
            inputList = listData;
        }
        foreach(var data in inputList)
        {
            foreach (var i in id)
            {
                if (data.TurretId == i)
                {
                    resList.Add(data);
                }
            }
        }
        return resList;
    }
    private static void StandardlizeData()
    {
        foreach (var data in listData)
        {
            if (data.eValue != null) 
            {
                if (data.eValue.Length == 1)
                {
                    data.Description = String.Format(data.Description, data.eValue[0]);
                }
                else if (data.eValue.Length == 2)
                {
                    data.Description = String.Format(data.Description, data.eValue[0], data.eValue[1]);
                }
                else if (data.eValue.Length == 3)
                {
                    data.Description = String.Format(data.Description, data.eValue[0], data.eValue[1], data.eValue[2]);
                }
            }
        }
    }
}
