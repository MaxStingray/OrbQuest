using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class WeaponController : MonoBehaviour {

    public bool isFiring;

    public ProjectileController projectile;
    public WeaponStats stats;

    public float projectileSpeed;

    public float timeBetweenShots;
    private float shotCounter;
    GameObject previousHead;

    public Transform firePoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!stats.loadedStats)
        {
            if (previousHead != null)
            {
                Destroy(previousHead);
            }
            
            projectile = stats.projectile.GetComponent<ProjectileController>();
            projectile.damage = stats.damage;
            GameObject head;
            head = Instantiate(stats.model, stats.transform.position, stats.transform.rotation);
            head.transform.parent = gameObject.transform;
            previousHead = head;

            stats.loadedStats = true;
            //stats.model = Instantiate(stats.model, stats.transform.position, stats.transform.rotation);
        }

		if(isFiring)
        {
            shotCounter -= Time.deltaTime;//count down the timer
            if(shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                ProjectileController newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation) as ProjectileController;//instantiate with all properties of projectile
                newProjectile.speed = projectileSpeed;
            }
        }
        else
        {
            shotCounter = 0;
        }
	}

}
