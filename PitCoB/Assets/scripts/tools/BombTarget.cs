using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTarget : Obstacle {
    [SerializeField]
    float weight;

    [SerializeField]
    float timer, radius;
    float t;

    [SerializeField]
    GameObject explosion;

    public override Vector3 Position {
        get {
            return transform.position;
        }
    }

    public override float Weight {
        get {
            return weight;
        }
    }

    // Use this for initialization
    void Start () {
        FindObjectOfType<World>().obstacles.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
		if(t < timer) {
            t += Time.deltaTime;
            if(t >= timer) {
                Explode();
            }
        }
	}

    void Explode() {
        Instantiate(explosion, transform.position, transform.rotation);

        FindObjectOfType<World>().DoExplosion(Position, radius);
        FindObjectOfType<World>().RemoveObject(this);

        Destroy(gameObject);
    }
}
