using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public bool bridgeTrigger;
    public GameObject bridge;
    public float targetY,stepSpeed;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
            bridgeTrigger = true;
        
    }

    void Update()
    {
        if (bridgeTrigger)
        {
            bridge.transform.position = Vector3.MoveTowards(bridge.transform.position,new Vector3(bridge.transform.position.x,targetY,bridge.transform.position.z),stepSpeed);
        }
    }
}
