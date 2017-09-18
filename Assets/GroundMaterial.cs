using System.Collections;
using System.Collections.Generic;
using ooparts.dungen;
using UnityEngine;

public class GroundMaterial : MonoBehaviour {

    public Material[] materialArray;

    public MeshRenderer rend;
    public Material thisMat;
    public RoomMapManager man;
    int prevFloor;

	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<MeshRenderer>();
        thisMat = rend.materials[0];//get the material from the mesh renderer
        man = GameObject.FindGameObjectWithTag("Manager").GetComponent<RoomMapManager>();
        prevFloor = man.currentFloor;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (man.currentFloor > prevFloor)
        {
            prevFloor = man.currentFloor;
            thisMat = SetNextMaterial();
            prevFloor = man.currentFloor;
        }	
	}

    public Material SetNextMaterial()
    {
        Material nextMaterial;

        if (materialArray[man.currentFloor] != null)
            nextMaterial = materialArray[man.currentFloor];
        else
            nextMaterial = materialArray[0];

        return nextMaterial;
    }
}
