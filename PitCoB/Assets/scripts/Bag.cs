using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : Obstacle {

	[SerializeField]private static float weight = 10f;

	public override float Weight {
		get {
			return weight;
		}
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
