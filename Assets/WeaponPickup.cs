using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    public string weaponName;

    public string weaponDescription;

    UIManager manager;


    public WeaponStats thisStats;
    // Use this for initialization

    void Awake()
    {
        thisStats = GetComponent<WeaponStats>();
        if (thisStats != null)
        {
            Debug.Log("found stats");
        }
    }
   
    void Start ()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<UIManager>();
        //thisStats = GetComponent<WeaponStats>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            manager.showStats(gameObject, other.gameObject);//pass this script as an argument to UImanager function, as well as the collided player
        }
        /*
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(weaponName + " picked up!");
            WeaponStats playerStats = other.gameObject.GetComponentInChildren<WeaponStats>();
            WeaponController playerController = other.gameObject.GetComponentInChildren<WeaponController>();
            WeaponStats thisStats = GetComponent<WeaponStats>();

            playerStats.weaponName = thisStats.weaponName;
            playerStats.damage = thisStats.damage;
            playerStats.model = thisStats.model;
            playerStats.projectile = thisStats.projectile;
            playerController.timeBetweenShots = thisStats._rOf;//save from here into save/load method in data manager
            playerController.projectileSpeed = thisStats._projSpeed;
            playerStats.CreateWeaponPrefab();
            playerStats.loadedStats = false;
            KillThis();
        }

    */
    }

    public void Assign(GameObject player)
    {
        Debug.Log(weaponName + " picked up!");
        WeaponStats playerStats = player.GetComponentInChildren<WeaponStats>();
        WeaponController playerController = player.GetComponentInChildren<WeaponController>();

        playerStats.weaponName = thisStats.weaponName;
        playerStats.damage = thisStats.damage;
        playerStats.model = thisStats.model;
        playerStats.projectile = thisStats.projectile;
        playerController.timeBetweenShots = thisStats._rOf;//save from here into save/load method in data manager
        playerController.projectileSpeed = thisStats._projSpeed;
        playerStats.CreateWeaponPrefab();
        playerStats.loadedStats = false;
        KillThis();
    }

    void KillThis()
    {
        Destroy(gameObject);
    }

    
}
