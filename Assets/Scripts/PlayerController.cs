using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    private Rigidbody myRigidBody;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    public WeaponController theWeapon;

    private Camera mainCamera;

    public bool useController;
	// Use this for initialization
	void Start ()
    {
        myRigidBody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();

        if (gameObject.GetComponentInChildren<WeaponStats>() != null)
        {
            Debug.Log("staff head found");
        }	
	}
	
	// Update is called once per frame
	void Update ()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;


        //rotate with mouse
        if (!useController)
        {
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))//set raylength to be equal to the dist from start point to the intersect point
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
            if (theWeapon != null)
            {
                if (Input.GetMouseButton(0))
                {
                    theWeapon.isFiring = true;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    theWeapon.isFiring = false;
                }
            }
        }
        //rotate with controller
        if(useController)
        {
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("Rhorizontal") + Vector3.forward * -Input.GetAxisRaw("Rvertical");
            if(playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
            if (theWeapon != null)
            {
                if (Input.GetKeyDown(KeyCode.Joystick1Button5))
                {
                    theWeapon.isFiring = true;
                }

                if (Input.GetKeyUp(KeyCode.Joystick1Button5))
                {
                    theWeapon.isFiring = false;
                }
            }
        }
	}

    void FixedUpdate()
    {
        myRigidBody.velocity = moveVelocity;
    }
}
