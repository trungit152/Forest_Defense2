using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleObject : MonoBehaviour
{
    [SerializeField] private bool visibleWhenOnline;

    void Start()
    {
        bool isOnline = GlobalController.CurrentModeGame == GlobalController.ModeGame.ModeOnline;
        gameObject.SetActive(visibleWhenOnline == isOnline);
    }
}
