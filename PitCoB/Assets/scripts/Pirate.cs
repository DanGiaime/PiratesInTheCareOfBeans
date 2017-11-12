using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : Agent
{

	public Skeleton skeletonClosest;

	// Use this for initialization
	void Start()
	{
		this.skeletonClosest = world.skeletons[0];
		base.Start();
	}

	// Update is called once per frame
	void Update()
	{
		CalcSteeringForces();
		this.ApplyForce(ultForce.normalized * 10f);
		base.Update();
		ultForce = Vector3.zero;
	}

	public override void CalcSteeringForces()
	{
		//Find closest skeletons
		float minDist = Vector3.Distance(this.transform.position, skeletonClosest.position);
		foreach (Skeleton skeleton in world.skeletons)
		{
			float newDist = Vector3.Distance(this.transform.position, skeleton.position);
			if (newDist < minDist)
			{
				minDist = newDist;
				skeletonClosest = skeleton;
			}
		}

		//Flee and seek
		ultForce += Flee(skeletonClosest);
		//ultForce += Seek (PSG);

		//avoid
		//foreach (Obstacle obst in world.obstacles)
		//{
		//	Vector3 avoidForce = AvoidObstacle(obst);
		//	ultForce += avoidForce;
		//}

	}
}
