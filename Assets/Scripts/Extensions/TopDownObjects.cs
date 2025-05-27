using UnityEngine;

public class TopDownObjects : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y/1000);
    }
}
