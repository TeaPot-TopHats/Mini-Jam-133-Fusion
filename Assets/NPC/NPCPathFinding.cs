using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCPathFinding : MonoBehaviour
{ 
    Path PathComponent;
    int currentWaypoint;
    bool reachedEndOfPath;
    public Vector2 Direction { get; private set; }
    public float distanceFromPlayer { get; private set; }
    [SerializeField] private float nextWaypointDistance;

    [SerializeField] Seeker SeekerComponent;
    [SerializeField] Rigidbody2D RB;
    [SerializeField] Transform Target;

    NPCMovementHandler MovementHandler;

    public void SetMovementHandler(NPCMovementHandler MovementHandler)
    {
        this.MovementHandler = MovementHandler;
    }

    void Start()
    {
        Target = GameObject.FindWithTag("Player").transform;
        InvokeRepeating("UpdatePath", 0f, 1f);
        distanceFromPlayer = Vector2.Distance(RB.position, Target.position);

    }

    void Update()
    {
        if (PathComponent == null)
            return;
        if (currentWaypoint >= PathComponent.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }

        Direction = ((Vector2)PathComponent.vectorPath[currentWaypoint] - RB.position).normalized;
        MovementHandler.SetDirection(Direction);
        //Debug.Log("Direction in Pathfinder: " + Direction);

        float distance = Vector2.Distance(RB.position, PathComponent.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
            currentWaypoint++;

        distanceFromPlayer = Vector2.Distance(RB.position, Target.position);
        /* This is just to change direction of sprite
        if (target.transform.localPosition.x < transform.localPosition.x && GetComponent<Transform>().localScale.x > 0)
            GetComponent<Transform>().localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        else if (target.transform.localPosition.x > transform.localPosition.x && GetComponent<Transform>().localScale.x < 0)
            GetComponent<Transform>().localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        */
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            PathComponent = p;
            currentWaypoint = 0;
        }
        else
            reachedEndOfPath = false;
    }

    void UpdatePath()
    {
        if (SeekerComponent.IsDone())
        {
            SeekerComponent.StartPath(RB.position, Target.position, OnPathComplete);
        }
    }

    private void OnDisable()
    {
        
    }

}
