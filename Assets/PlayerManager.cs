using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ooparts.dungen;

/// <summary>
/// keeps track of the player and respawns them on death
/// </summary>
public class PlayerManager : MonoBehaviour {

    RoomMapManager manager;
    PlayerHealthManager playerHealth;
    Map map;

    float respawnTimer = 5f;
    void Awake()
    {
        manager = GetComponent<RoomMapManager>();
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (playerHealth == null)
        {
            playerHealth = GameObject.FindObjectOfType<PlayerHealthManager>();
        }
        
        if (map == null)
        {
            map = GameObject.FindObjectOfType<Map>();
        }

        if (playerHealth.playerDead)
        {
            respawnTimer -= Time.deltaTime;

            playerHealth.gameObject.transform.position = map._rooms[0].transform.position;
            playerHealth.currentHP = playerHealth.HP;

            if (respawnTimer <= 0)
            {
                playerHealth.gameObject.SetActive(true);
                respawnTimer = 5f;
            }
        }
	}
}
