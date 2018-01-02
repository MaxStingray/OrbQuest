using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {
    MainMenu menu;


	// Use this for initialization
	void Start ()
    {
        menu = GetComponent<MainMenu>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            menu.StartMenuShow();
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            menu.StartMenuHide();
        }
    }
}
