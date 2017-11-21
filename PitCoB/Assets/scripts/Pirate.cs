using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : Agent
{

    [HideInInspector] public static int id = 2;

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
            if (world.IsInBounds(this.position))
            {
                Debug.Log("INSIDE");

                // Find close enough pirates
                foreach (Agent skeleton in world.skeletons)
                {
                    float dist = Vector3.Distance(this.position, skeleton.position);
                    if (dist < radiusOfCaring * 2)
                    {
                        ultForce += Evade(skeleton);
                    }
                }
                AvoidAllNearbyObstacles();

            }
            else {
                ultForce += Seek(world.center, false);
                //Debug.Log("OUTSIDE: " + ultForce);
            }

        }

	}


}
