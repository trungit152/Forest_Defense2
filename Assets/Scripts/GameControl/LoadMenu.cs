using TMPro;
using UnityEngine;

public class LoadMenu : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        DataEnemy.GetListData();
        DataTurret.GetListData();
        DataTurn.GetListData();
        DataWave.GetListData();
        GlobalController.CurrentModeGame = GlobalController.ModeGame.ModeOffline;
    }
}
