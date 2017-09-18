using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectControlMethod : MonoBehaviour {

    public PlayerController thePlayer;

	// Use this for initialization
	void Start ()
    {
        thePlayer = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		//Detect mouse input
        if(Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            thePlayer.useController = false;
        }

        //Detect controller
        if (Input.GetAxisRaw("Rhorizontal") != 0 || Input.GetAxisRaw("Rvertical") != 0)
        {
            thePlayer.useController = true;
        }

        if(Input.GetKey(KeyCode.Joystick1Button0) ||
           Input.GetKey(KeyCode.Joystick1Button1) ||
            Input.GetKey(KeyCode.Joystick1Button2) ||
            Input.GetKey(KeyCode.Joystick1Button3) ||
            Input.GetKey(KeyCode.Joystick1Button4) ||
            Input.GetKey(KeyCode.Joystick1Button5) ||
            Input.GetKey(KeyCode.Joystick1Button6) ||
            Input.GetKey(KeyCode.Joystick1Button6) ||
            Input.GetKey(KeyCode.Joystick1Button7) ||
            Input.GetKey(KeyCode.Joystick1Button8) ||
            Input.GetKey(KeyCode.Joystick1Button9))
        {
            thePlayer.useController = true;
        }


    }
}
