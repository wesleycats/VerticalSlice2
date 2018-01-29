using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	Ray ray;
	RaycastHit hit;
	List<GameObject> path;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		/*
		if (Input.GetMouseButtonDown(0))
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		}
		else
		{
			ray.origin = new Vector3(0, 0, 0);
		}

		if (Physics.Raycast(ray, out hit, 100))
		{
			//Debug.DrawLine(ray.origin, hit.point, Color.green);
			if (hit.transform.tag == "WalkNode")
			{
				//Debug.Log(FindPath(hit.transform.gameObject.GetComponent<Node>()));
			}
		}*/
	}
}
