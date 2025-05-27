using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemyList;
    [SerializeField] private List<Transform> _targets;
    [SerializeField] private EnemiesAnimator _enemiesAnimator;
    [SerializeField] private List<DataEnemy> _dataEnemyList;
    [Space(25)]
    [SerializeField] private int _testEnemyID;
    private List<Enemy> aliveEnemy;
    public static EnemiesController instance;
    private int _trapAmount = 0;
    //test
    private int _listCount;

    private void Start()
    {
        if(DataEnemy.listData == null)
        {
            _dataEnemyList = DataEnemy.GetListData();
        }
        else
        {
            _dataEnemyList = DataEnemy.listData;
        }
        SetStatEnemies();
        _listCount = enemyList.Count;
        enemyList.Refresh();
    }
    private void Awake()
    {
        aliveEnemy = new List<Enemy>();
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        Test();
    }
    public void SetStatEnemies()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            foreach (var dataEnemy in _dataEnemyList)
            {
                if (enemyList[i].ID == dataEnemy.EnemyId)
                {
                    enemyList[i].InitStat(dataEnemy);
                }
            }
        }
    }
    public void AddEnemyToList(Enemy enemy)
    {
        enemyList.Add(enemy);
    }
    public void RemoveEnemyFromlist(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }

    public List<Enemy> GetNearestEnemies(Vector2 pos, float range, int amount = 1)
    {
        return enemyList
            .Where(enemy => Vector2.Distance(enemy.transform.position, pos) < range && !enemy.IsDie() && enemy.gameObject.activeSelf)
            .OrderBy(enemy => Vector2.Distance(enemy.transform.position, pos))
            .Take(amount)
            .ToList();
    }
    public int EnemyListCount()
    {
        return enemyList.Count;
    }

    public Enemy CreateEnemy(int id = -1)
    {
        if (id < 0)
        {
            id = Random.Range(0, _listCount);
        }
        Enemy enemy = enemyList.GetCloneByID(id, transform);
        aliveEnemy.Add(enemy);
        return enemy;
    }
    public Enemy CreateEnemy(Enemy enemy)
    {
        if (enemy == null) return null;
        enemy = enemyList.GetCloneByID(enemy.ID, transform);
        aliveEnemy.Add(enemy);
        _enemiesAnimator.AddAnimator(enemy.EnemyAnimation.GetAnimator());
        return enemy;
    }

    public void RemoveDeathEnemy(Enemy enemy)
    {
        aliveEnemy.Remove(enemy);
        _enemiesAnimator.RemoveAnimator(enemy.EnemyAnimation.GetAnimator());
    }
    public void AddEnemyAnimator(Enemy enemy)
    {
        if (enemy == null) return;
        _enemiesAnimator.AddAnimator(enemy.EnemyAnimation.GetAnimator());
    }
    public int GetAliveEnemyCount()
    {
        return aliveEnemy.Count;
    }
    public Enemy GetNearlyEnemy(Vector2 from)
    {
        foreach (var enemy in aliveEnemy)
        {
            if (Vector2.Distance(from, enemy.transform.position) < 5f)
            {
                return enemy;
            }
        }
        return null;
    }
    public Enemy GetNearlyEnemyToShock(Vector2 position)
    {
        List<Enemy> validEnemies = new List<Enemy>();
        List<Enemy> backupList = new List<Enemy>();
        foreach (var enemy in aliveEnemy)
        {
            if (!enemy.IsShock() && Vector2.Distance(position, enemy.transform.position) < 5f)
            {
                if (Vector2.Distance(position, enemy.transform.position) > 2f)
                {
                    validEnemies.Add(enemy);
                }
                else
                {
                    backupList.Add(enemy);
                }
            }
        }
        if (validEnemies.Count > 0)
        {
            return validEnemies[UnityEngine.Random.Range(0, validEnemies.Count)];
        }
        else if (backupList.Count > 0)
        {
            return backupList[UnityEngine.Random.Range(0, backupList.Count)];
        }
        return null;
    }
    public void AddTrap()
    {
        _trapAmount++;
    }
    public int GetTrapAmount()
    {
        return _trapAmount;
    }
    public void FindPath()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].FindPath();
        }
    }
    public Enemy SpawnEnemy(Vector3 pos, int id = -1)
    {
        Enemy enemy = CreateEnemy(id);
        if (enemy != null)
        {
            enemy.gameObject.SetActive(true);
            enemy.transform.position = pos;
            enemy.Init();
            enemy.SetTarget(_targets);
        }
        AddEnemyAnimator(enemy);
        return enemy;
    }
    private void Test()
    {
        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < 1; i++)
            {
                SpawnEnemy(MouseController.instance.GetMouseWorldPosition(), _testEnemyID);
            }
        }
    }
}
