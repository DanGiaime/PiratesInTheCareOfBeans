using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour {

	protected Vector3 acceleration;
	public Vector3 velocity;
    public Vector3 position;
    public Quaternion rotation;
    public float maxSpeed = 1f;
	private float mass = 2f;
	public bool friction = true;
    public bool isGrounded;

    // Use this for initialization
    protected void Start () {
		this.acceleration = Vector3.zero;
		this.velocity = Vector3.zero;
		this.position = transform.position;
        this.rotation = Quaternion.identity;
        this.isGrounded = true;

		float vertExtent = Camera.main.orthographicSize;    
		float horzExtent = vertExtent * Screen.width / Screen.height;


	}

    // Update is called once per frame
    public virtual void Update () {
		velocity += acceleration * Time.deltaTime;
		velocity.z = 0;
		position += velocity * Time.deltaTime;
		transform.position = new Vector3(position.x, position.y, 0);
		acceleration = Vector3.zero;
        rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(velocity.x, velocity.y));
	}

	public void ApplyForce(Vector3 force) {
		acceleration += force / mass;
	}

	public void ApplyFriction() {
		acceleration += -1 * velocity * .01f;
	}

	public void ApplyMouseForce() {
		Vector3 mousePosition = Input.mousePosition + new Vector3(0, 0, 10);
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);
		Vector2 mouseForce = ((Vector3)this.position - mousePosition)/6;
		Debug.Log (this.position);
		Debug.Log (mousePosition/300);
		ApplyForce(mouseForce);
	}

	public float Mass {
		get { return mass;}
		set { mass = value;}
	}
}
