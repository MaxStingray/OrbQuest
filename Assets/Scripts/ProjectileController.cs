using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float speed;

    public float lifeTime;
    public int damage;

    public bool bounce;
    public bool air;
    public bool homing;

    Vision vision;


	// Use this for initialization
	void Start ()
    {
        if (homing)
        {
            vision = GetComponentInChildren<Vision>();

        }
        else
        {
            vision = null;
           
        }
	}   
	
	// Update is called once per frame
	void Update ()
    {
        if (!homing)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        else
        {
            transform.LookAt(vision.target);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
            KillThis();
	}

    void findTarget()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (!bounce)
        {

            if (other.gameObject.tag == "Wall")
            {
                if(!air)
                    KillThis();                                  
            }

            if (other.gameObject.tag == "Goblin" || other.gameObject.tag == "bossMonster")
            {
                other.gameObject.GetComponent<EnemyHealthManager>().hurtEnemy(damage);
                if(!air)
                    KillThis();
            }
        }
        else
        {
            if (other.gameObject.tag == "Goblin")
            {
                other.gameObject.GetComponent<EnemyHealthManager>().hurtEnemy(damage);
                //KillThis();
            }
        }
    }
    

    void KillThis()
    {
        Destroy(gameObject);
    }
}
