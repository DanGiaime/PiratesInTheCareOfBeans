using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : Agent {

    [HideInInspector] private static int id = 3;

    // Update is called once per frame
    public override void Update()
    {
        CalcSteeringForces();
        base.Update();
        ultForce = Vector3.zero;
    }

	public override void CalcSteeringForces() {
        ultForce += Flock(Squid.id);
	}
}
