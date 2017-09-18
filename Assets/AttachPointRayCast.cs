using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPointRayCast : MonoBehaviour {

    public HallwayGen hallway;
    
    public bool drawn = false;
    public Vector3 hitPoint;
    // Use this for initialization
    void Awake ()
    {
      

        Vector3 fwd = transform.TransformDirection(Vector3.forward) * 10;

        RaycastHit hit;

        GameObject connectedRoom;//a reference to the room we connect to, if the room is destroyed we will need to redraw the hallways. This check must happen in UPDATE until we have successfully drawn the full dungeon

        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            Debug.Log("Intersection found");
            if (hit.collider.tag == "Wall" && drawn == false)
            {
                hallway.roomDistance = hit.point - transform.position;
                Debug.Log("hit wall");
                hitPoint = new Vector3(hit.point.x, 0f, hit.point.z);
                float dist = Vector3.Distance(transform.position, hitPoint);
                hallway.hallwayLength = Mathf.RoundToInt(dist);
                Destroy(hit.collider.gameObject);
                drawn = true;
            }


            
        }
        else//if we don't find a wall
        {
            //TODO: replace this with a set of cubes and destroy
            Destroy(gameObject);
        }

        Debug.DrawRay(transform.position, fwd, Color.red);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
       
    }
}
