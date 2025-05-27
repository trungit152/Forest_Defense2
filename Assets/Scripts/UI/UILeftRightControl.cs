using UnityEngine;

public class UILeftRightControl : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Position _position;
    [SerializeField] private float _delta = 4f;
    private enum Position
    {
Left, Right };

    private void Awake()
    {
        if (_position == Position.Left)
        {
            float delta = Mathf.Max(Screen.width / 4, 260);
            _rectTransform.anchoredPosition = new Vector3(delta, _rectTransform.localPosition.y, 0);
        }
        else
        {
            float delta = Mathf.Max(Screen.width / 4, 260);
            _rectTransform.anchoredPosition = new Vector3(-delta, _rectTransform.localPosition.y, 0);
        }
    }
}
