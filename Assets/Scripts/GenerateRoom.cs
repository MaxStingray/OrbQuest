using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoom : MonoBehaviour {

    public int maxWidth;
    public int maxLength;
    public int minWidth;
    public int minlength;


    bool createdFloor;

	// Use this for initialization
	void Start ()
    {
        CreateFloor(maxWidth, maxLength);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateFloor(int width, int length)
    {
        int _width = Random.Range(minWidth, width + 1);
        int _length = Random.Range(minlength, length + 1);

        transform.localScale = new Vector3(_width * 0.1f, 0, length * 0.1f);

        CreateWalls(_width, _length);
    }

    void CreateWalls(int finalWidth, int finalLength)
    {
        //TODO: FIX THIS SHIT
        float offsetX;
        float offsetZ;

        GameObject eastWall = new GameObject();
        eastWall.transform.parent = transform;
        Debug.Log("center of room: " + transform.position.x);
        Debug.Log("right edge of room: " + transform.TransformPoint(transform.localScale).x);

        Debug.Log("final width (units): " + finalWidth);
        Debug.Log("final length (units): " + finalLength);

        //offsetX = transform.localScale.x - transform.position.x;
        //offsetX = transform.position.x + transform.TransformPoint(transform.localScale).x;
        offsetX = transform.position.x + transform.lossyScale.x * 4.75f;
        for(int i = 0; i < finalLength; i++)
        {
            GameObject cubeTempPos = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //GameObject cubeTempNeg = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cubeTempPos.transform.position = new Vector3(offsetX, transform.position.y + 0.5f, transform.position.z + i);
            //cubeTempNeg.transform.position = new Vector3(offsetX, transform.position.y + 0.5f, transform.position.z - i);
            cubeTempPos.transform.parent = eastWall.transform;

        }

        eastWall.transform.position = new Vector3(eastWall.transform.position.x, eastWall.transform.position.y, transform.position.x - finalLength / 2);
        Debug.Log(offsetX);
    }
}
