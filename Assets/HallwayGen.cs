using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayGen : MonoBehaviour {

    public int hallwayLength;

    DungeonMaster DM;

    public GameObject hallwayTile;
    public GameObject ender;
    public GameObject target;

    public Vector3 roomDistance;

    public bool GenerationCompleted = false;

    bool onlyOnce = false;

    bool drawnHallway;
	// Use this for initialization
	void Start ()
    {

        DM = FindObjectOfType<DungeonMaster>();
        DrawHallway();
        

    }
	
	// Update is called once per frame
	void Update ()
    {
        
       
    }

    void DrawHallway()
    {
        if (drawnHallway == false)
        {
            for (int i = 0; i < hallwayLength + 1; i++)
            {
                //TODO: place torches at intervals and cap off with doorways    
                GameObject nextTile;

                Vector3 spawnPointTemp = transform.position + ((roomDistance / (hallwayLength)) * i);
                Vector3 spawnPoint = new Vector3(spawnPointTemp.x, 0.0f, spawnPointTemp.z);

                if (i < hallwayLength)
                {
                    nextTile = Instantiate(hallwayTile, spawnPoint, transform.rotation);
                    nextTile.transform.parent = gameObject.transform;
                }
                else if (i >= hallwayLength)
                {
                    if (ender != null)
                    {
                        nextTile = Instantiate(ender, spawnPoint, transform.rotation);
                        nextTile.transform.parent = gameObject.transform;
                        drawnHallway = true;
                    }
                    else
                        Debug.Log("no ender, possible cause?");
                    
                }
            }
        }
    }
}
