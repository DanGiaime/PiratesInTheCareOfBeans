using UnityEngine;
using System.Collections;

public class Collision : MonoBehaviour
{

    private World world;
    public static float agentRadius = .1f;
    public GameObject skeletonPrefab;

	// Use this for initialization
	void Start()
	{
        this.world = gameObject.GetComponent<World>();
	}

	// Update is called once per frame
	void Update()
	{
        for (int j = 0; j < world.skeletons.Count; j++)
        {
            for (int i = 0; i < world.pirates.Count; i++)
            {
                Debug.Log(Vector2.Distance(world.skeletons[j].position, world.pirates[i].position) < agentRadius * 2);
                if ((world.skeletons[j].position != Vector3.zero) && AreCollided(world.skeletons[j].position, world.pirates[i].position))
                {
                    Vector3 position = world.pirates[i].position;
                    Destroy(world.pirates[i].gameObject);
                    world.pirates.RemoveAt(i);
                    i--;
                    GameObject newSkeleton = Instantiate(skeletonPrefab, position, Quaternion.identity, transform);
                    world.skeletons.Add(newSkeleton.GetComponent<Skeleton>());
                    newSkeleton.GetComponent<Skeleton>().world = world;


                    if (FindObjectOfType<LevelData>()) {
                        FindObjectOfType<LevelData>().CheckWinStates();
                    } else {
                        Debug.Log("No level data found");
                    }
                }
            }
        }
    }

    public static bool AreCollided(Vector3 a, Vector3 b)
    {
        

        //Check if collided
        bool collided = ((agentRadius * 2.0f) > Vector2.Distance(a, b));
        return collided;
    }
}
