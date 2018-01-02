using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

    GameObject thePlayer;
    Vector3 playerPos;


    public bool mapCamera;
    public bool menu;
	// Use this for initialization
	void Start ()
    {
        //thePlayer = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(thePlayer == null)
            thePlayer = GameObject.FindGameObjectWithTag("Player");

        if (!menu)
        {
            if (thePlayer != null)
            {
                if (!mapCamera)
                    playerPos = new Vector3(thePlayer.transform.position.x, 6.17f, thePlayer.transform.position.z - 3.75f);
                else
                    playerPos = new Vector3(thePlayer.transform.position.x, 54f, thePlayer.transform.position.z - 30);
            }
        }
        else
        {
            playerPos = new Vector3(thePlayer.transform.position.x, 4.95f, thePlayer.transform.position.z - 3.5f);
        }

        transform.position = playerPos;	
	}
}
