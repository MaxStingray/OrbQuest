using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCount : MonoBehaviour {

    Text theText;

	// Use this for initialization
	void Start ()
    {
        theText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        theText.text = "Enemy Count: " + GameObject.FindGameObjectsWithTag("Goblin").Length;		
	}
}
