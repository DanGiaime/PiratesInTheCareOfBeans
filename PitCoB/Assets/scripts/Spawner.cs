using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour {

	Vehicle[] children;
    public GameObject skeleton;
    public GameObject pirate;
	public GameObject PSG;

	private World world;

	const float MAX_X = 2f;
	const float MIN_X = -2f;
    const float MAX_Y = 2f;
	const float MIN_Y = -2f;

	// Use this for initialization
	void Start () {
		world = new World ();

		for (int i = 0; i < 5; i++) {
			GameObject newskeleton = Instantiate (skeleton, randomPosition (), Quaternion.identity, transform);
			newskeleton.transform.Rotate (0, 0, Random.Range (0f, 6.28f));
			newskeleton.GetComponentInChildren<Agent> ().Mass = 1f;
            //world.skeletons.Add(newskeleton.GetComponent<Skeleton>());
			newskeleton.GetComponent<Skeleton> ().world = world;

			GameObject newpirate = Instantiate (pirate, randomPosition(), Quaternion.identity, transform);
			newpirate.transform.Rotate(0, 0, Random.Range(0f, 6.28f));
			newpirate.GetComponentInChildren<Agent>().Mass = 1f;
            //world.pirates.Add(newpirate.GetComponent<Pirate>());
			newpirate.GetComponent<Pirate> ().world = world;

			newskeleton.GetComponent<Skeleton> ().pirateTarget = newpirate.GetComponent<Pirate>();
			newpirate.GetComponent<Pirate> ().skeletonClosest = newskeleton.GetComponent<Skeleton>();

			world.pirates.Add(newpirate.GetComponent<Pirate>());
            world.skeletons.Add(newskeleton.GetComponent<Skeleton>());
		}

		//for (int i = 0; i < 50; i++) {

		//	GameObject newObject = Instantiate (PSG, randomPosition(), Quaternion.identity, transform);
		//	newObject.transform.Rotate(0, 0, Random.Range(0f, 6.28f));
		//	world.obstacles.Add(newObject);

		//}
			
        
	}
	
	// Update is called once per frame
	void Update () {

        
	}

	Vector3 randomPosition() {
		return new Vector3 (Random.Range(MIN_X, MAX_X), Random.Range(MIN_Y, MAX_Y), 1);
	}
}
