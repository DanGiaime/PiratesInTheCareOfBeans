using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : Obstacle {

<<<<<<< HEAD
    [HideInInspector]
    public Vector3 position;
    [SerializeField]
    private static float weight = 10f;

    public Vector2 target;
    public float fallSpeed;

    public override float Weight {
        get {
            return weight;
        }
    }

    public override Vector3 Position {
        get {
=======
    [HideInInspector] public Vector3 position;
	[SerializeField] private static float weight = 20f;

	public override float Weight {
		get {
			return weight;
		}
	}

    public override Vector3 Position
    {
        get
        {
>>>>>>> fcf851ea1dc162569d6a5a8ceb1e76527328fdfa
            return position;
        }
    }


    // Use this for initialization
    void Start() {
        this.position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, target, fallSpeed * Time.deltaTime);
    }
}
