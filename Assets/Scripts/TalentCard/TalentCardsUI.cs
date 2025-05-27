using Pathfinding.RVO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalentCardsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _stacksText;
    [SerializeField] private Image _frame;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _background;
    private int _talentId;
    //private Talents _talent;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    public void OnCardClicked()
    {
        ChooseEnd();
        TalentsController.instance.AddChosenTalent(_talentId);
        switch (_talentId)
        {
            //ADD TURRET
            case IDs.TALENT_ADD_BOW:
                DeskController.instance.AddCard(IDs.TURRET_BOW);
                TalentsController.instance.DecreaseAppearAddPercent();
                break;
            case IDs.TALENT_ADD_BALISTA:
                DeskController.instance.AddCard(IDs.TURRET_BALISTA);
                TalentsController.instance.DecreaseAppearAddPercent();
                break;
            case IDs.TALENT_ADD_CANNON:
                DeskController.instance.AddCard(IDs.TURRET_CANON);
                TalentsController.instance.DecreaseAppearAddPercent();
                break;
            case IDs.TALENT_ADD_FIRE:
                DeskController.instance.AddCard(IDs.TURRET_FIRE);
                TalentsController.instance.DecreaseAppearAddPercent();
                break;
            case IDs.TALENT_ADD_THUNDER:
                DeskController.instance.AddCard(IDs.TURRET_THUNDER);
                TalentsController.instance.DecreaseAppearAddPercent();
                break;
            case IDs.TALENT_ADD_WATER:
                DeskController.instance.AddCard(IDs.TURRET_WATER);
                TalentsController.instance.DecreaseAppearAddPercent();
                break;
            case IDs.TALENT_ADD_SPIKE:
                DeskController.instance.AddCard(IDs.TRAP_SPIKE);
                TalentsController.instance.DecreaseAppearAddPercent();
                break;
            case IDs.TALENT_ADD_FIRE_TRAP:
                DeskController.instance.AddCard(IDs.TRAP_FIRE);
                TalentsController.instance.DecreaseAppearAddPercent();
                break;
            case IDs.TALENT_ADD_SWAMP:
                DeskController.instance.AddCard(IDs.TRAP_SWAMP);
                TalentsController.instance.DecreaseAppearAddPercent();
                break;
            //ATK
            case IDs.TALENT_ATK_BOW:
                TurretManager.instance.IncreaseATK(IDs.TURRET_BOW, DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_ATK_BALISTA:
                TurretManager.instance.IncreaseATK(IDs.TURRET_BALISTA, DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_ATK_CANNON:
                TurretManager.instance.IncreaseATK(IDs.TURRET_CANON, DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_ATK_FIRE:
                TurretManager.instance.IncreaseATK(IDs.TURRET_FIRE, DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_ATK_THUNDER:
                TurretManager.instance.IncreaseATK(IDs.TURRET_THUNDER, DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_ATK_WATER:
                TurretManager.instance.IncreaseATK(IDs.TURRET_WATER, DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_ATK_SPIKE:
                TurretManager.instance.IncreaseATK(IDs.TRAP_SPIKE, DataTalent.GetData(_talentId).eValue[0]);
                break;
            //ATKS
            case IDs.TALENT_ATKS_BOW:
                TurretManager.instance.IncreaseATKS(IDs.TURRET_BOW, DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_ATKS_BALISTA:
                TurretManager.instance.IncreaseATKS(IDs.TURRET_BALISTA, DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_ATKS_CANNON:
                TurretManager.instance.IncreaseATKS(IDs.TURRET_CANON, DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_ATKS_FIRE:
                TurretManager.instance.IncreaseATKS(IDs.TURRET_FIRE, DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_ATKS_THUNDER:
                TurretManager.instance.IncreaseATKS(IDs.TURRET_THUNDER, DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_ATKS_WATER:
                TurretManager.instance.IncreaseATKS(IDs.TURRET_WATER, DataTalent.GetData(_talentId).eValue[0]);
                break;
            //RANGE
            case IDs.TALENT_RANGE_BOW:
                TurretManager.instance.IncreaseRange(IDs.TURRET_BOW, DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_RANGE_CANNON:
                TurretManager.instance.IncreaseRange(IDs.TURRET_CANON, DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_RANGE_FIRE:
                TurretManager.instance.IncreaseRange(IDs.TURRET_FIRE, DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_RANGE_BALISTA:
                TurretManager.instance.IncreaseRange(IDs.TURRET_BALISTA, DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_RANGE_WATER:
                TurretManager.instance.IncreaseRange(IDs.TURRET_WATER, DataTalent.GetData(_talentId).eValue[0]);
                break;

            //SPECIAL
            case IDs.TALENT_MORE_PROJECTILE_BOW:
                TurretManager.instance.AddBulletAmount(IDs.TURRET_BOW);
                break;
            case IDs.TALENT_CHANCE_EXTRA_SHOT_CANNON:
                TurretManager.instance.AddBulletAmount(IDs.TURRET_CANON);
                TurretManager.instance.AddMultiShootPercent(DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_ADD_TARGET_THUNDER:
                TurretManager.instance.AddBounceAmount(DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_SLOW_SPIKE:
                TurretManager.instance.AddTrapSpecialEffect1(IDs.TRAP_SPIKE, DataTalent.GetData(_talentId).eValue[0]);
                break;
            case IDs.TALENT_CHANCE_STUN_THUNDER:
                TurretManager.instance.AddStunEffect(IDs.TURRET_THUNDER, DataTalent.GetData(_talentId).eValue[0]
                    , DataTalent.GetData(_talentId).eValue[1]);
                break;
            case IDs.TALENT_CHANCE_STUN_BALISTA:
                TurretManager.instance.AddStunEffect(IDs.TURRET_BALISTA, DataTalent.GetData(_talentId).eValue[0]
                    , DataTalent.GetData(_talentId).eValue[1]);
                break;
            //
            default:
                break;
        }
    }


    public void SetInfo(int id)
    {
        _talentId = id;
        int[] eId = DataTalent.GetData(id).eId;
        List<Sprite> infoSprite = TalentImage.instance.GetTalentImageById(eId[0]);
        _description.text = DataTalent.GetData(id).Description;
        _icon.sprite = infoSprite[0];
        _icon.SetNativeSize();
        _stacksText.text = "1";
        _frame.sprite = infoSprite[1];
        _frame.SetNativeSize();
        _background.sprite = infoSprite[2];
        _background.SetNativeSize();
    }
    public IEnumerator Rotate()
    {
        float angleElapsed = 0;
        while (angleElapsed < 360)
        {
            angleElapsed += Time.deltaTime * 720;
            _rectTransform.rotation = Quaternion.Euler(0, angleElapsed, 0);
            yield return null;
        }
    }
    private void ChooseEnd()
    {
        PlayerLevel.instance.ChooseTalentEnd();
    }
}
