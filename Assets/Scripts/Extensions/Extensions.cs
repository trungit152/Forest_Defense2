using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T GetCloneByID<T>(this List<T> list, int id, Transform papa = null) where T : IdComponent
    {
        if (list == null || list.Count < 1) return null;
        Transform parent = papa == null ? list[0].transform.parent : papa;
        T prefab = null;
        for (int i = 0; i < list.Count; ++i)
        {
            if (list[i].ID != id) continue;
            prefab = list[i];
            if (list[i].gameObject.scene.name != null)
            {
                if (!list[i].gameObject.activeSelf)
                {
                    list[i].transform.SetParent(parent);
                    return list[i];
                }
            }
        }

        if (prefab == null) return null;

        T obj = UnityEngine.Object.Instantiate(prefab, parent);
        obj.gameObject.SetActive(false);
        list.Add(obj);
        return obj;
    }
    public static void Refresh<T>(this List<T> list) where T : Component
    {
        for (int i = 0; i < list.Count; ++i)
        {
            // list[i].transform.DOKill(false);
            list[i].gameObject.SetActive(false);
        }
    }
}
