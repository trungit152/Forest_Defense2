using UnityEngine;
public class AttackImpactPool : ObjectPool
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
