using UnityEngine;
public class Bombs : MonoBehaviour
{
    [SerializeField] private Transform _shadow;
    [SerializeField] private GameObject _bompSphere;
    [SerializeField] private Transform _visual;
    private float _damage;
    private float elapsedTime = 0;
    private bool isMoving = false;
    private Vector2 startPos, endPos;
    private float height, duration;
    private float _scaleSize;
    private float _rotationSpeed;
    private bool _isCrit; 
    private ObjectPool _objectPool;
    public ObjectPool ObjectPool
    {
        get
        {
            if (_objectPool == null)
            {
                _objectPool = GetComponent<ObjectPool>();
            }
            return _objectPool;
        }
        set
        {
            _objectPool = value;
        }
    }
    private CrocExplosionPool _crocExplosionPool;
    public CrocExplosionPool CrocExplosionPool
    {
        get
        {
            if (_crocExplosionPool == null)
            {
                _crocExplosionPool = GetComponent<CrocExplosionPool>();
            }
            return _crocExplosionPool;
        }
        set
        {
            _crocExplosionPool = value;
        }
    }
    private BlackHolePool _blackHolePool;
    public BlackHolePool BlackHolePool
    {
        get
        {
            if (_blackHolePool == null)
            {
                _blackHolePool = GetComponent<BlackHolePool>();
            }
            return _blackHolePool;
        }
        set
        {
            _blackHolePool = value;
        }
    }
    public void StartParabolMove(Vector2 start, Vector2 end)
    {
        _visual.rotation = Quaternion.identity;
        startPos = start;
        endPos = end;
        CheckReasonableStat(start, end);
        elapsedTime = 0;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving) 
        {
            elapsedTime += Time.deltaTime * GameStat.gameTimeScale;
            float t = elapsedTime / duration;

            if (t >= 1)
            {
                transform.position = endPos;
                _shadow.transform.position = transform.position;
                isMoving = false;
                TakeDamage();
                gameObject.SetActive(false);
                return;
            }

            float x = Mathf.Lerp(startPos.x, endPos.x, t);
            float y = Mathf.Lerp(startPos.y, endPos.y, t) + height * Mathf.Sin(t * Mathf.PI);
            MoveShadow(t);
            ScaleSize(t);
            Rotate();
            transform.position = new Vector2(x, y);
        }
    }
    private void MoveShadow(float t)
    {
        float x = Mathf.Lerp(startPos.x, endPos.x, t);
        float y = Mathf.Lerp(startPos.y - 1f, endPos.y, t);
        _shadow.position = new Vector2(x, y);
    }
    private void Rotate()
    {
        float zRotation = _rotationSpeed * Time.deltaTime * GameStat.gameTimeScale;
        _visual.Rotate(0, 0, zRotation);
    }

    private void ScaleSize(float t)
    {
        float scale;
        if (t < 0.5f)  
        {
            scale = Mathf.Lerp(1f, _scaleSize, t * 2); 
        }
        else 
        {
            scale = Mathf.Lerp(_scaleSize, 1f, (t - 0.5f) * 2);
        }

        transform.localScale = new Vector3(scale, scale, 1);
    }

    private void CheckReasonableStat(Vector2 start, Vector2 end)
    {
        float distance = Vector2.Distance(start, end);
        duration = Mathf.Lerp(0.2f, 1f, distance / 4f);
        height = Mathf.Lerp(0.2f, 1f, distance / 4f);
        _scaleSize = Mathf.Lerp(1.1f, 1.2f, height);
        if (Mathf.Abs(start.x - end.x) < 2.5) 
        {
            _rotationSpeed = 120f;
        }
        else
        {
            _rotationSpeed = 360f;
        }
    }
    private void TakeDamage()
    {
        AudioManager.instance.PlaySoundEffect("Explode");
        var shpereCast = ObjectPool.GetObject(transform.position);
        var shpereCastComponent = shpereCast.GetComponent<BombSphereCast>();
        if (shpereCastComponent != null)
        {
            shpereCastComponent.SetDamage(_damage, _isCrit);
        }
        CrocExplosionPool.GetObject(transform.position);
        BlackHolePool.GetObject(transform.position);
    }
    public void SetDamage(float damage, bool isCrit)
    {
        _damage = damage;
        _isCrit = isCrit;
    }
}
