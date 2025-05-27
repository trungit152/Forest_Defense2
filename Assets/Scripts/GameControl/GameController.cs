using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Image _startPanel;
    private bool _isGameStarted = false;
    private float _elapsedTime = 0;
    public static GameController instance;
    private void Awake()
    {
        if(instance!=null && instance != this)
        {
            Destroy(this);

        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        GameStat.ChangeGameTimeScale(0);
    }

    private void Update()
    {
        if(!_isGameStarted)
        {
            _elapsedTime += Time.deltaTime;
            if(_elapsedTime > 3f)
            {
                StartCoroutine(FeelingTools.FadeInCoroutine(_startPanel, 0.2f, 0.3f));
            }
            if (_joystick.Direction != Vector2.zero)
            {
                GameStat.ChangeGameTimeScale(1);
                _isGameStarted = true;
                _startPanel.gameObject.SetActive(false);
            }
        }
    }
}
