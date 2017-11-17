using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour {

	protected Vector3 acceleration;
	public Vector3 velocity;
    public Vector3 position;
    public Transform rotation;
    public float maxSpeed = 1f;
	private float mass = 2f;
    public float radius = 0.5f;

    // Use this for initialization
    public virtual void Start () {
		this.acceleration = Vector3.zero;
		this.velocity = Vector3.zero;
		this.position = transform.position;
        this.rotation = transform.GetChild(1);

		float vertExtent = Camera.main.orthographicSize;    
		float horzExtent = vertExtent * Screen.width / Screen.height;


	}

    // Update is called once per frame
    public virtual void Update () {
		velocity += acceleration * Time.deltaTime;
		velocity.z = 0;
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
		position += velocity * Time.deltaTime;
		transform.position = new Vector3(position.x, position.y, 0);
		acceleration = Vector3.zero;
        rotation.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(velocity.x, velocity.y));
        Debug.DrawLine(this.position, this.position + this.velocity);
	}

	public void ApplyForce(Vector3 force) {
		acceleration += force / mass;
	}

	public float Mass {
		get { return mass;}
		set { mass = value;}
	}
}
