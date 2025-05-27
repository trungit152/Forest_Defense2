using UnityEngine;

namespace ntDev
{
    public class CameraResize : MonoBehaviour
    {
        Camera cam;
        public Camera Cam
        {
            get
            {
                if (cam == null) cam = GetComponent<Camera>();
                return cam;
            }
        }

        const float zFlip = 1080 / 2640f;

        [SerializeField] float minSize = 540;
        // [SerializeField] float maxSize = 720;
        [SerializeField] bool Orthographic = true;

        void Start()
        {
            CheckCamera();
        }

        void CheckCamera()
        {
            if (Orthographic)
            {
                if (Cam.aspect > 9 / 16f) Cam.orthographicSize = minSize;
                else Cam.orthographicSize = minSize * (9 / 16f / Cam.aspect);
            }
            else
            {
                if (Cam.aspect < (9 / 16f)) Cam.fieldOfView = 75 * Cam.aspect / (9 / 16f);
            }
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            CheckCamera();
        }
#endif
    }
}
