﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandWave : MonoBehaviour {

    public float growFactor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
	}
}
