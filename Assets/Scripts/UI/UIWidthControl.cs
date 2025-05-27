using UnityEngine;

public class UIWidthControl : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _delta = 1.8f;

    private void Awake()
    {
        _delta = (float)(Screen.height) / Screen.width;
        float width = Screen.width / _delta;
        if(width > 800)
        {
            width = 800;
        }
        else if (width < 438)
        {
            width = 438;
        }
        _rectTransform.sizeDelta = new Vector2(width, 36);
    }
}
