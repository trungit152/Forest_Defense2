using UnityEngine;

public class TurnOffInGame : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _col;

    private void Start()
    {
        if(_col != null)
        {
            _col.enabled = false;
        }
    }
}
