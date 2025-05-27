using UnityEngine;

public class WaveControlButton : MonoBehaviour
{
    public void SetStart()
    {
        SpawnEnemiesController.instance.SetStart();
    }
}
