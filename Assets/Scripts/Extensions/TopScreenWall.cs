using UnityEngine;

public class TopScreenWall : MonoBehaviour
{
    [SerializeField] private Position _position;
    private enum Position
    {
        Top,
        Bottom,
        Left,
        Right
    }
    private void Awake()
    {
        Vector2 screenPosition = new Vector2();
        switch (_position)
        {
            case (Position.Left):
                screenPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f));
                break;
            case (Position.Right):
                screenPosition = Camera.main.ViewportToWorldPoint(new Vector2(1f, 0.5f));
                break;
            case (Position.Top):
                screenPosition = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 1f));
                break;
            case(Position.Bottom):
                screenPosition = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0f));
                break;
        }
        transform.position = screenPosition;
    }
}
