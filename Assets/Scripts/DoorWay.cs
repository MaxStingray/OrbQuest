using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWay : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Collided");
        if (other.gameObject.tag == "Wall")
        {
            Destroy(other.gameObject);
        }
    }
}
