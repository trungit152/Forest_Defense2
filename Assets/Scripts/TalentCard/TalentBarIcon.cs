using UnityEngine;
using UnityEngine.UI;

public class TalentBarIcon : MonoBehaviour
{
    private int _id;
    private Talents _talent;
    [SerializeField] private Image _icon;
    [SerializeField] private RectTransform _rectTransform;
    public void SetTalent(Talents talent)
    {
        _talent = talent;
        _icon.sprite = talent.GetIcon();
        _icon.SetNativeSize();
        _rectTransform.localScale = Vector3.one/2;
    }
    public void SetTalent(int id)
    {
        var eId = DataTalent.GetData(id).eId;
        _id = id;
        _icon.sprite = TalentImage.instance.GetTalentImageById(eId[0])[0];
        _icon.SetNativeSize();
        _rectTransform.localScale = Vector3.one / 2;
    }
    public void OnClick()
    {
        TalentsInfo.instance.SetShownInfo(_id);
    }
}
