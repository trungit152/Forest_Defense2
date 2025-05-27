using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalentsInfo : MonoBehaviour
{
    [SerializeField] private Image _infoPanel;
    [SerializeField] private RectTransform _infoChild;
    [SerializeField] private Image _talentIcon;
    [SerializeField] private Image _talentFrame;
    [SerializeField] private Image _talentBackGround;
    [SerializeField] private Image _stackImage;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _stacksText;
    [SerializeField] private TextMeshProUGUI _title;
    private int _index;
    private List<int> _chosenTalents;

    public static TalentsInfo instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;    
        }
    }

    public void SetShownInfo(int id)
    {
        _chosenTalents = TalentsController.instance.GetChosenTalentsList();
        if (_chosenTalents.Contains(id))
        {
            SetInfo(id);
        }
    }

    public void GoNextTalents()
    {
        if( _index < _chosenTalents.Count - 1)
        {
            _index++;
            var talent = _chosenTalents[_index];
            SetInfo(talent);
        }
        else
        {
            _index=0;
            var talent = _chosenTalents[_index];
            SetInfo(talent);
        }
    }
    public void GoPreviousTalents()
    {
        if (_index > 0)
        {
            _index--;
            var talent = _chosenTalents[_index];
            SetInfo(talent);
        }
        else
        {
            _index = _chosenTalents.Count - 1;
            var talent = _chosenTalents[_index];
            SetInfo(talent);
        }
    }
    public void SetInfo(int id)
    {
        if (!_infoPanel.gameObject.activeInHierarchy)
        {
            StartCoroutine(FeelingTools.FadeInCoroutine(_infoPanel, 0.15f, 0.8f));
            StartCoroutine(FeelingTools.ZoomInUI(_infoChild, 0.5f, 1f, 0.25f));
        }
        var eId = DataTalent.GetData(id).eId[0];
        var listImage = TalentImage.instance.GetTalentImageById(eId);
        var dataTalent = DataTalent.GetData(id);
        _talentIcon.sprite = listImage[0];
        _talentIcon.SetNativeSize();
        _talentFrame.sprite = listImage[1];
        _talentFrame.SetNativeSize();
        _talentBackGround.sprite = listImage[2];
        _talentBackGround.SetNativeSize();
        _stackImage.SetNativeSize();
        _description.text = dataTalent.Description;
        _title.text = "TITLE";
        _stacksText.text = "1";
    }
    public void CloseClick()
    {
        StartCoroutine(FeelingTools.FadeOutCoroutine(_infoPanel, 0.1f));
    }
}
