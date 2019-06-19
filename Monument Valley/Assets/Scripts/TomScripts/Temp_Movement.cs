using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_Movement : MonoBehaviour {

    private float move = 0.25f;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + move);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - move);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position = new Vector3(this.transform.position.x + move, this.transform.position.y, this.transform.position.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position = new Vector3(this.transform.position.x - move, this.transform.position.y, this.transform.position.z);
        }
    }
}
