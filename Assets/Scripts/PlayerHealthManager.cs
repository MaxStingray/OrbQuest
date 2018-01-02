using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ooparts.dungen;


public class PlayerHealthManager : MonoBehaviour {

    public float HP;
    public float currentHP;

    RoomMapManager manager;

    bool initialLoad;

    public bool playerDead;


    void Awake()
    {
        if (manager == null)
            manager = GameObject.FindObjectOfType<RoomMapManager>();
        else
            if (manager.currentFloor > 1)
                initialLoad = true;
    }

    // Use this for initialization
    void Start()
    {//TODO: put in some kinda thing or whatever to determine whether this is a fresh instance or carrying from previous floor
        if(!initialLoad)
            currentHP = HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP >= HP)
        {
            currentHP = HP;
        }

        if (currentHP <= 0)
        {
            KillThis();
        }
        else
        {
            playerDead = false;
        }

    }

    public void hurtPlayer(int damage)
    {
        currentHP -= damage;
    }

    void KillThis()
    {
        bool onlyOnce = false;

        if (!onlyOnce)
        {
            onlyOnce = true;
            playerDead = true;
            gameObject.SetActive(false);            
        }
        
    }
}
