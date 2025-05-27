using UnityEngine;

public class FollowUIPoint : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    void Update()
    {
        transform.position = FeelingTools.ConvertUIToWorldPositionCamera(_rectTransform, Camera.main);
    }
}
