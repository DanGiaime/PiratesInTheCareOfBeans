using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : Obstacle {

    [SerializeField]
    private static float weight = 20f;

    public Vector2 target;
    public float fallSpeed;

    public override float Weight {
        get {
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
    void Start() {
    }

    // Update is called once per frame
    public override void Update() {
        transform.position = Vector3.MoveTowards(transform.position, target, fallSpeed * Time.deltaTime);
        base.Update();
    }
}
