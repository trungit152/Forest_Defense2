using UnityEngine;

public class BlackHolePool : ObjectPool
{
    protected override void Start()
    {
        if (parentTransform == null)
        {
            SetParentTransform(GameObject.Find("VFXPool").transform);
        }
        base.Start();
    }
    public override GameObject GetObject(Vector3 position)
    {
        foreach (var obj in _objectPool)
        {
            if (!obj.activeSelf)
            {
                obj.transform.position = position;
                obj.SetActive(true);
                var blackHole = obj.GetComponent<BlackHole>();
                if(blackHole != null)
                {
                    blackHole.FadeOut();
                }
                return obj;
            }
        }
        return null;
    }
}
