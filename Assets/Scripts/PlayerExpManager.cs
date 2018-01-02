using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ooparts.dungen;


public class PlayerExpManager : MonoBehaviour {

    public int level;

    public float requiredXP = 100;

    public float currentXP;

    float expPool;

    PlayerHealthManager pHealth;
    RoomMapManager manager;


    bool initialLoad;

    void Awake()
    {
        if (manager == null)
            manager = GameObject.FindObjectOfType<RoomMapManager>();
        else
            if (manager.currentFloor > 1)
                initialLoad = true;
    }

	// Use this for initialization
	void Start ()
    {
        if(!initialLoad)
            currentXP = 0;


        pHealth = GetComponent<PlayerHealthManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (currentXP >= requiredXP)
        {
            expPool = currentXP - requiredXP;
            LevelUp();
            
        }

      
	}

    void LevelUp()
    {
        currentXP = 0;

        float previousExp = requiredXP;
        float previousHP = pHealth.HP;

        requiredXP += 0.2f * previousExp;
        pHealth.HP += 0.2f * previousHP;


        level += 1;
        pHealth.currentHP = pHealth.HP;
        currentXP += expPool;

        Debug.Log("Current Level: " + level);
    }
}
