using UnityEngine;

public class TestButtons : MonoBehaviour
{
    [SerializeField] private GameObject _testPanel;
    [SerializeField] private GameObject _showBtn;
    [SerializeField] private GameObject _hideBtn;


    [SerializeField] private DeskController _deskController;
    [SerializeField] private CardUI _badgerCard;
    [SerializeField] private CardUI _crocCard;
    [SerializeField] private CardUI _groundSpikeCard;
    [SerializeField] private CardUI _rabbitCard;
    [SerializeField] private CardUI _foxCard;
    [SerializeField] private CardUI _penguinCard;
    [SerializeField] private CardUI _duckCard;
    [SerializeField] private CardUI _thunderBirdCard;
    [SerializeField] private CardUI _firePressureCard;
    public void AddBadgerClick()
    {
        _deskController.AddCard(_badgerCard);
    }
    public void AddCrocClick()
    {
        _deskController.AddCard(_crocCard);
    }
    public void AddGroundSpikeClick()
    {
        _deskController.AddCard(_groundSpikeCard);
    }
    public void AddRabbitClick()
    {
        _deskController.AddCard(_rabbitCard);
    }
    public void AddFoxClick()
    {
        _deskController.AddCard(_foxCard);
    }
    public void AddPenguinClick()
    {
        _deskController.AddCard(_penguinCard);
    }
    public void AddDuckClick()
    {
        _deskController.AddCard(_duckCard);
    }
    public void AddThunderBirdClick()
    {
        _deskController.AddCard(_thunderBirdCard);
    }
    public void AddFirePressureClick()
    {
        _deskController.AddCard(_firePressureCard);
    }
    public void ShowTestPanel()
    {
        if (_testPanel != null)
        {
            _testPanel.SetActive(true);
            _showBtn.SetActive(false);
            _hideBtn.SetActive(true);
        }
    }
    public void HideTestPanel()
    {
        if (_testPanel != null)
        {
            _testPanel.SetActive(false);
            _showBtn.SetActive(true);
            _hideBtn.SetActive(false);
        }
    }
}
