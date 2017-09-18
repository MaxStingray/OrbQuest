using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    public int HP;
    private int currentHP;

    public EnemyExpController exp;

    public GameObject[] dropItems;
    UIManager manager;
    bool isBoss;

    bool willDrop;

    void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<UIManager>();
        int dropChance = Random.Range(0, 101);
        if (dropChance < 30)
        {
           willDrop = true;
        }
    }

	// Use this for initialization
	void Start ()
    {
        currentHP = HP;
        exp = GetComponent<EnemyExpController>();
        if (gameObject.tag == "bossMonster")
            isBoss = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(currentHP <= 0)
        {
            KillThis();
            manager.killCount++;
        }
	}

    public void hurtEnemy(int damage)
    {
        currentHP -= damage;
    }

    public void Drop()
    {
        if (willDrop && !isBoss)
        {
            int whichDrop = Random.Range(0, dropItems.Length);
            Vector3 dropPos = new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);


            GameObject tempDrop = Instantiate(dropItems[whichDrop], dropPos, transform.rotation);
        }
    }


    void KillThis()
    {
        if(isBoss)
        {
            GameObject.FindGameObjectWithTag("Manager").GetComponent<UIManager>().showVictory();
        }
        exp.rewardExperience();
        Drop();
        Destroy(gameObject);
    }
}
