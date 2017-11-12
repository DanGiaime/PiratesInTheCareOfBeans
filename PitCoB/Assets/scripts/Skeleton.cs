using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Agent {

	public Pirate pirateTarget;

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		CalcSteeringForces ();
		this.ApplyForce (ultForce.normalized * 10f);
		base.Update ();
		ultForce = Vector3.zero;
	}

	public override void CalcSteeringForces() {
        float minDist = Vector3.Distance (this.transform.position, pirateTarget.position);

		foreach (Pirate pirate in world.pirates) {
			float newDist = Vector3.Distance (this.transform.position, pirate.position);
			if (newDist < minDist) {
				minDist = newDist;
				pirateTarget = pirate;
			}
		}

		ultForce += Seek (pirateTarget);

  //      foreach (Obstacle obst in world.obstacles) {
		//	Vector3 avoidForce = AvoidObstacle(obst);
		//	ultForce += avoidForce;
		//}

	}

}
