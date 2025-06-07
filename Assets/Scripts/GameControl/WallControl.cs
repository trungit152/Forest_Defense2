using EasyUI.Toast;
using UnityEngine;

public class WallControl : MonoBehaviour
{
    [SerializeField] private Sprite _wall_100hp;
    [SerializeField] private Sprite _wall_75hp;
    [SerializeField] private Sprite _wall_50hp;
    [SerializeField] private Sprite _wall_25hp;
    [SerializeField] private float _maxHp = 1000;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _hpBar;
    public float _hp;
    private Vector2 _hpBarSize;
    private bool _isLose;
    private int _star;
    public int Star { get => _star;} 
    public static WallControl instance;

    private void Awake()
    {
        if (instance != null && instance != this)
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
        Init();
    }
    private void Init()
    {
        _star = 3;
        _hp = _maxHp;
        _spriteRenderer.sprite = _wall_100hp;
        _hpBarSize = _hpBar.size;
    }

    public void Attacked(float dmg)
    {
        _animator.SetTrigger("hit");
        if (_hp > 0)
        {
            _hp -= dmg;
            _hpBar.size = new Vector2(_hp / _maxHp * _hpBarSize.x, _hpBarSize.y);
        }
        else if (_hp <= 0 && !_isLose)
        {
            InGameUI.instance.ShowLosePanel();
            _hp = 0;
            _hpBar.size = new Vector2(_hp / _maxHp * _hpBarSize.x, _hpBarSize.y);
            _isLose = true;
        }

        Sprite newSprite = null;

        if (_hp / _maxHp < 0.25f)
        {
            _star = 0;
            newSprite = _wall_25hp;
        }
        else if (_hp / _maxHp < 0.5f)
        {
            _star = 1;
            newSprite = _wall_50hp;
        }
        else if (_hp / _maxHp < 0.75f)
        {
            _star = 2;
            newSprite = _wall_75hp;
        }
        if (newSprite != null && _spriteRenderer.sprite != newSprite)
        {
            //Handheld.Vibrate();
            _spriteRenderer.sprite = newSprite;
        }
    }
}
