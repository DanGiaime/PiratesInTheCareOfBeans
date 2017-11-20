using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour {

	private World world;

	const float MAX_X = 2f;
	const float MIN_X = -2f;
    const float MAX_Y = 2f;
	const float MIN_Y = -2f;

	// Use this for initialization
	void Start () {
        world = this.GetComponent<World>();

        foreach (Obstacle obst in gameObject.GetComponentsInChildren<Bag>())
        {
            world.obstacles.Add(obst);
        }
        foreach (Skeleton skele in gameObject.GetComponentsInChildren<Skeleton>())
        {
            world.skeletons.Add(skele);
            skele.world = world;
        }
        foreach (Pirate pirate in gameObject.GetComponentsInChildren<Pirate>())
        {
            world.pirates.Add(pirate);
            pirate.world = world;
        }
        foreach (Squid squid in gameObject.GetComponentsInChildren<Squid>())
        {
            world.squids.Add(squid);
            squid.world = world;
        }

    }
	
	// Update is called once per frame
	void Update () {

        
	}

	Vector3 randomPosition() {
		return new Vector3 (Random.Range(MIN_X, MAX_X), Random.Range(MIN_Y, MAX_Y), 1);
	}
}
