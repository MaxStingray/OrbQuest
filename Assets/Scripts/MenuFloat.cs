using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFloat : MonoBehaviour {

    float y0;
    public float speed;
    public float amplitude;

	// Use this for initialization
	void Start ()
    {
        y0 = transform.position.y;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(0.38f, y0 + amplitude * Mathf.Sin(speed * Time.time), -8.58f);
	}
}
