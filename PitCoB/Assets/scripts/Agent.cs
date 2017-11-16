using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : Vehicle {

	public float seekWeight = 2f;
	public float fleeWeight = 10f;
    public float avoidWeight = 100f;
    public float maxForce = 10f;
    public float wanderRadius = 2f;

    /// <summary>
    /// When to stop seeking something
    /// </summary>
    public float distanceToObject = .1f;

    public Vector3 seekObject;
    public Vector3 fleeObject;
    public Vector3 avoidObject;

    /// <summary>
    /// How far before the edge of the map I want to start avoiding the edge
    /// </summary>
    public float edgeRange = .01f;


	public World world;
	protected Vector3 ultForce;

	// Use this for initialization
	void Start () {
        seekObject = Vector3.zero;
        fleeObject = Vector3.zero;
        avoidObject = Vector3.zero;
		ultForce = Vector3.zero;
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
        //Debug.Log("Update");
        // Check if we're seeking
        if (seekObject != Vector3.zero)
        {
            if (Vector3.Distance(position, seekObject) > distanceToObject)
            {
                Debug.Log("seeking");
                ultForce += Seek(seekObject);
            }
            else
            {
                seekObject = Vector3.zero;
            }
        }

        // Check if we're fleeing
        else if (fleeObject != Vector3.zero)
        {

            if (Vector3.Distance(position, fleeObject) < distanceToObject) {
                Debug.Log("fleeing");
                ultForce += Flee(fleeObject);
            }
            else {
                fleeObject = Vector3.zero;
            }
        }

        // Check if we're avoiding
        else if (avoidObject != Vector3.zero)
        {
            if (Vector3.Distance(position, avoidObject) < distanceToObject)
            {
                Debug.Log("avoiding");
                ultForce += AvoidObstacle(avoidObject);
            }
            else {
                avoidObject = Vector3.zero;
            }
        }

        // If none of the above, wander
        else {
            Debug.Log("wandering");
            ultForce += Wander();
        }

        // Apply forces
        this.ApplyForce(ultForce.normalized * maxForce);
        base.Update();
	}

    public Vector3 Seek(Vector3 target)
    {
        Vector3 desiredVelocity = target - this.position;
        desiredVelocity = desiredVelocity.normalized * this.maxSpeed;
        Vector3 seekForce = desiredVelocity - this.velocity;
        return seekWeight * seekForce;
    }

    public Vector3 Flee(Vector3 target)
    {
        Vector3 desiredVelocity = this.position - target;
        desiredVelocity = desiredVelocity.normalized * this.maxSpeed;
        Vector3 fleeForce = desiredVelocity - this.velocity;
        return fleeWeight * fleeForce;
    }

    public Vector3 AvoidObstacle(Vector3 obstaclePosition) 
	{
		float distToObj = Vector3.Distance (this.transform.position, obstaclePosition);
		Vector3 objCenter = obstaclePosition - this.transform.position;


        float dotForward = Vector3.Dot ((this.rotation * this.transform.forward), objCenter);

		//Is the object in front of us? If not, no reason to care.
		if (dotForward < 0)
			return Vector3.zero;

		//Vector3 objProjected = Vector3.Project (objCenter, this.transform.right);
        float dotRight = Vector3.Dot ((this.rotation * this.transform.right), objCenter);

		//Is the object close to us? If not, no reason to care.
		//if (distToObj < maxSpeed) {
            Debug.Log("AVOID");

			//Is the object to our right? turn Left!
			if (dotRight > 0) {
                return (this.rotation * this.transform.right) * -1 * avoidWeight;// * obstacle.Weight;
			} 

			//Is the object to our left? turn Right!
			else if (dotRight < 0) {
                return (this.rotation * this.transform.right) * avoidWeight;// * obstacle.Weight;
			} 

			//It's...right in front of us? ... RIGHTTT
			else {
                return (this.rotation * this.transform.right) * avoidWeight;// * obstacle.Weight;
			}
		//} else {
		//	return Vector3.zero;
		//}

	}

    // wander method
    // move at a max speed toward a random point on a circle
    public Vector3 Wander()
    {
        // create a desired velocity vector
        Vector3 desiredVelocity = Vector3.zero;

        // get the center of the "circle"
        Vector3 circleCenter = this.velocity;
        circleCenter.Normalize();

        circleCenter += this.transform.position;

        // negate the forward vector and scale it to radius
        Vector3 displacement = -this.velocity;
        displacement = displacement.normalized * wanderRadius;

        displacement = Quaternion.AngleAxis(Random.Range(-60, 60), velocity.normalized) * displacement;

        desiredVelocity = transform.up + displacement;
        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;


        return desiredVelocity - velocity;
    }

    /// <summary>
    /// Avoids the edges by Fleeing future positions
    /// </summary>
    /// <returns>Force to avoid edges</returns>
    public Vector3 AvoidEdges() {
        Vector3 fromMeToEdge = velocity.normalized * edgeRange;
        Vector3 futurePosition = position + fromMeToEdge;
        Debug.DrawLine(position, futurePosition);
        if(Physics2D.Raycast(position, velocity.normalized, edgeRange)) {
            Debug.Log("Gonna die");
            for (int i = 0; i < 21; i++)
            {
                fromMeToEdge = Quaternion.Euler(0, 0, 15f) * fromMeToEdge;
                Debug.DrawLine(position, fromMeToEdge + position);
                if (!Physics2D.Raycast(position, fromMeToEdge.normalized, edgeRange))
                {
                    Debug.Log("Found a way out");
                    return position + fromMeToEdge;
                }
            }
            return Vector3.zero;
        }
        Debug.Log("Not Gonna die");
        return Vector3.zero;
    }

    public void SetFleeTarget(Vector3 fleeTarget)
    {
        if (this.fleeObject == Vector3.zero)
        {
            this.fleeObject = fleeTarget;
            this.seekObject = Vector3.zero;
            this.avoidObject = Vector3.zero;
        }
    }
    public void SetSeekTarget(Vector3 seekTarget)
    {
        if (this.seekObject == Vector3.zero)
        {
            //Debug.Log("Seek target set");
            this.fleeObject = Vector3.zero;
            this.seekObject = seekTarget;
            this.avoidObject = Vector3.zero;
        }

    }
    public void SetAvoidTarget(Vector3 avoidTarget)
    {
        if (this.avoidObject == Vector3.zero)
        {
            this.fleeObject = Vector3.zero;
            this.seekObject = Vector3.zero;
            this.avoidObject = avoidTarget;
        }
    }





	public abstract void CalcSteeringForces();
}
