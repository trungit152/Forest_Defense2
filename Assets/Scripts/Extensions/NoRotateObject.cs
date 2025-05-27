using UnityEngine;

public class NoRotateObject : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
