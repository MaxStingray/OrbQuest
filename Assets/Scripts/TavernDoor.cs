using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernDoor : MonoBehaviour {

    MainMenu menu;

	// Use this for initialization
	void Start ()
    {
        menu = FindObjectOfType<MainMenu>();
        if (menu != null)
        {
            Debug.Log("main menu found");
        }	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            menu.ExitPanelShow();
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            menu.ExitPanelHide();
        }
    }
}
