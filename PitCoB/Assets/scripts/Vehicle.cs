﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour {

	protected Vector3 acceleration;
	public Vector3 velocity;
	public Vector3 position;
    public float maxSpeed = 3f;
	private float mass = 2f;
	public bool friction = true;
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;

    public string text;

    // Use this for initialization
	public virtual void Start () {
		this.acceleration = Vector3.zero;
		this.velocity = Vector3.zero;
		this.position = transform.position;

		float vertExtent = Camera.main.orthographicSize;    
		float horzExtent = vertExtent * Screen.width / Screen.height;

		// Calculations assume map is position at the origin
//		minX = Camera.main.transform.position.x - horzExtent;
//		maxX = Camera.main.transform.position.x + horzExtent;
//		minY = Camera.main.transform.position.x - vertExtent;
//		maxY = Camera.main.transform.position.x + vertExtent;

	}

    // Update is called once per frame
	public virtual void Update () {
		velocity += acceleration * Time.deltaTime;
		position += velocity * Time.deltaTime;
		transform.position = new Vector3(position.x, 0, position.z);
		acceleration = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x));
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

//	public void Bounce() {
//		if (position.x < minX || position.x > maxX) {
//			velocity.x *= -1;
//		}
//		if (position.y < minY || position.y > maxY) {
//			velocity.y *= -1;
//		}
//	}

	public float Mass {
		get { return mass;}
		set { mass = value;}
	}
}
