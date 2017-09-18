using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MiniMap : MonoBehaviour {

    public GameObject theMap;

    bool displayMap;
	// Use this for initialization
	void Start ()
    {
        
        displayMap = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Joystick1Button6))
            displayMap = true;

        if (Input.GetKeyUp(KeyCode.Tab) || Input.GetKeyUp(KeyCode.Joystick1Button6))
            displayMap = false;

        if (displayMap)
            theMap.gameObject.SetActive(true);
        else
            theMap.gameObject.SetActive(false);
	}
}
