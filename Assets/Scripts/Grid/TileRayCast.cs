using UnityEngine;

public class TileRayCast : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask blockLayer;
    [SerializeField] private LayerMask pivotLayer;
    public float raycastDistance = 20f;

    private Tile _tile;
    public Tile Tile
    {
        get
        {
            if(_tile == null)
            {
                _tile = GetComponent<Tile>();
            }
            return _tile;
        }
        set
        {
            _tile = value;
        }
    }
    public void Init()
    {
        Vector3 origin = new(transform.position.x, transform.position.y, transform.position.z + 10);
        Ray ray = new(origin, new Vector3(0, 0, -1));
        if (Physics.Raycast(ray, raycastDistance, pivotLayer))
        {
            Tile.SetType(Tile.Type.Pivot);
        }
        else if (Physics.Raycast(ray, raycastDistance, blockLayer))
        {
            Tile.SetType(Tile.Type.Block);
        }
    }
}
