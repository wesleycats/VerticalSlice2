using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    private float _sensitivity = 1f;
    public string rotationAxis, inputAxis;
    public float minAngle, maxAngle;
    public float[] midAngle;
    public float _angleCorrection = 10f;
    private Vector3 _mouseReference , _mouseOffset;
    private Vector3 _rotation = Vector3.zero;
    private bool _isRotating;

    // Use this for initialization
    void Start () {
        rotationAxis = rotationAxis.ToUpper();
        inputAxis = inputAxis.ToUpper();
	}
	
	// Update is called once per frame
	void Update () {
       if (_isRotating)
        { 
            switch (rotationAxis)
            {
                case "X":
                    RotateXAxis();
                 break;
                case "Y":
                    RotateYAxis();
                  break;
                case "Z":
                    RotateZAxis();
                  break;
                default:
                    Debug.Log("no rotation axis provided");
                  break;
            }
            _mouseOffset = (Input.mousePosition - _mouseReference);
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
        switch (rotationAxis)
        {
            case "X":
                RotateXAxisSnap();
                break;
            case "Y":
                RotateYAxisSnap();
                break;
            case "Z":
                RotateZAxisSnap();
              break;
        }
    }



    void RotateXAxis()
    {
        switch (inputAxis)
        {
            case "Y":
                _rotation.x = -(_mouseOffset.y) * _sensitivity;
                break;
            case "-Y":
                _rotation.x = (_mouseOffset.y) * _sensitivity;
                break;
            case "X":
                _rotation.x = -(_mouseOffset.x) * _sensitivity;
                break;
            case "-X":
                _rotation.x = (_mouseOffset.x) * _sensitivity;
                break;
        }

        if (transform.eulerAngles.x < minAngle)
        {
            print("rotating to " + minAngle);
            transform.rotation = Quaternion.Euler(new Vector3(minAngle, 0, 0));
        }
        if (transform.eulerAngles.x > maxAngle)
        {
            print("rotating to " + maxAngle);
            transform.rotation = Quaternion.Euler(new Vector3(maxAngle, 0, 0));
        }
        if (transform.eulerAngles.x + _rotation.x >= minAngle && transform.eulerAngles.x + _rotation.x <= maxAngle)
        {
            transform.Rotate(_rotation);
        }
    }

    void RotateXAxisSnap()
    {
        print("x angle on liftup: " + transform.eulerAngles.x);

        if (transform.eulerAngles.x > maxAngle - _angleCorrection)
        {
            transform.rotation = Quaternion.Euler(new Vector3(maxAngle, 0, 0));
            print("autocorrecting to maxAngle = " + maxAngle);
        }

        if (transform.eulerAngles.x < minAngle + _angleCorrection)
        {
            transform.rotation = Quaternion.Euler(new Vector3(minAngle, 0, 0));
            print("autocorrecting to minAngle = " + minAngle);
        }

        for(int i = 0; i < midAngle.Length; i++)
        {
            if (transform.eulerAngles.x > midAngle[i] -_angleCorrection && transform.eulerAngles.x < midAngle[i] + _angleCorrection)
            {
                transform.rotation = Quaternion.Euler(new Vector3(midAngle[i], 0, 0));
                print("autocorrecting to midAngle = " + midAngle[i]);
            }
        }
    }

    void RotateYAxis()
    {
        switch (inputAxis)
        {
            case "Y":
                _rotation.y = -(_mouseOffset.y) * _sensitivity;
                break;
            case "-Y":
                _rotation.y = (_mouseOffset.y) * _sensitivity;
                break;
            case "X":
                _rotation.y = -(_mouseOffset.x) * _sensitivity;
                break;
            case "-X":
                _rotation.y = (_mouseOffset.x) * _sensitivity;
                break;
        } 

        if (transform.eulerAngles.y < minAngle)
        {
            print("rotating to " + minAngle);
            transform.rotation = Quaternion.Euler(new Vector3(0, minAngle, 0));
        }
        if (transform.eulerAngles.y > maxAngle)
        {
            print("rotating to " + maxAngle);
            transform.rotation = Quaternion.Euler(new Vector3(0, maxAngle, 0));
        }
        if (transform.eulerAngles.y + _rotation.y >= minAngle && transform.eulerAngles.y + _rotation.y <= maxAngle)
        {
            transform.Rotate(_rotation);
        }
    }

    void RotateYAxisSnap()
    {
        print("y angle on liftup: " + transform.eulerAngles.y);

        if (transform.eulerAngles.y > maxAngle - _angleCorrection)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, maxAngle, 0));
            print("autocorrecting to maxAngle = " + maxAngle);
        }

        else if (transform.eulerAngles.y < minAngle + _angleCorrection)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, minAngle, 0));
            print("autocorrecting to minAngle = " + minAngle);
        }

        for(int i =0; i < midAngle.Length; i++)
        {
            if (transform.eulerAngles.y > midAngle[i] - _angleCorrection && transform.eulerAngles.y < midAngle[i] + _angleCorrection)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, midAngle[i], 0));
                print("autocorrecting to midAngle = " + midAngle[i]);
            }
        }
        
    }

    void RotateZAxis()
    {
        switch (inputAxis)
        {
            case "Y":
                _rotation.z = -(_mouseOffset.y) * _sensitivity;
                break;
            case "-Y":
                _rotation.z = (_mouseOffset.y) * _sensitivity;
                break;
            case "X":
                _rotation.z = -(_mouseOffset.x) * _sensitivity;
                break;
            case "-X":
                _rotation.z = (_mouseOffset.x) * _sensitivity;
                break;
        }

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
    }

    void RotateZAxisSnap()
    {
        print("z angle on liftup: " + transform.eulerAngles.z);

        if (transform.eulerAngles.z > maxAngle - _angleCorrection)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, maxAngle));
            print("autocorrecting to maxAngle = " + maxAngle);
        }

        else if (transform.eulerAngles.z < minAngle + _angleCorrection)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, minAngle));
            print("autocorrecting to minAngle = " + minAngle);
        }

        for(int i =0; i < midAngle.Length; i++)
        {
            if (transform.eulerAngles.z > midAngle[i] - _angleCorrection && transform.eulerAngles.z < midAngle[i] + _angleCorrection)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, midAngle[i]));
                print("autocorrecting to midAngle = " + midAngle[i]);
            }
        }
    }
}

