using UnityEngine;
using UnityEngine.UI;

public class WaveBarProcess : MonoBehaviour
{
    [SerializeField] private Slider _waveBarSlider;
    [SerializeField] private GameObject _handle;
    [SerializeField] private GameObject _separator;
    [SerializeField] private Transform _separatorParent;
    private void Start()
    {
        _waveBarSlider.value = 0;
        for (int i = 0; i < SpawnEnemiesController.instance.GetWavesAmount()-1; i++)
        {
            Instantiate(_separator, _separatorParent);
        }
    }
    private void Update()
    {
        SpawnEnemiesController.instance.WaveBarProcess(_waveBarSlider);
        if (_waveBarSlider.value >= 0.999) 
        {
            _handle.SetActive(false);
        }
    }


}
