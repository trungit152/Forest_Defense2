using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretsCard : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] protected CardUI _turretCardUI;
    [SerializeField] private Transform _visual;
    [SerializeField] private SpriteRenderer _visualSr;
    [SerializeField] private BezierCurve _bezierCurve;
    [SerializeField] private AnimationCurve _animationCurve;
    public Type _type;
    public enum Type
    {
        Turret,
        Trap
    }

    public void Init()
    {
        _bezierCurve.Init();
        _visualSr.sortingOrder = 0;
        SetPicked(true);
    }
    public void SetPicked(bool isPicked = true)
    {
        _boxCollider.enabled = false;
        StartCoroutine(RotateAndScaleUp(_bezierCurve.GetDuration()));
        _bezierCurve.StartMovement();
        _visualSr.sortingOrder = 100;
    }
    private void CheckOnTarget()
    {
        gameObject.SetActive(false);
        _boxCollider.enabled = true;
        AddToDesk();
    }
    private IEnumerator RotateAndScaleUp(float duration)
    {
        float elapsedTime = 0f;
        Vector3 startScale = _visual.localScale;
        Vector3 targetScale = new Vector3(0.8f, 0.8f, 0.8f);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float easedT = t * t * t;

            float zRotation = 480 * t * Time.deltaTime * (1+_animationCurve.Evaluate(t)*2);
            _visual.Rotate(0, 0, zRotation);

            _visual.localScale = Vector3.Lerp(startScale, targetScale, easedT);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _visual.localScale = targetScale;
        CheckOnTarget();

    }
    protected virtual void AddToDesk()
    {
        DeskController.instance.AddCard(_turretCardUI);
    }
}
