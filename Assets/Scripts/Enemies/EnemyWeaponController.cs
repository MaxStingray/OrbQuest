using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ooparts.dungen;


public class EnemyWeaponController : MonoBehaviour {

    public bool fire;
    public EnemyProjectile projectile;

    public float minSpeed;
    public float maxSpeed;

    public float projectileSpeed;

   

    public float timeBetweenShots;
    private float shotCounter;

    public Transform firePoint;

    void Start()
    {
        projectileSpeed = Random.Range(minSpeed, maxSpeed);
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (RoomMapManager.generated != true)
            return;
        else
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0 && fire == false)
            {
                EnemyProjectile newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation) as EnemyProjectile;
                newProjectile.speed = projectileSpeed;
                fire = true;
                shotCounter = timeBetweenShots;
            }

            if (shotCounter > 0)
                fire = false;
        }
	}
}
