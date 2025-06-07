using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtIndexRank;
    [SerializeField] private TextMeshProUGUI txtNameRank;
    [SerializeField] private TextMeshProUGUI txtCoin;

    public void SetRankParam(int indexRank, string nameRank, int coin, bool isMyRank)
    {
        txtIndexRank.text = indexRank.ToString();
        txtNameRank.text = nameRank.Split("#")[0];
        txtCoin.text = coin.ToString();
        txtIndexRank.color = isMyRank ? Color.red : Color.white;
    }
}
