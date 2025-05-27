using UnityEngine;

public class DebugManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Vector2 a = new Vector2(1,2) - new Vector2(0,1);
            UnityEngine.Debug.Log(Mathf.Atan2(1, 2) * Mathf.Rad2Deg);
        }
    }
}
