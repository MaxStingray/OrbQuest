using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ooparts.dungen;
using Pathfinding;

public class EnemyController : MonoBehaviour {

    //private Rigidbody myRB;
    public float moveSpeed;

    public float minMoveSpeed;
    public float maxMoveSpeed;

    public PlayerController thePlayer;//possibly change to generic target transform
    public Vector3 playerTransform;

    private CharacterController controller;

    private Seeker seeker;
    public Path path;

    public float nextWayPointDistance = 3;//the distance to the next waypoint
    private int currentWaypoint = 0;
    public float repathRate = 0.5f;//how often to recalculate the path (in seconds)
    private float lastRepath = -9999;

	// Use this for initialization
	void Start ()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        //myRB = GetComponent<Rigidbody>();
        thePlayer = FindObjectOfType<PlayerController>();//possiby change to tag

        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
	}

    public void OnPathComplete(Path p)
    {
        //Debug.Log("returned path. Error? " + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    /*void FixedUpdate()
    {
        if (RoomMapManager.generated != true)
            return;
        else
            myRB.velocity = (transform.forward * moveSpeed);
        
    }*/

	// Update is called once per frame
	void FixedUpdate ()
    {
        if (RoomMapManager.generated != true)
            return;
        else
        {
            if (thePlayer != null)
            {
                playerTransform = new Vector3(thePlayer.transform.position.x, 0.5f, thePlayer.transform.position.z);
                transform.LookAt(playerTransform);
                //transform.LookAt(path.vectorPath[currentWaypoint + 1]);

                if (Time.time - lastRepath > repathRate && seeker.IsDone())
                {
                    //start a new path to the target position, call the OnPathComplete function
                    seeker.StartPath(transform.position, playerTransform, OnPathComplete);
                }

                if (path == null)
                {
                    //no path yet, do nothing
                    return;
                }

                if (currentWaypoint > path.vectorPath.Count)
                    return;
                if (currentWaypoint == path.vectorPath.Count)
                {
                    //Debug.Log("End of Path Reached");
                    currentWaypoint++;
                    return;
                }

                //direction to next waypoint
                Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
                dir *= moveSpeed;
                //simplemove takes velocity in m/s, do not multiply by Time.deltaTime
                controller.SimpleMove(dir);

                if ((transform.position - path.vectorPath[currentWaypoint]).sqrMagnitude < nextWayPointDistance * nextWayPointDistance)
                {
                    currentWaypoint++;
                    return;
                }

            }
        }
        
        //transform.LookAt(new Vector3(thePlayer.transform.position.x, 0.0f, thePlayer.transform.position.z));
	}
}
