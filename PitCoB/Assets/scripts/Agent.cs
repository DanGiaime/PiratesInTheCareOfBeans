using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : Vehicle {
    
    public float maxForce = 10f;
    public float wanderRadius = 2f;

    public GameObject deathParticles;

    /// <summary>
    /// When to stop seeking something
    /// </summary>
    public float radiusOfCaring = .1f;

	public World world;
	protected Vector3 ultForce;

	// Use this for initialization
	public override void Start () {
		ultForce = Vector3.zero;
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
        if (ultForce == Vector3.zero)
        {
            ultForce += Wander();
        }

        // Apply forces
        ApplyForce(ultForce.normalized * maxForce);
        base.Update();
	}

    public Vector3 Seek(Vector3 target, bool closerIsStronger)
    {
        Vector3 desiredVelocity = target - this.position;
        desiredVelocity = desiredVelocity.normalized * this.maxSpeed;
        Vector3 seekForce = desiredVelocity - this.velocity;
        if(closerIsStronger){
            return ForceWeight(target) * seekForce;
        }
        else {
            return InverseForceWeight(target) * seekForce;
        }
    }

    public Vector3 Seek(Vector3 target)
    {
        Vector3 desiredVelocity = target - this.position;
        desiredVelocity = desiredVelocity.normalized * this.maxSpeed;
        Vector3 seekForce = desiredVelocity - this.velocity;
        return ForceWeight(target) * seekForce;
    }

    public Vector3 Flee(Vector3 target)
    {
        Vector3 desiredVelocity = this.position - target;
        desiredVelocity = desiredVelocity.normalized * this.maxSpeed;
        Vector3 fleeForce = desiredVelocity - this.velocity;
        return ForceWeight(target) * fleeForce;
    }

    public Vector3 Pursue(Agent agent)
    {
        Vector3 agentFuturePosition = agent.position + agent.velocity.normalized;
        return Seek(agentFuturePosition);
    }

    public Vector3 Evade(Agent agent)
    {
        Vector3 agentFuturePosition = agent.position + agent.velocity.normalized;
        return Flee(agentFuturePosition);
    }

    public Vector3 Flock(int id) {
        List<Agent> agents = world.GetAgents(id);
        Vector3 center = Vector3.zero;
        foreach (Agent a in agents)
        {
            center += a.position;
        }
        center = center / agents.Count;
        return Seek(center, false);
    }

    public Vector3 AvoidObstacle(Vector3 obstaclePosition) 
	{
		float distToObj = Vector3.Distance (this.transform.position, obstaclePosition);
		Vector3 objCenter = obstaclePosition - this.transform.position;


        float dotForward = Vector3.Dot (rotation.forward, objCenter);

		//Is the object in front of us? If not, no reason to care.
		if (dotForward < 0)
			return Vector3.zero;

		//Vector3 objProjected = Vector3.Project (objCenter, this.transform.right);
        float dotRight = Vector3.Dot (rotation.right, objCenter);

		//Is the object close to us? If not, no reason to care.
		//Is the object to our right? turn Left!
		if (dotRight > 0) {
            return (rotation.right) * -1 * ForceWeight(obstaclePosition);// * obstacle.Weight;
		} 

		//Is the object to our left? turn Right!
		else if (dotRight < 0) {
            return rotation.right * ForceWeight(obstaclePosition);// * obstacle.Weight;
		} 

		//It's...right in front of us? ... RIGHTTT
		else {
            return rotation.right * ForceWeight(obstaclePosition);// * obstacle.Weight;
		}

	}

    // wander method
    // move at a max speed toward a random point on a circle
    public Vector3 Wander()
    {
        // create a desired velocity vector
        Vector3 desiredVelocity = Vector3.zero;

        Vector3 forward = (this.velocity == Vector3.zero) ? transform.right : this.velocity;

        // get the center of the "circle"
        Vector3 circleCenter = forward.normalized + this.position;

        // negate the forward vector and scale it to radius
        Vector3 displacement = -forward;
        displacement = displacement.normalized * wanderRadius;

        // rotate that same vector randomly
        displacement = Quaternion.Euler(0, 0, Random.Range(-60f, 60f)) * displacement;

        // Find desired velocity by adding out velocity to the displacement
        desiredVelocity = velocity + displacement;
        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;

        return desiredVelocity;
    }

    public float ForceWeight(Vector3 target) {
        return 1 / Mathf.Pow(Vector3.Distance(this.position, target), 2);
    }

    public float InverseForceWeight(Vector3 target)
    {
        return Mathf.Pow(Vector3.Distance(this.position, target), 1);
    }

	public abstract void CalcSteeringForces();
}
