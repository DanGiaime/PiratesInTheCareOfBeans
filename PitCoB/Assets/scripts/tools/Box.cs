using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Obstacle {

    [SerializeField] private static float weight = 10f;

    public float lethalTimer;
    float t;

    public float radius;

    World w;

    public override float Weight {
        get {
            return 0;
        }
    }

    public override Vector3 Position {
        get {
            return transform.position;
        }
    }

    // Use this for initialization
    void Start () {
        t = lethalTimer;
        w = FindObjectOfType<World>();

        w.obstacles.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
		if(t > 0) {
            t -= Time.deltaTime;
            if(t <= 0) {
                t = 0;
            }
        }
	}
}
