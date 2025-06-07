using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnlineTimeCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counter;
    private bool _canCount;
    private float _remainingTime;
    private void Awake()
    {
        if(SaveGame.SaveGameLevel.currentLevel != 0)
        {
            _counter.gameObject.SetActive(false);
            _canCount = false;
        }
        else
        {
            _counter.gameObject.SetActive(true);
            _canCount = true;
            _remainingTime = 300;
        }
    }
    private void Update()
    {
        if (_canCount)
        {
            _remainingTime -= Time.deltaTime;
            _counter.text = MyMath.FormatTime(_remainingTime).ToString();
        }
    }
}
