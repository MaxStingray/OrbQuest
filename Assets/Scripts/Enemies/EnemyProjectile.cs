using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    public float speed;

    public float lifeTime;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
            KillThis();
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Wall")
            KillThis();

        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthManager>().hurtPlayer(1);
            //KillThis();
        }
    }


    void KillThis()
    {
        Destroy(gameObject);
    }
}
