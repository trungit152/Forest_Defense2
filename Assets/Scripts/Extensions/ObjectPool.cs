using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected GameObject _objectPref;
    [SerializeField] protected GameObject _rangePref;
    [SerializeField] protected GameObject _melePref;
    [SerializeField] protected int _poolSize = 20;
    [SerializeField] protected Transform parentTransform;

    protected List<GameObject> _objectPool = new List<GameObject>();

    protected virtual void Start()
    {
        if (parentTransform == null)
        {
            var parent = GameObject.Find("DroppedItems").transform;
            if (parent != null)
            {
                SetParentTransform(parent);
            }
        }
        for (int i = 0; i < _poolSize; i++)
        {
            var obj = Instantiate(_objectPref, parentTransform);
            obj.SetActive(false);
            _objectPool.Add(obj);
        }
    }
    public void ResetPool()
    {
        _objectPool.Clear();
        if (parentTransform == null)
        {
            var parent = GameObject.Find("DroppedItems").transform;
            if (parent != null)
            {
                SetParentTransform(parent);
            }
        }
        for (int i = 0; i < _poolSize; i++)
        {
            var obj = Instantiate(_objectPref, parentTransform);
            obj.SetActive(false);
            _objectPool.Add(obj);
        }
    }
    public virtual GameObject GetObject(Vector3 position)
    {
        foreach (var obj in _objectPool)
        {
            if (!obj.activeSelf)
            {
                obj.transform.position = position;
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    protected void SetParentTransform(Transform parent = null)
    {
        parentTransform = parent;
    }
    public void ChangeWeapon(int type)
    {
        if (type == 0)
        {
            _objectPref = _rangePref;
        }
        else if (type == 1)
        {
            _objectPref = _melePref;
        }
        else
        {
            Debug.LogError("Invalid weapon type");
            return;
        }
        ResetPool();
    }
}
