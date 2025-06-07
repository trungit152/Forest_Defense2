public class SaveTalentPoint
{
    const string TALENT_POINT = "TALENT_POINT";
    const string COIN = "COIN";
    private int _talentPoint;
    private int _coin;

    public int Coin
    {
        get
        {
            if (_coin == 0)
            {
                _coin = ES3.Load(COIN, 0);
            }
            return _coin;
        }
    }
    public int TalentPoint
    {
        get
        {
            if (_talentPoint == 0)
            {
                _talentPoint = ES3.Load(TALENT_POINT, 0);
            }
            return _talentPoint;
        }
    }

    public SaveTalentPoint()
    {
        _talentPoint = ES3.Load<int>(TALENT_POINT, 0);
        _coin = ES3.Load<int>(COIN, 0);
    }

    public void Load()
    {
        _talentPoint = ES3.Load<int>(TALENT_POINT,0);
        _coin = ES3.Load<int>(COIN, 0);
    }
    public void Save()
    {
        ES3.Save(TALENT_POINT, _talentPoint);
        ES3.Save(COIN, _coin);
        RankingFeature.SubmitScore(_coin);
    }
    public void AddTalentPoint(int point)
    {
        _talentPoint += point;
        Save();
    }

    public void AddCoin(int coin)
    {
        _coin += coin;
        Save();
    }
}
