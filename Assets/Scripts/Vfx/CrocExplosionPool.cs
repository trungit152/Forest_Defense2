using UnityEngine;
public class CrocExplosionPool : ObjectPool
{
    protected override void Start()
    {
        if (parentTransform == null)
        {
            SetParentTransform(GameObject.Find("VFXPool").transform);
        }
        base.Start();
    }
}
