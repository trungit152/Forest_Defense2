using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public void FadeOut()
    {
        StartCoroutine(FeelingTools.FadeOutCoroutine(_spriteRenderer, 0.6f));
    }
}
