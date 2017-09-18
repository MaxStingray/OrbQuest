using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsText : MonoBehaviour {

    Text theText;
    UIManager manager;

    

    void Awake()
    {
        theText = GetComponent<Text>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<UIManager>();
    }

	// Use this for initialization
	void Start ()
    {

    }

    public void Refresh()
    {
        theText.text = "Ye braved the depths and finished your quest in " + manager.minutes + ":" + manager.seconds +
            ". On your travels ye vanquished " + manager.killCount + " hell bastards.";
    }

	// Update is called once per frame
	void Update () {
		
	}
}
