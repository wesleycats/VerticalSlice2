using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour {

    public float _sensitivity = 0.01f;
    public float minY, maxY, midY;
    public string inputAxis;
    public float _slideCorrection = 1f;
    private Vector3 _mouseReference, _mouseOffset;
    private Vector3 _slide = Vector3.zero;
    private bool _isSliding;

    // Use this for initialization
    void Start()
    {
        inputAxis = inputAxis.ToUpper();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isSliding)
        {
            _mouseOffset = (Input.mousePosition - _mouseReference);
            SlideY();
            _mouseReference = Input.mousePosition;
        }
    }

    void OnMouseDown()
    {
        _isSliding = true;
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        _isSliding = false;
        SlideYSnap();
    }

    void SlideY()
    {
        switch (inputAxis)
        {
            case "Y":
                _slide.y = -(_mouseOffset.y) * _sensitivity;
                break;
            case "-Y":
                _slide.y = (_mouseOffset.y) * _sensitivity;
                break;
            case "X":
                _slide.y = -(_mouseOffset.x) * _sensitivity;
                break;
            case "-X":
                _slide.y = (_mouseOffset.x) * _sensitivity;
                break;
        }

        if (transform.position.y < minY)
        {
            print("sliding to " + minY);
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        }
        if (transform.position.y > maxY)
        {
            print("sliding to " + maxY);
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }
        if (transform.position.y + _slide.y >= minY && transform.position.y + _slide.y <= maxY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + _slide.y, transform.position.z);
        }
    }

    void SlideYSnap()
    {
        print("position on liftup: " + transform.position.y);

        if (transform.position.y > maxY - _slideCorrection)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
            print("autocorrecting to maxY = " + maxY);
        }

        if(midY != null)
        {
            if (transform.position.y > midY - _slideCorrection && transform.position.y < midY + _slideCorrection)
            {
                transform.position = new Vector3(transform.position.x, midY, transform.position.z);
                print("autocorrecting to midAngle = " + midY);
            }
        }

        if (transform.position.y < minY + _slideCorrection)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
            print("autocorrecting to minY = " + minY);
        }
    }
}
