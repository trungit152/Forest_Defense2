using UnityEngine;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour
{
    [SerializeField] protected int _iD;
    [SerializeField] protected string _name;
    [SerializeField] protected RectTransform _visual;
    [SerializeField] protected RectTransform _shadow;
    protected SetableObjects _currentDrag;

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (_currentDrag != null)
        {
            _currentDrag.transform.position = (Vector2)GetMouseWorldPosition() +new Vector2(0, 1.5f);
        }
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        VisualDown();
        TurretManager.instance.HideMergeArrow();
        if (_currentDrag != null)
        {
            _currentDrag.SetOnPosition();
            GridManager.instance.ShowTile(false);
            GameStat.ChangeGameTimeScale(1);
            if (_currentDrag != null)
            {
                _currentDrag = null;
            }
        }
    }

    protected Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    public virtual string GetName()
    {
        return _name;
    }
    public void StopDrag()
    {
        if (_currentDrag != null)
        {
            Debug.Log("stop");
            _currentDrag.StopDrag();
            _currentDrag = null;
            GridManager.instance.ShowTile(false);
            GameStat.ChangeGameTimeScale(1);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            StopDrag();
        }
    }
    public void VisualUp()
    {
        StartCoroutine(FeelingTools.MoveToTarget(_visual, 0.1f, new Vector3(0,25f,0), null, 0));
        StartCoroutine(FeelingTools.MoveToTarget(_shadow, 0.12f, new Vector3(8,10f,0), null, 0));
    }
    public void VisualDown()
    {
        StartCoroutine(FeelingTools.MoveToTarget(_visual, 0.1f, new Vector3(0, 0f, 0), null, 0));
        StartCoroutine(FeelingTools.MoveToTarget(_shadow, 0.12f, new Vector3(0, 0f, 0), null, 0));
    }
    public int ID()
    {
        return _iD;
    }
}
