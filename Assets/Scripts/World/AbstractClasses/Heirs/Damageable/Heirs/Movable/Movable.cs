using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Movable : Damageable {

    public NavMeshAgent agent;

	// Use this for initialization
	public new void Start () {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
     * Moves this object as close as possible to the passed Transform
     * @param trans The passed Transform towards which this object is moving
     */ 
    public void SetDestination(Vector3 pos)
    {
        agent.SetDestination(pos);
    }

    public Clickable GetObjectOfTypeWithShortestPath(Type type)
    {
        Clickable[] stageClickables = stage.GetComponentsInChildren<Clickable>();
        List<Clickable> stageClickablesOfType = new List<Clickable>();
        Clickable nearestClickableOfType = null;

        for (int i = 0; i < stageClickables.Length; i++)
        {
            if (stageClickables[i].GetType() == type)
            {
                stageClickablesOfType.Add(stageClickables[i]);
            }
        }

        foreach (Clickable clickable in stageClickablesOfType)
        {
            if ((nearestClickableOfType == null || CalculatePathLength(clickable.transform.position) < CalculatePathLength(nearestClickableOfType.transform.position)) && clickable != this)
            {
                nearestClickableOfType = clickable;
            }
        }

        return nearestClickableOfType;
    }

    public Clickable GetObjectOfTypeWithLongestPath(Type type)
    {
        Clickable[] stageClickables = stage.GetComponentsInChildren<Clickable>();
        List<Clickable> stageClickablesOfType = new List<Clickable>();
        Clickable farthestClickableOfType = null;

        for (int i = 0; i < stageClickables.Length; i++)
        {
            if (stageClickables[i].GetType() == type)
            {
                stageClickablesOfType.Add(stageClickables[i]);
            }
        }

        foreach (Clickable clickable in stageClickablesOfType)
        {
            if ((farthestClickableOfType == null || CalculatePathLength(clickable.transform.position) > CalculatePathLength(farthestClickableOfType.transform.position)) && clickable != this)
            {
                farthestClickableOfType = clickable;
            }
        }

        return farthestClickableOfType;
    }

    public float CalculatePathLength(Vector3 targetPos)
    {
        // Create a path and set it based on a target position.
        NavMeshPath path = new NavMeshPath();
        if (agent.enabled)
            agent.CalculatePath(targetPos, path);

        // Create an array of points which is the length of the number of corners in the path + 2.
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        // The first point is the enemy's position.
        allWayPoints[0] = transform.position;

        // The last point is the target position.
        allWayPoints[allWayPoints.Length - 1] = targetPos;

        // The points inbetween are the corners of the path.
        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        // Create a float to store the path length that is by default 0.
        float pathLength = 0;

        // Increment the path length by an amount equal to the distance between each waypoint and the next.
        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }
}
