using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject parent;
    public void TurnOffObject()
    {
        gameObject.SetActive(false);
    }
    public void TurnOffParent()
    {
        parent.SetActive(false);
    }
    public void DestroyParent()
    {
        Destroy(parent);
    }
}
