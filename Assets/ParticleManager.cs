using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public void OnParticleSystemStopped()
    {
        gameObject.SetActive(false);
    }
}
