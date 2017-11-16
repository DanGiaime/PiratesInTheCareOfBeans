using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : Agent
{

	public Skeleton skeletonClosest;
    public Obstacle obstacleClosest;
    public Vector3 edgeAvoidTarget;

	// Update is called once per frame
	public override void Update()
	{
        CalcSteeringForces();
		base.Update();
		ultForce = Vector3.zero;
	}

	public override void CalcSteeringForces()
	{
        if (world != null)
        {
            // Find closest skeleton
            float minDist = Mathf.Infinity;
            foreach (Skeleton skeleton in world.skeletons)
            {
                float newDist = Vector3.Distance(this.transform.position, skeleton.position);
                if (newDist < minDist)
                {
                    minDist = newDist;
                    skeletonClosest = skeleton;
                }
            }

            // Find closest object
            minDist = Mathf.Infinity;
            foreach (Obstacle obstacle in world.obstacles)
            {
                float newDist = Vector3.Distance(this.transform.position, obstacle.Position);
                if (newDist < minDist)
                {
                    minDist = newDist;
                    obstacleClosest = obstacle;
                }
            }

            // Find target if I would hit an edge
            edgeAvoidTarget = AvoidEdges();

            if (edgeAvoidTarget != Vector3.zero)
            {
                Debug.Log("Seek Away from Edge Pirate");
                Debug.Log(edgeAvoidTarget);
                SetSeekTarget(edgeAvoidTarget);
            }
            if (skeletonClosest != null && Vector3.Distance(skeletonClosest.position, position) < distanceToObject )
            {
                Debug.Log("Flee From Sekeleton");
                SetFleeTarget(skeletonClosest.position);
            }
            //else if (obstacleClosest != null)
            //{
            //    Debug.Log("Avoid Obstacle pirate");
            //    SetAvoidTarget(obstacleClosest.Position);
            //}
            else
            {
                Debug.Log("wat");
            }
        }

	}


}
