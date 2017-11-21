using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Agent {

    [HideInInspector] public static int id = 2;

	// Update is called once per frame
	public override void Update () {
		CalcSteeringForces ();
		base.Update ();
		ultForce = Vector3.zero;
	}

	public override void CalcSteeringForces() {
        if (world != null)
        {
            if (world.IsInBounds(position))
            {

                // Find close enough pirates
                foreach (Agent pirate in world.pirates)
                {
                    float dist = Vector3.Distance(this.position, pirate.position);
                    if (dist < radiusOfCaring * 10)
                    {
                        ultForce += Pursue(pirate);
                    }
                }

                AvoidAllNearbyObstacles(true);
            } else {
                ultForce += Seek(world.center, false);
            }
        }
	}

    public void Die() {
        GameObject p = Instantiate(deathParticles, transform.position, Quaternion.identity);
        p.GetComponent<DeathParticles>().Type = 1;
        FindObjectOfType<World>().RemoveObject(this);
        Destroy(gameObject);
    }
}
