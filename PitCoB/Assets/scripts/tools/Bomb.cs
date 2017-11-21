using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    
    public Vector2 target;
    public float fallSpeed = 30f;
    [SerializeField] private static float weight = 15f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target, fallSpeed * Time.deltaTime);
    }
}
