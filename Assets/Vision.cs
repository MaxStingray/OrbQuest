using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{

    List<Transform> currentCollisions = new List<Transform>();
    GameObject closestObj;

    public Transform target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentCollisions.Count >= 1)
        {
            target = GetClosestEnemy();
        }
        else
        {
            target = null;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goblin")
        {
            currentCollisions.Add(other.gameObject.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Goblin")
        {
            currentCollisions.Remove(other.gameObject.transform);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Goblin")
        {
            currentCollisions.Add(other.gameObject.transform);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Goblin")
        {
            currentCollisions.Remove(other.gameObject.transform);
        }
    }


    Transform GetClosestEnemy()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Transform potentialTarget in currentCollisions)
        {
            if (potentialTarget != null)
            {
                Vector3 directionToTarget = potentialTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
            
            return bestTarget;
        }

        return null;
    }

}


