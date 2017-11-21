using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Obstacle {
    
    public Vector2 target;
    public float fallSpeed = 30f;
    [SerializeField] private static float weight = 15f;


    public override float Weight
    {
        get
        {
            return weight;
        }
    }

    public override Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public override void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target, fallSpeed * Time.deltaTime);
        base.Update();
    }
}
