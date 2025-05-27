using UnityEngine;

namespace ntDev
{
    [ExecuteInEditMode]
    public class SnapToScene : MonoBehaviour
    {
        [SerializeField] float d = 0.275f;
        [SerializeField] bool update = true;
#if UNITY_EDITOR
        void Update()
        {
            if (Application.isPlaying) return;
            if (!update) return;
            if (!transform.hasChanged)
            {
                Vector3 pos = transform.localPosition;
                pos.x = Mathf.RoundToInt(pos.x / d) * d;
                pos.y = Mathf.RoundToInt(pos.y / d) * d;
                transform.localPosition = pos;

                Vector3 rotation = transform.eulerAngles;
                rotation.x = 0;
                rotation.y = Mathf.RoundToInt(rotation.y / 90) * 90;
                rotation.z = 0;
                transform.localEulerAngles = rotation;
            }
            else transform.hasChanged = false;
        }
#endif
    }
}