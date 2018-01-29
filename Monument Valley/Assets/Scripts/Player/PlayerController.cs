using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour {

	public List<GameObject> walkTargets;

	private void Awake()
	{
		walkTargets = GameObject.FindGameObjectsWithTag("WalkTarget").ToList();
	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	/*GameObject CheckClosestTarget()
	{
		GameObject closest;

		

		return closest;
	}*/
}
