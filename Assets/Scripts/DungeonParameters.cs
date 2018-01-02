using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonParameters : MonoBehaviour {

    public SettingsMenu settingsMenu;

    public int sizeX;
    public int sizeZ;
    public int maxRooms;
    public int numFloors;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
