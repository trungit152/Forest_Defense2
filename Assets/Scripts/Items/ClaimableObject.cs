using System.Collections;
using UnityEngine;

public class ClaimableObject : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _knockbackDistance = 0.5f;
    [SerializeField] private float _knockbackDuration = 0.1f;
    [SerializeField] private SpriteRenderer _sr;
    private float _expAmount;
    private bool _isMovingToTarget;

    [SerializeField] private float _bounceHeight = 0.5f;  
    [SerializeField] private float _bounceDuration = 0.2f;

    private void Awake()
    {
        _target = GameObject.Find("FollowExpBarPoint").transform;
    }

    private void FixedUpdate()
    {
        if (_isMovingToTarget)
        {
            MoveToTarget();
        }
    }

    public void Init()
    {
        StartCoroutine(BounceAndStartMove());  // Bắt đầu hiệu ứng nảy trước
        transform.localScale = Vector2.one * Random.Range(0.5f, 1f);
        SetSortingOrder(3);
    }

    public void SetExpAmount(float expAmount)
    {
        _expAmount = expAmount;
    }

    public void SetSortingOrder(int value)
    {
        _sr.sortingOrder = value;
    }

    public void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime * GameStat.gameTimeScale);
        SetSortingOrder(101);
        if (Vector2.Distance(_target.position, transform.position) < 0.05f)
        {
            PlayerLevel.instance.AddClaimCoroutineToQueue(_expAmount);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator BounceAndStartMove()
    {
        // Xác định hướng nảy (trái hoặc phải ngẫu nhiên)
        float randomDirection = Random.value > 0.5f ? 1f : -1f;
        float bounceXOffset = Random.Range(0.2f, 0.5f) * randomDirection; // Khoảng lệch ngang
        float maxTime = _bounceDuration;

        // Lưu vị trí ban đầu
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + new Vector2(bounceXOffset, 0f); // Đích theo trục X

        float elapsedTime = 0f;
        while (elapsedTime < maxTime)
        {
            float t = elapsedTime / maxTime;  // Tỉ lệ thời gian (0 -> 1)
            float parabola = 4 * _bounceHeight * (t - t * t); // Công thức đường cong parabol

            // Cập nhật vị trí theo cả X và Y
            transform.position = Vector2.Lerp(startPosition, endPosition, t) + Vector2.up * parabola;

            elapsedTime += Time.deltaTime * GameStat.gameTimeScale;
            yield return null;
        }

        transform.position = endPosition; // Đảm bảo đối tượng ở đúng vị trí

        // Sau khi nảy, thực hiện knockback và move
        StartCoroutine(KnockbackAndMove());
    }


    private IEnumerator KnockbackAndMove()
    {
        _isMovingToTarget = false;
        Vector2 knockbackDirection = (transform.position - _target.position).normalized;
        Vector2 startPosition = transform.position;
        Vector2 knockbackPosition = startPosition + knockbackDirection * _knockbackDistance;

        float elapsedTime = 0f;
        while (elapsedTime < _knockbackDuration)
        {
            transform.position = Vector2.Lerp(startPosition, knockbackPosition, elapsedTime / _knockbackDuration);
            elapsedTime += Time.deltaTime * GameStat.gameTimeScale;
            yield return null;
        }

        _isMovingToTarget = true;
    }
}

