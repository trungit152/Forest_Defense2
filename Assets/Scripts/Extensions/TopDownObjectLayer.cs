using UnityEngine;

public class TopDownObjectLayer : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y / 1000);
    }
}
