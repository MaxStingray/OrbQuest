using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SettingsMenu : MonoBehaviour {

    public DungeonParameters DP;
    public bool loadScene = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void smallDungeon()
    {
        DP.sizeX = 50;
        DP.sizeZ = 50;
        DP.maxRooms = 10;
        DP.numFloors = 3;
        Time.timeScale = 1;
        loadScene = true;
    }

    public void mediumDungeon()
    {
        DP.sizeX = 60;
        DP.sizeZ = 60;
        DP.maxRooms = 18;
        DP.numFloors = 5;
        Time.timeScale = 1;
        loadScene = true;
    }

    public void largeDungeon()
    {
        DP.sizeX = 200;
        DP.sizeZ = 200;
        DP.maxRooms = 50;
        DP.numFloors = 8;
        Time.timeScale = 1;
        loadScene = true;
    }
}
