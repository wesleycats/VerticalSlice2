﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] float moveSpeed;

	public List<Node> path;
	public bool move;
	public int pathIndex;

	float step;

	private void Awake()
	{
		path = new List<Node>();
	}

	// Use this for initialization
	void Start()
	{
		step = moveSpeed * Time.deltaTime;
		move = false;
		pathIndex = 0;
	}

	// Update is called once per frame
	void Update()
	{
		path = GetComponent<PathFinding>().path;
		//Debug.Log("Index: " + pathIndex);
		if (move)
		{
			Node currentNode = path[pathIndex];
			Node clickedNode = GetComponent<PathFinding>().clickedNode;
			try
			{
				Node nextNode = path[pathIndex + 1];

				if (currentNode != clickedNode)
				{
					if (Vector3.Distance(transform.position, currentNode.transform.position) <= 0.01f)
					{
						transform.position = currentNode.transform.position;
						currentNode = path[pathIndex++];
						Debug.Log(GetComponent<PathFinding>().targetNode);
					}
				}
			}
			catch
			{
				Debug.Log("Current node: " + currentNode);
				Debug.Log("Last Node");
				if (Vector3.Distance(transform.position, currentNode.transform.position) <= 0.01f)
				{
					transform.position = currentNode.transform.position;
					move = false;
					GetComponent<PathFinding>().correct = false;
					Debug.Log(GetComponent<PathFinding>().correct);
					GetComponent<PathFinding>().ClearList(path);
				}
			}

			transform.LookAt(currentNode.transform.position);
			transform.Translate(Vector3.forward * step);
		}



























		/*if (transform.position == path[path.Count].transform.position)
		{
			move = false;
		}

		step = moveSpeed * Time.deltaTime;
		path = GetComponent<PathFinding>().path;
		try
		{
			if (move)
			{
				Node currentNode = path[pathIndex];
				transform.LookAt(currentNode.transform.position);
				transform.Translate(transform.forward * step);

				if (Vector3.Distance(transform.position, currentNode.transform.position) <= 0.05)
				{
					transform.position = currentNode.transform.position;
					currentNode = path[pathIndex++];
					// is dit de einde node?
				}

			}
		}
		catch
		{
			//move = false;
		}*/
	}
}
