using System.Collections.Generic;
using UnityEngine;

public class TalentImage : MonoBehaviour
{
    [Header("Icon")]
    public Sprite _unknowAsset;
    public Sprite _iconAmorBreakCast;
    public Sprite _iconBacklash;
    public Sprite _iconChargedAtack;
    public Sprite _iconDeflect;
    public Sprite _iconFlameEnchant;
    public Sprite _iconFrostEnchant;
    public Sprite _iconIncreaseAtk;
    public Sprite _iconIncreaseAtkRange;
    public Sprite _iconIncreaseAtkSpd;
    public Sprite _iconIncreaseCritDmg;
    public Sprite _iconIncreaseCritRate;
    public Sprite _iconMultishot;
    public Sprite _iconPiercingProjectile;
    public Sprite _iconSplitProjectile;

    public Sprite _iconAddRabbit;
    public Sprite _iconAddCroc;
    public Sprite _iconAddFox;
    public Sprite _iconAddDuck;
    public Sprite _iconAddPenguin;
    public Sprite _iconAddThunderBird;
    public Sprite _iconAddBalista;
    public Sprite _iconAddSpike;
    public Sprite _iconAddFireTrap;
    public Sprite _iconAddSwamp;
  [Space(10)]
    [Header("Frame")]
    [SerializeField] private Sprite _rareFrame;
    [SerializeField] private Sprite _epicFrame;
    [Space(10)]
    [Header("BackGround")]
    [SerializeField] private Sprite _rareBg;
    [SerializeField] private Sprite _epicBg;

    public static TalentImage instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public List<Sprite> GetTalentImageById(int id)
    {
        List<Sprite> res = new List<Sprite>();
        switch (id)
        {
            case IDs.EFFECT_ATK:
                res.Add(_iconIncreaseAtk);
                res.Add(_rareFrame);
                res.Add(_rareBg);
                break;
            case IDs.EFFECT_ATKS:
                res.Add(_iconIncreaseAtkSpd);
                res.Add(_rareFrame);
                res.Add(_rareBg);
                break;
            case IDs.EFFECT_MORE_PROJECTILE:
                res.Add(_iconMultishot);
                res.Add(_epicFrame);
                res.Add(_epicBg);
                break;
            case IDs.EFFECT_MORE_TARGET:
                res.Add(_iconPiercingProjectile);
                res.Add(_epicFrame);
                res.Add(_epicBg);
                break;
            case IDs.EFFECT_RANGE:
                res.Add(_iconIncreaseAtkRange);
                res.Add(_rareFrame);
                res.Add(_rareBg);
                break;
            //Add
            case IDs.EFFECT_ADD_BOW:
                res.Add(_iconAddRabbit);
                res.Add(_rareFrame);
                res.Add(_rareBg);
                break;
            case IDs.EFFECT_ADD_CANNON:
                res.Add(_iconAddCroc);
                res.Add(_rareFrame);
                res.Add(_rareBg);
                break;
            case IDs.EFFECT_ADD_FOX:
                res.Add(_iconAddFox);
                res.Add(_rareFrame);
                res.Add(_rareBg);
                break;
            case IDs.EFFECT_ADD_DUCK:
                res.Add(_iconAddDuck);
                res.Add(_rareFrame);
                res.Add(_rareBg);
                break;
            case IDs.EFFECT_ADD_BALISTA:
                res.Add(_iconAddBalista);
                res.Add(_rareFrame);
                res.Add(_rareBg);
                break;
            case IDs.EFFECT_ADD_THUNDER:
                res.Add(_iconAddThunderBird);
                res.Add(_rareFrame);
                res.Add(_rareBg);
                break;
            case IDs.EFFECT_ADD_SPIKE:
                res.Add(_iconAddSpike);
                res.Add(_rareFrame);
                res.Add(_rareBg);
                break;
            case IDs.EFFECT_ADD_FIRE_TRAP:
                res.Add(_iconAddFireTrap);
                res.Add(_rareFrame);
                res.Add(_rareBg);
                break;
            case IDs.EFFECT_ADD_SWAMP:
                res.Add(_iconAddSwamp);
                res.Add(_rareFrame);
                res.Add(_rareBg);
                break;

            default:
                res.Add(_unknowAsset);
                res.Add(_rareFrame);
                res.Add(_rareBg);
                break;
        }
        return res;
    }
}
