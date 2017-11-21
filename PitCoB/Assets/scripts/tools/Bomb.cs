using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

<<<<<<< HEAD
    public Vector2 target;
    public float fallSpeed = 30f;
=======
    [SerializeField] private static float weight = 15f;
>>>>>>> fcf851ea1dc162569d6a5a8ceb1e76527328fdfa

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target, fallSpeed * Time.deltaTime);
    }
}
