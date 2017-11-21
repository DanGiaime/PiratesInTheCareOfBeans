using UnityEngine;
using System.Collections;

public class Collision : MonoBehaviour
{

    private World world;
    static float agentRadius = .1f;
    public GameObject skeletonPrefab;

	// Use this for initialization
	void Start()
	{
        this.world = gameObject.GetComponent<World>();
	}

	// Update is called once per frame
	void Update()
	{
        foreach (Skeleton skele in world.skeletons)
        {
            for (int i = 0; i < world.pirates.Count; i++)
            {
                if (AreCollided(skele.position, world.pirates[i].position))
                {
                    Vector3 position = world.pirates[i].position;
                    Destroy(world.pirates[i].gameObject);
                    world.pirates.RemoveAt(i);
                    i--;
                    GameObject newSkeleton = Instantiate(skeletonPrefab, position, Quaternion.identity, transform);
                    world.skeletons.Add(newSkeleton.GetComponent<Skeleton>());
                }
            }
        }
    }

    public static bool AreCollided(Vector3 a, Vector3 b)
    {
        

        //Check if collided
        bool collided = (2 * agentRadius > Vector3.Distance(a, b));
        return collided;
    }
}
