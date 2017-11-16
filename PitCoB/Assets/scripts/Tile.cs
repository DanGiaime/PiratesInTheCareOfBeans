using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
			
	}



    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("COLLISION");

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) //check the int value in layer manager(User Defined starts at 8))
        {
            collision.gameObject.GetComponent<Vehicle>().isGrounded = true;
            Debug.Log("ONGROUND");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) //check the int value in layer manager(User Defined starts at 8))
        {
            collision.gameObject.GetComponent<Vehicle>().isGrounded = false;
            Debug.Log("FALL");
        }
    }
}
