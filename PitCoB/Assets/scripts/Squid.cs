using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : Agent {

    [HideInInspector] private static int id = 3;

    // Update is called once per frame
    public override void Update()
    {
        CalcSteeringForces();
        base.Update();
        ultForce = Vector3.zero;
    }

	public override void CalcSteeringForces() {
        if (world != null)
        {
            if (world.IsInBounds(position))
            {
                ultForce += Flock(Squid.id);
            }
            else {
                ultForce += Seek(world.center, false);
            }
            AvoidAllNearbyObstacles(true);

        }
	}

    public void Die() {
        GameObject p = Instantiate(deathParticles, transform.position, Quaternion.identity);
        p.GetComponent<DeathParticles>().Type = 2;
        FindObjectOfType<World>().RemoveObject(this);
        Destroy(gameObject);
    }
}
