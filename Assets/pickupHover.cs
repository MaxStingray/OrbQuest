using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupHover : MonoBehaviour {

    float x0, y0, z0;
    public float speed, amplitude;

	// Use this for initialization
	void Start ()
    {
        x0 = transform.position.x;
        y0 = transform.position.y;
        z0 = transform.position.z;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(x0, y0 + amplitude * Mathf.Sin(speed * Time.time), z0);	
	}
}
