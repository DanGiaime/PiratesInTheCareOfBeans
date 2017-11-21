using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour{

    public List<Agent> pirates = new List<Agent>();
    public List<Agent> skeletons = new List<Agent>();
    public List<Agent> squids = new List<Agent>();
    public List<Obstacle> obstacles = new List<Obstacle>();

    public List<Bag> bags = new List<Bag>();
    public List<Box> boxes = new List<Box>();
    public List<Bean> beans = new List<Bean>();
    public List<Bomb> bombs = new List<Bomb>();

    public float radiusY = 1f;
    public float radiusX = 1f;
    public Vector2 center;

    void Start() {
        
    }

    public bool IsInBounds(Vector2 position) {
        float x = position.x;
        float y = position.y;
        float h = center.x;
        float k = center.y;
        float xTerm = Mathf.Pow(x - h, 2) / Mathf.Pow(radiusX, 2);
        float yTerm = Mathf.Pow(y - k, 2) / Mathf.Pow(radiusY, 2);
        bool inBounds = (xTerm + yTerm <= 1);
        return inBounds;
    }

    public void RemoveObject(Object obj) {
        if (obj is Skeleton)
            skeletons.Remove((Agent)obj);
        else if (obj is Pirate)
            pirates.Remove((Agent)obj);
        else if (obj is Squid)
            squids.Remove((Agent)obj);
        else if (obj is Obstacle)
            obstacles.Remove((Obstacle)obj);
    }

    public List<Agent> GetAgents(int id) {
        switch (id)
        {
            case 1:
                return pirates;
            case 2:
                return skeletons;
            case 3:
                return squids;
            default:
                return null;
        }
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

    public void DoExplosion(Vector2 origin, float radius)
    {
        List<Skeleton> dead = new List<Skeleton>();

        foreach (Skeleton s in skeletons)
        {
            if (Vector2.Distance(s.position, origin) < radius)
                dead.Add(s);
        }

        foreach (Skeleton s in dead)
        {
            s.Die();
        }
    }

}
