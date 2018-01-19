using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    private float _sensitivity = 1f;
    public float minAngle, maxAngle, midAngle;
    public float _angleCorrection = 10f;
    private Vector3 _mouseReference , _mouseOffset;
    private Vector3 _rotation = Vector3.zero;
    private bool _isRotating;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_isRotating)
        {
            _mouseOffset = (Input.mousePosition - _mouseReference);
            _rotation.z = -(_mouseOffset.y) * _sensitivity;

            if (transform.eulerAngles.z < minAngle)
            {
                print("rotating to " + minAngle);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, minAngle));
            } 
            if (transform.eulerAngles.z > maxAngle)
            {
                print("rotating to " + maxAngle);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, maxAngle));
            }
            if (transform.eulerAngles.z + _rotation.z >= minAngle && transform.eulerAngles.z + _rotation.z <= maxAngle)
            {
                transform.Rotate(_rotation);
            }

            _mouseReference = Input.mousePosition;
        }
	}

    void OnMouseDown()
    {
        _isRotating = true;
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        _isRotating = false;

        
        print("angle on liftup: " + transform.eulerAngles.z);
        
        if (transform.eulerAngles.z > maxAngle - _angleCorrection)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, maxAngle));
            print("autocorrecting to maxAngle = " + maxAngle);
        }

        else if (transform.eulerAngles.z < minAngle + _angleCorrection)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, minAngle));
            print("autocorrecting to minAngle = " + minAngle);
            print("angle after correction: " + transform.eulerAngles.z);
        }

        else if(transform.eulerAngles.z > midAngle - _angleCorrection && transform.eulerAngles.z < midAngle + _angleCorrection)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, midAngle));
            print("autocorrecting to midAngle = " + midAngle);
        }
    }
}