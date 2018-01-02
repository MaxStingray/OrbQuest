using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ooparts.dungen;

public class Stairs : MonoBehaviour {

    public bool reachedStairs;

   

    void Awake()
    {
    }

	// Use this for initialization
	void Start ()
    {
        reachedStairs = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<DataManager>().Save();
            other.gameObject.GetComponentInChildren<WeaponStats>().CreateWeaponPrefab();
            reachedStairs = true;
        }
    }
}
