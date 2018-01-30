using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] float moveSpeed;

	public List<Node> path;
	public bool move;

	float step;
	int pathIndex = 0;
	private void Awake()
	{
		path = new List<Node>();
	}

	// Use this for initialization
	void Start()
	{
		step = moveSpeed * Time.deltaTime;
		move = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (transform.position == path[path.Count - 1].transform.position)
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
		}
	}
}
