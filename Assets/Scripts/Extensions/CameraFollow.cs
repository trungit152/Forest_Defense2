using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector2 _maxOffset;
    [SerializeField] private Transform _followObject;
    [SerializeField] private float _smoothSpeed = 5f;

    private void Update()
    {
        float targetX = Mathf.Min(_maxOffset.x, _followObject.position.x / 8);
        float targetY = Mathf.Min(_maxOffset.y, _followObject.position.y / 6);
        Vector3 targetPos = new Vector3(targetX, targetY, _camera.transform.position.z);

        _camera.transform.position = Vector3.Lerp(
            _camera.transform.position,
            targetPos,
            Time.deltaTime * _smoothSpeed
        );
    }
}
