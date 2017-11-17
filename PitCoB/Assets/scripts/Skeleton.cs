using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Agent {

	// Update is called once per frame
	public override void Update () {
		CalcSteeringForces ();
		base.Update ();
		ultForce = Vector3.zero;
	}

	public override void CalcSteeringForces() {
        if (world != null)
        {
            // Find close enough pirates
            foreach (Pirate pirate in world.pirates)
            {
                float dist = Vector3.Distance(this.position, pirate.position);
                if (dist < radiusOfCaring * 3)
                {
                    ultForce += Seek(pirate.position);
                }
            }

            // Find close enough objects
            foreach (Obstacle obstacle in world.obstacles)
            {
                float dist = Vector3.Distance(this.position, obstacle.Position);
                if (dist < radiusOfCaring)
                {
                    Debug.Log("AVOID");
                    ultForce += AvoidObstacle(obstacle.Position);
                }
            }
        }
	}

    void OnMouseDown() {
        if(FindObjectOfType<Tools>() && FindObjectOfType<Tools>().toolSelected == 1)
            Die();
    }

    public void Die() {
        GameObject p = Instantiate(deathParticles, transform.position, Quaternion.identity);
        p.GetComponent<DeathParticles>().Type = 1;
        FindObjectOfType<World>().RemoveObject(this);
        Destroy(gameObject);
    }
}
