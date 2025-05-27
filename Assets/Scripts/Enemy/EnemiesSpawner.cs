    using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float _spawnTime = 5f;
    private Vector3 topScreenPosition;
    public static EnemiesSpawner instance;

    private ObjectPool _enemyPool;
    private ObjectPool EnemyPool
    {
        get
        {
            if (_enemyPool == null)
            {
                _enemyPool = GetComponent<ObjectPool>();
            }
            return _enemyPool;
        }
        set
        {
            _enemyPool = value;
        }
    }
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
    private void Start()
    {
        topScreenPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1, 0)) ;
        InvokeRepeating("SpawnEnemy", 0, _spawnTime);
    }

    public void SpawnEnemy(int id = -1)
    {
        Enemy enemy = EnemiesController.instance.CreateEnemy(id);
        if (enemy != null)
        {
            enemy.gameObject.SetActive(true);
            enemy.transform.position = MyMath.RandomVector(topScreenPosition, 4, 1);
            enemy.Init();
            enemy.SetTarget(target);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            Enemy enemy = EnemiesController.instance.CreateEnemy();
            if (enemy != null)
            {
                enemy.gameObject.SetActive(true);
                enemy.transform.position = MouseController.instance.GetMouseWorldPosition();
                enemy.Init();
                enemy.SetTarget(target);
            }
        }
    }
}
