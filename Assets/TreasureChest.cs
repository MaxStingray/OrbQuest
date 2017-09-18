using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour {

    Mesh defaultMesh;
    public Mesh swapMesh;//set in inspector, the mesh we intend to swap to

    GameObject theChest;

    public GameObject treasureObj;

	// Use this for initialization
	void Start ()
    {
        defaultMesh = GetComponent<MeshFilter>().mesh;
        theChest = transform.parent.gameObject;

        transform.position = theChest.transform.position;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SwapMesh(swapMesh);
            InstantiateTreasure(treasureObj);
        }
    }

    // Update is called once per frame
    void SwapMesh(Mesh pMesh)
    {
        if (pMesh == null) return;

        Mesh meshInstance = Instantiate(pMesh) as Mesh;

        GetComponent<MeshFilter>().mesh = meshInstance;
    }

    void InstantiateTreasure(GameObject treasure)
    {
        GameObject pickupInstance = Instantiate(treasure) as GameObject;
        pickupInstance.transform.position = transform.position;
    }
}
