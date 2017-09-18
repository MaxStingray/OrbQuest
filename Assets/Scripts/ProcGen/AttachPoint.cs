using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPoint : MonoBehaviour {

    public enum directions {North = 1, South, East, West}
    public bool ATTACHED;

    public int thisDirection;

    RoomManager theRoom;
    Vector3 positionInRoom;

	// Use this for initialization
	void Start ()
    {
        theRoom = GetComponentInParent<RoomManager>();

        AssignDirection();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AssignDirection()
    {
        //temporarily based on cardinal directions, rewrite for robustness to handle off center doors and shit
        if (transform.position.z > theRoom.transform.position.z)
        {
            thisDirection = (int)directions.North;
        }
        else if (transform.position.z < theRoom.transform.position.z)
        {
            thisDirection = (int)directions.South;
        }
        else if (transform.position.x < theRoom.transform.position.x)
        {
            thisDirection = (int)directions.West;
        }
        else if (transform.position.x > theRoom.transform.position.x)
        {
            thisDirection = (int)directions.East;
        }
    }
}
