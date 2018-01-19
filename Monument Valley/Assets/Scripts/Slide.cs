using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour {

    private float _sensitivity = 0.01f;
    public float minY, maxY;
    private float _slideCorrection = 1f;
    private Vector3 _mouseReference, _mouseOffset;
    private Vector3 _slide = Vector3.zero;
    private bool _isSliding;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_isSliding)
        {
            _mouseOffset = (Input.mousePosition - _mouseReference);
            _slide.y = (_mouseOffset.y) * _sensitivity;

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


        print("position on liftup: " + transform.position.y);

        if (transform.position.y > maxY - _slideCorrection)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
            print("autocorrecting to maxY = " + maxY);
        }

        if (transform.position.y < minY + _slideCorrection)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
            print("autocorrecting to minY = " + minY);
        }
    }
}
