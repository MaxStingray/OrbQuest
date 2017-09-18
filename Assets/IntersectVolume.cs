using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectVolume : MonoBehaviour {

    public bool intersects = false;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Intersect" || other.gameObject.tag == "Wall")
        {
            intersects = true;
        }
    }
}
