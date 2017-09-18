using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    public bool isRoom;

    DungeonMaster dungeonMaster;

    public List<GameObject> attachPoints = new List<GameObject>();

    private GameObject[] initialArray;
	// Use this for initialization
	void Start ()
    {
        dungeonMaster = FindObjectOfType<DungeonMaster>();

        if (dungeonMaster == null)
        {
            Debug.Log("no dungeon master found");
        }

        initialArray = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
        {//add all child objects to an array
            initialArray[i] = transform.GetChild(i).gameObject;
        }
        
        //from the array, add all child objects which contain an AttachPoint component
        foreach (GameObject child in initialArray)
        {
            if(child.GetComponent<AttachPoint>() != null)
            {
                attachPoints.Add(child.gameObject);
            }
        }

        //AssignNext();

        
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void AssignNext()
    {

        //if we are a room
        if (isRoom)
        {
            foreach (GameObject attachPoint in attachPoints)
            {
                if (attachPoint.GetComponent<AttachPoint>().ATTACHED == false)
                {
                    int _dir = attachPoint.GetComponent<AttachPoint>().thisDirection;
                    if (_dir == 1)
                    {
                        int whichTile = Random.Range(0, dungeonMaster.possibleNorthHallways.Count);
                        GameObject nextTile = dungeonMaster.possibleNorthHallways[whichTile];
                        Vector3 placePoint = new Vector3(attachPoint.transform.position.x, attachPoint.transform.position.y, attachPoint.transform.position.z + 4);
                        GameObject placeTile = Instantiate(nextTile, placePoint, attachPoint.transform.rotation);
                        //attachPoints.Remove(attachPoint);
                        //Destroy(attachPoint);
                    }
                    else if (_dir == 2)
                    {
                        
                        int whichTile = Random.Range(0, dungeonMaster.possibleNorthHallways.Count);
                        GameObject nextTile = dungeonMaster.possibleNorthHallways[whichTile];
                        Vector3 placePoint = new Vector3(attachPoint.transform.position.x, attachPoint.transform.position.y, attachPoint.transform.position.z - 4);
                        GameObject placeTile = Instantiate(nextTile, placePoint, attachPoint.transform.rotation);
                        //attachPoints.Remove(attachPoint);
                        //Destroy(attachPoint);
                    }
                    //attachPoints.Remove(attachPoint);
                }
                //Destroy(attachPoint);
            }
        }
        //if we are a hallway
        else if (!isRoom)
        {
            foreach (GameObject attachPoint in attachPoints)
            {
                if (attachPoint.GetComponent<AttachPoint>().ATTACHED == false)
                {
                    int _dir = attachPoint.GetComponent<AttachPoint>().thisDirection;
                    if (_dir == 1)
                    {
                        
                        int whichTile = Random.Range(0, dungeonMaster.possibleNorthRooms.Count);
                        GameObject nextTile = dungeonMaster.possibleNorthRooms[whichTile];
                        Vector3 placePoint = new Vector3(attachPoint.transform.position.x, attachPoint.transform.position.y, attachPoint.transform.position.z + 4);
                        GameObject placeTile = Instantiate(nextTile, placePoint, attachPoint.transform.rotation);

                        //attachPoints.Remove(attachPoint);

                    }
                    else if (_dir == 2)
                    {
                        
                        int whichTile = Random.Range(0, dungeonMaster.possibleNorthRooms.Count);
                        GameObject nextTile = dungeonMaster.possibleNorthHallways[whichTile];
                        Vector3 placePoint = new Vector3(attachPoint.transform.position.x, attachPoint.transform.position.y, attachPoint.transform.position.z - 4);
                        GameObject placeTile = Instantiate(nextTile, placePoint, attachPoint.transform.rotation);
                        //attachPoints.Remove(attachPoint);

                    }
                    //attachPoints.Remove(attachPoint);
                }
                //Destroy(attachPoint);
            }
        }   

            

            /*foreach (GameObject attachPoint in attachPoints)
            {
                //if the attach point is north facing and has not already been attached...
                if(attachPoint.GetComponent<AttachPoint>().thisDirection == 1
                    && attachPoint.GetComponent<AttachPoint>().ATTACHED == false)
                {
                    int hallwayOrRoom = Random.Range(0, 2);
                    
                    //select from the DM's list...
                    attachPoint.GetComponent<AttachPoint>().ATTACHED = true;
                    if (hallwayOrRoom == 0)
                    {
                        int whichTile = Random.Range(0, dungeonMaster.possibleNorthRooms.Count);
                        //store tile at index to gameObject
                        GameObject nextTile = dungeonMaster.possibleNorthRooms[whichTile];

                        //create position
                        Vector3 placePoint = new Vector3(attachPoint.transform.position.x, attachPoint.transform.position.y, attachPoint.transform.position.z + 5);
                        //PLACE IT
                        GameObject placeTile = Instantiate(nextTile, placePoint, attachPoint.transform.rotation);
                    }
                    else if (hallwayOrRoom == 1)
                    {
                        int whichTile = Random.Range(0, dungeonMaster.possibleNorthHallways.Count);
                        GameObject nextTile = dungeonMaster.possibleNorthHallways[whichTile];
                        //create position
                        Vector3 placePoint = new Vector3(attachPoint.transform.position.x, attachPoint.transform.position.y, attachPoint.transform.position.z + 5);
                        //PLACE IT
                        GameObject placeTile = Instantiate(nextTile, placePoint, attachPoint.transform.rotation);
                    }
                }
            }*/
        
    }
}
