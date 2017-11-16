using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Agent {

	public Pirate pirateTarget;
    public Obstacle obstacleClosest;
    public Vector3 edgeAvoidTarget;
	
	// Update is called once per frame
	public override void Update () {
		CalcSteeringForces ();

        //ultForce += Wander();
        this.ApplyForce (ultForce.normalized * maxForce);
		base.Update ();
		ultForce = Vector3.zero;
	}

	public override void CalcSteeringForces() {
        if (world != null)
        {
            // Find closest pirate
            float minDist = Mathf.Infinity;
            foreach (Pirate pirate in world.pirates)
            {
                float newDist = Vector3.Distance(this.transform.position, pirate.position);
                if (newDist < minDist)
                {
                    minDist = newDist;
                    pirateTarget = pirate;
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
                Debug.Log("Seek Away from Edge");
                SetSeekTarget(edgeAvoidTarget);
            }
            if (pirateTarget != null)
            {
                Debug.Log("Seek Pirate");
                SetSeekTarget(pirateTarget.position);
            }
            else if (obstacleClosest != null)
            {
                Debug.Log("Avoid Obstacle Skeleton");
                SetAvoidTarget(obstacleClosest.Position);
            }
            else
            {
                Debug.Log("wat");
            }
        }


	}


}
