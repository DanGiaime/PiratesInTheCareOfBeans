using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : Obstacle {

    [HideInInspector] public Vector3 position;
	[SerializeField] private static float weight = 10f;

	public override float Weight {
		get {
			return weight;
		}
	}

    public override Vector3 Position
    {
        get
        {
            return position;
        }
    }


    // Use this for initialization
    void Start () {
        this.position = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
