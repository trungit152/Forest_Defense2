using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    private List<Vector3> controlPoints;
    [SerializeField] private float duration = 2f;
    private Vector3 topScreenPosition;
    private Vector3 botScreenPosition;
    private Vector3 rightScreenPosition;
    private Vector3 leftScreenPosition;
    public void Init()
    {
        controlPoints = new List<Vector3>();
        SetControlPoint();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            StartMovement(); 
        }
    }
    public void StartMovement()
    {
        CheckLeftRight();
        StartCoroutine(FollowCurve());
    }
    private void SetControlPoint()
    {
        topScreenPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 10f));
        botScreenPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, 10f));
        rightScreenPosition = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0.5f, 10f));
        leftScreenPosition = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.5f, 10f));

        controlPoints.Add(topScreenPosition);
        controlPoints.Add(rightScreenPosition);
        controlPoints.Add(botScreenPosition);
    }
    private void CheckLeftRight()
    {
        if (transform.position.x < 0f)
        {
            controlPoints[1] = rightScreenPosition;
        }
        else
        {
            controlPoints[1] = leftScreenPosition;
        }
    }
    private System.Collections.IEnumerator FollowCurve()
    {
        float time = 0f;
        Vector3 startPos = transform.position;

        while (time < 1f)
        {
            time += Time.deltaTime / duration;
            float t = IncreaseSpeed(time);
            transform.position = CalculateBezierPoint(t, startPos, controlPoints[0], controlPoints[1], controlPoints[2]);
            yield return null;
        }
    }
    private float IncreaseSpeed(float t)
    {
        return t*t*t;
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0 + 3 * (uu * t) * p1 + 3 * (u * tt) * p2 + ttt * p3;
        return p;
    }
    public bool CheckOnTarget()
    {
        Debug.Log(Vector2.Distance(transform.position, controlPoints[2]));
        if(Vector2.Distance(transform.position, controlPoints[2]) < 0.5f)
        {
            return true;
        }
        return false;
    }
    public float GetDuration()
    {
        return duration;
    }
}