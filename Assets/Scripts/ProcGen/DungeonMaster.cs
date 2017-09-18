using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DungeonMaster : MonoBehaviour {
    //list of all hallway prefabs this DM has access to
    public List<GameObject> hallwayPrefabs = new List<GameObject>();
    //list of all possible hallways to be placed NORTH
    //[HideInInspector]
    public List<GameObject> possibleNorthHallways = new List<GameObject>();
    public List<GameObject> possibleSouthHallways = new List<GameObject>();
    public List<GameObject> possibleEastHallways = new List<GameObject>();
    public List<GameObject> possibleWestHallways = new List<GameObject>();

    //list of all room prefabs this DM has access to
    public List<GameObject> roomPrefabs = new List<GameObject>();
    //list of all possible rooms to be placed NORTH
    //[HideInInspector]
    public List<GameObject> possibleNorthRooms = new List<GameObject>();
    public List<GameObject> possibleSouthRooms = new List<GameObject>();
    public List<GameObject> possibleEastRooms = new List<GameObject>();
    public List<GameObject> possibleWestRooms = new List<GameObject>();


    public List<GameObject> initialPlacement = new List<GameObject>();
    public int levelSize;
    public int numberOfRooms;
    public Vector3 spawnPosition;
    public bool placedRooms = false;

    public bool successfullyPlaced = false;

    //sort prefabs into their potential lists before runtime
    void Awake()
    {
        

        SortHallways();
        SortRooms();
        //placeRooms();
    }

    void Start()
    {
        placeRooms();
    }

    void SortHallways()//sort hallways into the possible directions
    {
        foreach (GameObject prefab in hallwayPrefabs)//check every prefab
        {
            if (prefab.GetComponentsInChildren<AttachPoint>() != null)
            {
                for (int i = 0; i < prefab.GetComponentsInChildren<AttachPoint>().Length; i++)
                {
                    switch (prefab.GetComponentsInChildren<AttachPoint>()[i].thisDirection)
                    {
                        case 1:
                            possibleSouthHallways.Add(prefab);
                            break;
                        case 2:
                            possibleNorthHallways.Add(prefab);
                            break;
                        case 3:
                            possibleEastHallways.Add(prefab);
                            break;
                        case 4:
                            possibleWestHallways.Add(prefab);
                            break;
                    }

                }
            }
        }
    }

    void SortRooms()
    {
        foreach (GameObject prefab in roomPrefabs)//check every prefab
        {
            if (prefab.GetComponentsInChildren<AttachPoint>() != null)
            {
                for (int i = 0; i < prefab.GetComponentsInChildren<AttachPoint>().Length; i++)
                {
                    switch (prefab.GetComponentsInChildren<AttachPoint>()[i].thisDirection)
                    {
                        case 1:
                            possibleSouthRooms.Add(prefab);
                            break;
                        case 2:
                            possibleNorthRooms.Add(prefab);
                            break;
                        case 3:
                            possibleEastRooms.Add(prefab);
                            break;
                        case 4:
                            possibleWestRooms.Add(prefab);
                            break;
                    }

                }
            }
        }
    }

    void Update()
    {
        if(placedRooms && !successfullyPlaced)
            reshuffle();

        //Debug.Log(initialPlacement.Count);
    }


    void placeRooms()
    {
        int[] direction = new int[3];

        direction[0] = 90;
        direction[1] = 180;
        direction[2] = 270;


        
        int i;
        for (i = 0; i < numberOfRooms; i++)
        {
            GameObject nextRoom;
            Vector3 temp = Random.insideUnitSphere * levelSize;

            spawnPosition = new Vector3(Mathf.RoundToInt(temp.x), 0.0f, Mathf.RoundToInt(temp.z));

            //Quaternion rotation = new Quaternion(0.0f, Random.Range(0, direction[Random.Range(0, direction.Length)]), 0.0f, 0.0f);

            nextRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Count)], spawnPosition, transform.rotation);
            initialPlacement.Add(nextRoom);
            
        }

        if (i == numberOfRooms)
            placedRooms = true;
    }

    void reshuffle()
    {
        
            for (int i = 0; i < initialPlacement.Count; i++)
            {
                if (initialPlacement[i].GetComponentInChildren<IntersectVolume>().intersects)
                {
                    GameObject temp = initialPlacement[i];
                    initialPlacement.Remove(initialPlacement[i]);
                    Destroy(temp);
                }

            }


        //numberOfRooms -=  (numberOfRooms -initialPlacement.Count);

        //if (initialPlacement.Count == numberOfRooms)
        //successfullyPlaced = true;
        if (initialPlacement.Count <= numberOfRooms)
            placeRooms();
        //else
            //successfullyPlaced = true;
    }    
}
