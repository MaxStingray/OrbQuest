using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    public float healthAmount;

    [HideInInspector]
    PlayerHealthManager pHealth;


	// Use this for initialization
	void Awake ()
    {
        healthAmount = Random.Range(5, 30);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            pHealth = other.gameObject.GetComponent<PlayerHealthManager>();
            percentageCalc();
            KillThis();
        }
    }

    void percentageCalc()
    {
        float result = (pHealth.currentHP / 100) * healthAmount;
        float diff = pHealth.HP - pHealth.currentHP;

        if (diff < result)
        {
            pHealth.currentHP += result;
        }
        else if (diff > result)
        {
            pHealth.currentHP = pHealth.HP;
        }
        else
        {
            return;
        }
        
        //my dumb brain can't figure it out. add healthAmount as a percentage to the current HP pool
    }

    void KillThis()
    {
        Destroy(gameObject);
    }
}
