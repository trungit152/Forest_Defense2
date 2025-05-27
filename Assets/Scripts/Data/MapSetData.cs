using Unity.VisualScripting;
using UnityEngine;

public class MapSetData : MonoBehaviour
{
    [SerializeField] private GameObject _map1;
    [SerializeField] private GameObject _map2;
    [SerializeField] private GameObject _map3;
    [SerializeField] private GameObject _map4;
    [SerializeField] private GameObject _map5;
    [SerializeField] private GameObject _map6;
    [SerializeField] private GameObject _map7;
    [SerializeField] private GameObject _map8;
    [SerializeField] private GameObject _map9;
    [SerializeField] private GameObject _map10;
    [SerializeField] private GameObject _map11;
    [SerializeField] private GameObject _map12;
    [SerializeField] private GameObject _map13;

    public static MapSetData instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public GameObject GetMap(int id)
    {
        switch (id)
        {
            case 1:
                return _map1;
            case 2:
                return _map2;
            case 3:
                return _map3;
            case 4:
                return _map4;
            case 5:
                return _map5;
            case 6:
                return _map6;
            case 7:
                return _map7;
            case 8:
                return _map8;
            case 9:
                return _map9;
            case 10:
                return _map10;
            case 11:
                return _map11;
            case 12:
                return _map12;
            case 13:
                return _map13;
            default:
                return _map1;
        }
    }
}
