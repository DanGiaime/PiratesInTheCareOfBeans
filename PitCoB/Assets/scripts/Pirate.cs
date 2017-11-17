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
            // Find close enough pirates
            foreach (Skeleton skeleton in world.skeletons)
            {
                float dist = Vector3.Distance(this.position, skeleton.position);
                if (dist < radiusOfCaring * 2)
                {
                    ultForce += Evade(skeleton);
                }
            }

            // Find close enough objects
            foreach (Obstacle obstacle in world.obstacles)
            {
                float dist = Vector3.Distance(this.position, obstacle.Position);
                if (dist < radiusOfCaring)
                {
                    ultForce += AvoidObstacle(obstacle.Position);
                }
            }
        }

	}


}
