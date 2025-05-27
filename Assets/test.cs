using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform target;  // Điểm đến của enemy
    private Seeker seeker;
    private AIPath aiPath;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        aiPath = GetComponent<AIPath>();

        if (target != null)
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
    }

    void Update()
    {
        if (target != null && seeker.IsDone())
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            aiPath.SetPath(p);
        }
    }
}
