using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Enemy : IdComponent
{
    protected string _enemyName;
    [SerializeField] protected float _maxHp;
    [SerializeField] private float _speed;
    [SerializeField] private EnemyDropItem _enemyDropItem;
    [SerializeField] private DamageNumber _damageNumber;
    [SerializeField] private bool _numberBounceRight;
    [SerializeField] private SpriteRenderer _healthBar;
    private Vector2 _healthBarSize;
    [SerializeField] private Trait _trait;
    [SerializeField] private GameObject _iceBox;
    [SerializeField] private GameObject _shock;
    [SerializeField] private GameObject _attackImpact;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] protected Transform _center;
    [SerializeField] protected bool _isBoss;
    protected float _hp;
    private EnemyAI _enemyAI;
    private bool _isDie;
    private float _cardPercent;
    private float _expPercent;
    public int _coin;
    private bool _isBurnted;
    private Coroutine _burntCoroutine;
    private Coroutine _burntSpriteCoroutine;

    private bool _isFreeze;
    private Coroutine _freezeCoroutine;

    private bool _isShock;
    private Coroutine _shockCoroutine;
    private Coroutine _shockShaderCoroutine;

    private bool _isStun;
    private Coroutine _stunCoroutine;

    private bool _isCardFly;
    public enum Trait
    {
        Wood,
        Iron,
        Water
    }
    public EnemyAI EnemyAI
    {
        get
        {
            if(_enemyAI == null)
            {
                _enemyAI = GetComponent<EnemyAI>();
            }
            return _enemyAI;
        }
        set
        {
            _enemyAI = value;
        }
    }
    private EnemyAnimation _enemyAnimation;
    public EnemyAnimation EnemyAnimation
    {
        get
        {
            if (_enemyAnimation == null)
            {
                _enemyAnimation = GetComponent<EnemyAnimation>();
            }
            return _enemyAnimation;
        }
        set
        {
            _enemyAnimation = value;
        }
    }
    private EnemyChase _enemyChase;
    public EnemyChase EnemyChase
    {
        get
        {
            if (_enemyChase == null)
            {
                _enemyChase = GetComponent<EnemyChase>();
            }
            return _enemyChase;
        }
        set
        {
            _enemyChase = value;
        }
    }

    private EnemyKnockBack _enemyKnockBack;
    public EnemyKnockBack EnemyKnockBack
    {
        get
        {
            if(_enemyKnockBack == null)
            {
                _enemyKnockBack = GetComponent<EnemyKnockBack>();
            }
            return _enemyKnockBack;
        }
        set
        {
            _enemyKnockBack = value;
        }
    }
    private EnemyAttack _enemyAttack;
    public EnemyAttack EnemyAttack
    {
        get
        {
            if (_enemyAttack == null)
            {
                _enemyAttack = GetComponent<EnemyAttack>();
            }
            return _enemyAttack;
        }
        set
        {
            _enemyAttack = value;
        }
    }
    private void Awake()
    {
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        if (col != null)
        {
            col.offset = _center.localPosition;
        }
        if (_healthBar != null)
        {
            _healthBarSize = _healthBar.size;
        }
    }
    public void Init()
    {
        StopAllCoroutines();
        ResetShockShader();
        ResetBurntSprite();
        _expPercent = 100;
        if(_center == null)
        {
            _center = transform;
        }
        _iceBox.SetActive(false);
        _shock.SetActive(false);
        _attackImpact.SetActive(false);
        _isCardFly = true;
        _isDie = false;
        _isBurnted = false;
        _isShock = false;
        _isFreeze = false;
        EnemyAI.SetSpeed(_speed);
        EnemyAI.Init();
        _hp = _maxHp;
        _cardPercent = 0;
        if (_healthBar != null)
        {
            _healthBar.transform.parent.gameObject.SetActive(false);
        }
        UpdateHpBar();
        EnemyChase.Init();
        EnemyKnockBack.Init();
    }
    public void InitStat(string name, float hp, float damage, float cd, float exp, float speed, int coin)
    {
        _enemyName = name;
        _maxHp = hp;
        EnemyAttack.Init(damage, cd);
        _enemyDropItem.Init(exp);
        _speed = speed;
        _coin = coin;
    }
    public void InitStat(DataEnemy dataEnemy)
    {
        _enemyName = dataEnemy.Name;
        _maxHp = dataEnemy.HP[0];
        EnemyAttack.Init(dataEnemy.ATK[0], dataEnemy.CD[0]);
        _enemyDropItem.Init(dataEnemy.EXP[0]);
        _speed = dataEnemy.MS;
        _coin = dataEnemy.Coin;
    }
    public void SetTarget(Transform target)
    {
        EnemyAI.SetTarget(target);
    }
    public void SetTarget(List<Transform> targets)
    {
        float minDistance = float.MaxValue;
        int index = -1;
        foreach (Transform t in targets)
        {
            var offset = Mathf.Abs(transform.position.x - t.position.x);
            if (minDistance >= offset)
            {
                minDistance = offset;
                index = targets.IndexOf(t);
            }
        }
        EnemyAI.SetTarget(targets[index]);
    }
    public void LoseHealth(float damage,bool isPlayAnim = true)
    {
        if(_healthBar!=null) _healthBar.transform.parent.gameObject.SetActive(true);
        _hp -= damage;
        UpdateHpBar();
        //custom number
        if (_damageNumber != null)
        {
            DamageNumber damageNumber = _damageNumber;
            damageNumber.Spawn(_center.position + new Vector3(0, 0.5f), damage);
        }

        //
        if ( _hp > 0 && isPlayAnim && !EnemyAnimation.IsPlayingHurt())
        {
            EnemyAnimation.SetHurt();
        }
        else if (_hp <= 0)
        {
            StopAllCoroutines();
            EnemyAnimation.EnabledAnimator(true);
            EnemyAnimation.SetDie();
            if (_healthBar != null)
            {
                _healthBar.transform.parent.gameObject.SetActive(false);
            }
        }
    }
    public void BurntHealth(float damagePerHalfSecond, float time)
    {
        if (_isBurnted && _burntCoroutine!= null)
        {
            StopCoroutine(_burntCoroutine);
        }
        if (_isBurnted && _burntSpriteCoroutine != null)
        {
            StopCoroutine(_burntSpriteCoroutine);
        }
        _burntCoroutine = StartCoroutine(BurntCoroutine(damagePerHalfSecond, time));
        _burntSpriteCoroutine = StartCoroutine(BurntSprite(time));
    }
    private IEnumerator BurntCoroutine(float damagePerHalfSecond, float time)
    {
        _isBurnted = true;
        float elapsedTime = 0f;
        float lateTime = -0.7f;
        while (elapsedTime <= time)
        {
            elapsedTime += Time.deltaTime * GameStat.gameTimeScale;
            if (elapsedTime > lateTime + 0.5f)
            {
                LoseHealth(damagePerHalfSecond, false);
                lateTime += 0.5f;
            }
            yield return null;
        }
        _isBurnted = false;
    }
    public IEnumerator BurntSprite(float time)
    {
        if(_spriteRenderer != null)
        {
            var color = _spriteRenderer.color;

            float elapsedTime = 0;
            float duration = time;
            float min = 0.5f;
            float max = 0.8f;
            float speed = 15f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float colorG = Mathf.Lerp(min , max, Mathf.PingPong(elapsedTime * speed, 1));
                color.g = colorG;
                _spriteRenderer.color = color;
                yield return null;
            }
            color.g = 1;
            _spriteRenderer.color = color;
        }       
    }
    private void ResetBurntSprite()
    {
        if (_spriteRenderer != null)
        {
            var color = _spriteRenderer.color;
            color.g = 1;
            _spriteRenderer.color = color;
        }
    }
    public void Freeze(float time)
    {
        if (_isFreeze && _freezeCoroutine != null)
        {
            StopCoroutine(_freezeCoroutine);
        }
        _freezeCoroutine = StartCoroutine(FreezeCroutine(time));
    }
    private IEnumerator FreezeCroutine(float time)
    {
        float elapsedTime = 0f;
        StartFreeze();
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime*GameStat.gameTimeScale;
            yield return null;
        }
        EndFreeze();
    }
    private void StartFreeze()
    {
        _isFreeze = true;
        _iceBox.SetActive(true);
        EnemyAnimation.EnabledAnimator(false);
        EnemyAI.SetSpeed(0);
    }
    private void EndFreeze()
    {
        _iceBox.SetActive(false);
        EnemyAnimation.EnabledAnimator(true);
        EnemyAI.SetSpeed(_speed);
        _isFreeze = false;
    }
    public void Shock(float time = 0.4f)
    {
        if (_isShock && _shockCoroutine != null)
        {
            StopCoroutine(_shockCoroutine);
        }
        if(_isShock && _shockShaderCoroutine != null)
        {
            StopCoroutine(_shockShaderCoroutine);
        }

        _shockCoroutine = StartCoroutine(ShockCoroutine(time));
        _shockShaderCoroutine = StartCoroutine(ShockShaderCoroutine(time));
    }
    private IEnumerator ShockCoroutine(float time)
    {
        float elapsedTime = 0f;
        _isShock = true;
        _shock.SetActive(true);
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime*GameStat.gameTimeScale;
            yield return null;
        }
        _isShock = false;
        _shock.SetActive(false);
    }

    public void Stun(float time)
    {
        Debug.Log("stunned");
        if (_isStun && _stunCoroutine != null)
        {
            StopCoroutine(_stunCoroutine);
        }
        _stunCoroutine = StartCoroutine(StunCoroutine(time));
    }
    public virtual void Die()
    {
        GlobalController.OnEnemyDie?.Invoke(_coin);
        Debug.Log("Enemy die: " + _coin);
        if(GameController.instance != null)
        {
            GameController.instance.AddPoint(_coin);
        }
        _enemyDropItem.DropItem(_cardPercent,_expPercent, _isCardFly);
        StopAllCoroutines();
        EnemiesController.instance.RemoveDeathEnemy(this);
        if(WaveManager.instance != null && WaveManager.instance.IsEndGame() 
            && EnemiesController.instance != null && EnemiesController.instance.GetAliveEnemyCount() <= 0)
        {
            InGameUI.instance.ShowWinPanel(WallControl.instance.Star);
        }
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            var swordDame = collision.GetComponent<SwordSlashes>();
            if (swordDame != null)
            {
                LoseHealth(swordDame.GetDamage());
                EnemyKnockBack.ApplyKnockback(swordDame.gameObject.transform.position);
            }
        }
    }
    public virtual void UpdateHpBar()
    {
        if (_healthBar != null)
        _healthBar.size = new Vector2((_hp>0?_hp:0) / _maxHp * _healthBarSize.x, _healthBarSize.y);
    }
    public void SetIdle()
    {
        EnemyAnimation.SetIdle();
    }
    public void SetStatic()
    {
        _isDie = true;
        EnemyAI.SetSpeed(0);
    }
    public bool IsDie()
    {
        return _isDie;
    }
    public Trait GetTrait()
    {
        return _trait;
    }
    public void ChangeCardPercent(float value)
    {
        _cardPercent = value;
    }
    public Enemy GetNearlyEnemyToShock()
    {
        return EnemiesController.instance.GetNearlyEnemyToShock(transform.position);
    }
    public bool IsShock()
    {
        return _isShock;
    }
    public void SetShock(bool b = true)
    {
        _isShock = b;
    }
    public void TakeDamage()
    {
        EnemyAttack.TakeDamage();
        ShowAttackImpact();
    }

    private void ShowAttackImpact()
    {
        _attackImpact.SetActive(true);
    }
    public Transform GetPlayerTransform()
    {
        return EnemyChase.GetPlayerTransform();
    }
    public void SpawnBullet()
    {
        EnemyAttack.SpawnBullet();
    }
    public void SetCanmove(bool b = true)
    {
        EnemyAI.SetCanMove(b);
    }
    public void SlowDown(float percent, float animSpeed = -1)
    {
        if(animSpeed == -1)
        {
            animSpeed = (100-percent);
        }
        EnemyAnimation.ChangeAnimationSpeed(animSpeed);
        EnemyAI.SetSpeed(_speed - _speed*percent);
    }
    public void SetBasicSpeed()
    {
        EnemyAnimation.ChangeAnimationSpeed(1);
        EnemyAI.SetSpeed(_speed);
    }
    public IEnumerator StunCoroutine(float time)
    {
        EnemyAnimation.EnabledAnimator(false);
        EnemyAI.SetSpeed(0);
        yield return new WaitForSeconds(time);
        EnemyAnimation.EnabledAnimator(true);
        EnemyAI.SetSpeed(_speed);
    }
    public IEnumerator ShockShaderCoroutine(float time)
    {
        if (_spriteRenderer != null)
        {
            var mat = _spriteRenderer.material;
            if (mat != null)
            {
                float elapsedTime = 0;
                float duration = time;
                float flashMin = 0.1f;
                float flashMax = 0.4f;
                float flashSpeed = 25f;

                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    float flashValue = Mathf.Lerp(flashMin, flashMax, Mathf.PingPong(elapsedTime * flashSpeed, 1));
                    mat.SetFloat("_flash", flashValue);
                    yield return null;
                }

                mat.SetFloat("_flash", 0);
            }
        }
    }
    private void ResetShockShader()
    {
        if (_spriteRenderer != null)
        {
            var mat = _spriteRenderer.material;
            if (mat != null)
            {
                mat.SetFloat("_flash", 0);
            }
        }
    }
    public void ChangeExpPercent(float percent)
    {
        _expPercent = percent;
    }
    public float GetCurrentHp() { return _hp; }

    public void SetIsCardFly(bool b)
    {
        _isCardFly = b;
    }
    public void SetAttackImpactPosition(Vector3 pos)
    {
        _attackImpact.transform.position = pos;
    }
    public virtual void SetHpBar(RectTransform border, RectTransform fill)
    {

    }
    public Transform GetCenter()
    {
        return _center;
    }

    public IEnumerator GetDamageAnimCoroutine(float time)
    {
        if (_spriteRenderer != null)
        {
            var mat = _spriteRenderer.material;
            if (mat != null)
            {
                float elapsedTime = 0;
                float duration = time;
                float flashMin = 0.1f;
                float flashMax = 0.4f;
                float flashSpeed = 25f;

                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    float flashValue = Mathf.Lerp(flashMin, flashMax, Mathf.PingPong(elapsedTime * flashSpeed, 1));
                    mat.SetFloat("_flash", flashValue);
                    yield return null;
                }

                mat.SetFloat("_flash", 0);
            }
        }
    }
    public void FindPath()
    {
        EnemyAI.FindPath();
    }
    public bool IsBoss()
    {
        return _isBoss;
    }
    public void SetDamageNumber(DamageNumber damageNumber)
    {
        _damageNumber = damageNumber;
    }
    public void SetNumberDirection(bool isRight)
    {
        _numberBounceRight = isRight;
    }

    public void ChangeExp(float exp)
    {
        _enemyDropItem.ChangeExp(exp);
    }
}
