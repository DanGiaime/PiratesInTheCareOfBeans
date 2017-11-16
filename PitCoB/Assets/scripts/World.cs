using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour{

	public List<Pirate> pirates;
	public List<Skeleton> skeletons;
	public List<Squid> squids;
	public List<Obstacle> obstacles;

    public List<Bag> bags;
    public List<Box> boxes;
    public List<Bean> beans;
    public List<Bomb> bombs;

    public float radius;

    void Start() {
        pirates = new List<Pirate>();
        skeletons = new List<Skeleton>();
        squids = new List<Squid>();
        obstacles = new List<Obstacle>();
        bags = new List<Bag>();
        boxes = new List<Box>();
        beans = new List<Bean>();
        bombs = new List<Bomb>();
    }

    public void ClearObjects()
    {
        foreach(Bag bag in bags)
            Destroy(bag.gameObject);

        foreach (Box box in boxes)
            Destroy(box.gameObject);

        foreach (Bean bean in beans)
            Destroy(bean.gameObject);

        foreach (Bomb bomb in bombs)
            Destroy(bomb.gameObject);

        bags = new List<Bag>();
        boxes = new List<Box>();
        beans = new List<Bean>();
        bombs = new List<Bomb>();
    }
}
