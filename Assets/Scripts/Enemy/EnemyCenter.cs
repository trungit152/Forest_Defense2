using UnityEngine;

public class EnemyCenter : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;

    public GameObject GetEnemy()
    {
        return _enemy;
    }
}
