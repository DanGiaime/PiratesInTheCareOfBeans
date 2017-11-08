using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAnimationTest : MonoBehaviour {

    [SerializeField]
    float forceMax;

    [SerializeField]
    bool controlForce;

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 force = new Vector3();

        if (controlForce) {
            force.x += Input.GetAxisRaw("Horizontal");
            force.y += Input.GetAxisRaw("Vertical");
        }

        force *= forceMax;

        anim.SetFloat("ForceX", force.x);
        anim.SetFloat("ForceY", force.y);
    }
}
