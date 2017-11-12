using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

	public List<Pirate> pirates;
	public List<Skeleton> skeletons;
	public List<Squid> squids;
	public List<Obstacle> obstacles;

	// Use this for initialization
	public World () {
        pirates = new List<Pirate>();
        skeletons = new List<Skeleton>();
        squids = new List<Squid>();
        obstacles = new List<Obstacle>();
	}
}
