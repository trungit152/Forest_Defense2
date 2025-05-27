using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private float _radius = 1f;
    [SerializeField] private float _maxDistance = 1f;
    [SerializeField] private LayerMask _cardLayer;
    [SerializeField] private LayerMask _itemLayer;
    [SerializeField] private Transform _deskPosition;
    [SerializeField] private Transform _expTarget;
    void Update()
    {
        PickupCardCircleCast();
    }

    private void PickupCardCircleCast()
    {
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.zero;

        int combinedLayerMask = _cardLayer | _itemLayer;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, _radius, direction, _maxDistance, combinedLayerMask);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                int hitLayer = hit.collider.gameObject.layer;

                if (((1 << hitLayer) & _cardLayer) != 0)
                {
                    var card = hit.collider.GetComponent<TurretsCard>();
                    if (card != null)
                    {
                        card.Init();
                        card.SetPicked(true);
                    }
                }
            }
        }
        Debug.DrawLine(origin, origin + direction * _maxDistance, Color.blue);
    }
    public void SetRadius(float radius)
    {
        _radius = radius;
    }
}
