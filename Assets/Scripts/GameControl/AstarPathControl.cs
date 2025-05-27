using Pathfinding;
using UnityEngine;
public class AstarPathControl : MonoBehaviour
{
    [SerializeField] private AstarPath _astarPath;
    [SerializeField] private Seeker _agent;
    [SerializeField] private Transform _target;

    public static AstarPathControl instance;
    public bool _canSet = true;

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

    public void RescanMap(Bounds bounds)
    {
        var guo = new GraphUpdateObject(bounds);
        guo.updatePhysics = true;
        AstarPath.active.UpdateGraphs(guo);
        AstarPath.active.FlushGraphUpdates(); 
        CheckReachableWay();
    }
    public void ScanAll()
    {
        _astarPath.Scan();
        CheckReachableWay();
    }
    public void CheckReachableWay()
    {
        _agent.StartPath(_agent.transform.position, _target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            Vector3 finalPos = p.vectorPath[p.vectorPath.Count - 1];
            float distanceToTarget = Vector3.Distance(finalPos, _target.position);

            if (distanceToTarget > 0.5f)
            {
                _canSet = false;
            }
            else
            {
                _canSet = true;
            }
        }
    }
}
