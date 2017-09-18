using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExpController : MonoBehaviour {

    public float rewardExp;

    public PlayerExpManager playerExp;
    public EnemyHealthManager enemyHealth;

	// Use this for initialization
	void Start ()
    {
        enemyHealth = GetComponent<EnemyHealthManager>();
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerExp == null)
        {
            playerExp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExpManager>();
        }	
	}

    public void rewardExperience()
    {
        playerExp.currentXP += rewardExp;
    }
}
