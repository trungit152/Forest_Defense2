using UnityEngine;

public class TalentCards : MonoBehaviour
{
    [SerializeField] private Talents _talent;
    
    public void Active()
    {
        _talent.Active();
    }
}
