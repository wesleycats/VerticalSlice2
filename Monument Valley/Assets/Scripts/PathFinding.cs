using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

	Ray ray;
	RaycastHit hit;
	List<GameObject> path;
	GameObject clickedNode;
	GameObject startNode;
	GameObject currentNode;
	GameObject nextNode;
	GameObject targetNode;
	int direction;
	int nextDirection;

	int test;
	bool clear;

	private void Awake()
	{
		path = new List<GameObject>();
	}

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			test = 0;
			clear = true;
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
				path.Clear();
				FindPath(hit.transform.gameObject.GetComponent<Node>());
			}
		}
	}

	List<GameObject> FindPath(Node node)
	{
		Debug.LogWarning("Clicked node: " + node.gameObject.name);
		if (node.walkable)
		{
			direction = 0;
			Debug.Log("direction: " + direction);
			startNode = node.gameObject;
			clickedNode = startNode;
			currentNode = startNode;
			path.Add(currentNode);
			Debug.Log("added " + currentNode.name + " in index " + path.IndexOf(currentNode));
			try
			{
				do
				{
					nextNode = currentNode.GetComponent<Node>().neighbours[direction];

					if (currentNode.GetComponent<Node>().neighbours.Length > startNode.GetComponent<Node>().neighbours.Length)
					{
						startNode = currentNode;
						Debug.Log("start node changed to " + currentNode.name);
					}

					if (nextNode.GetComponent<Node>().walkable)
					{
						currentNode = nextNode;
						Debug.Log("next node:" + nextNode.name);
						path.Add(currentNode);
						Debug.Log("added " + currentNode.name + " in index " + path.IndexOf(currentNode));
						if (currentNode == targetNode)
						{
							Debug.Log("Correct path");
							return path;
						}
					}
					else
					{
						direction = 2;
						Debug.Log("direction: " + direction);
						nextNode = currentNode.GetComponent<Node>().neighbours[direction];
						if (nextNode.GetComponent<Node>().walkable)
						{
							currentNode = nextNode;
							path.Add(currentNode);
							Debug.Log("added " + currentNode.name + " in index " + path.IndexOf(currentNode));
							if (currentNode == targetNode)
							{
								Debug.Log("Correct path");
								return path;
							}
							else
							{
								clear = false;
								if (currentNode.GetComponent<Node>().neighbours[direction])
								{
									// To break out this loop and continue in the catch loop 
								}
							}
						}
						else
						{
							Debug.Log("Uncorrect path");
							return path;
						}
					}
				} while (clear && currentNode.GetComponent<Node>().walkable && currentNode.GetComponent<Node>().neighbours[1]);
			}
			catch
			{
				direction = 1;
				Debug.Log("direction: " + direction);
				if (clear)
				{
					path.Clear();
					Debug.Log("Cleared path");
					currentNode = clickedNode;
					path.Add(currentNode);
					Debug.Log("added " + currentNode.name + " in index " + path.IndexOf(currentNode));
					currentNode = startNode;
					path.Add(currentNode);
					Debug.Log("added " + currentNode.name + " in index " + path.IndexOf(currentNode));
					nextNode = startNode.GetComponent<Node>().neighbours[direction];
					if (nextNode.GetComponent<Node>().walkable)
					{
						currentNode = nextNode;
						Debug.Log("next node:" + nextNode.name);
						path.Add(currentNode);
						Debug.Log("added " + currentNode.name + " in index " + path.IndexOf(currentNode));
						clear = false;
						try
						{
							if (currentNode.GetComponent<Node>().neighbours[direction])
							{
								// To break out this loop and continue in the catch loop 
							}
						}
						catch
						{
							Debug.Log("Uncorrect path");
							return path;
						}
					}
					else
					{
						Debug.Log("Uncorrect path");
						return path;
					}
				}
				else
				{
					Debug.Log("Uncorrect path");
					return path;
				}
				try
				{
					do
					{
						test++;
						nextNode = currentNode.GetComponent<Node>().neighbours[direction];
						if (currentNode.GetComponent<Node>().neighbours.Length > startNode.GetComponent<Node>().neighbours.Length)
						{
							startNode = currentNode;
							Debug.Log("start node changed to " + currentNode.name);
						}
						if (nextNode.GetComponent<Node>().walkable)
						{
							currentNode = nextNode;
							Debug.Log("next node:" + nextNode.name);
							path.Add(currentNode);
							Debug.Log("added " + currentNode.name + " in index " + path.IndexOf(currentNode));
							if (currentNode == targetNode)
							{
								Debug.Log("Correct path");
								return path;
							}
						}
						else
						{
							// TEST
							direction = 2;
							Debug.Log("direction: " + direction);
							nextNode = currentNode.GetComponent<Node>().neighbours[direction];
							if (nextNode.GetComponent<Node>().walkable)
							{
								currentNode = nextNode;
								path.Add(currentNode);
								Debug.Log("added " + currentNode.name + " in index " + path.IndexOf(currentNode));
								if (currentNode == targetNode)
								{
									Debug.Log("Correct path");
									return path;
								}
								else
								{
									direction = 1;
									Debug.Log("direction: " + direction);
									nextNode = currentNode.GetComponent<Node>().neighbours[direction];
									if (nextNode.GetComponent<Node>().walkable)
									{
										currentNode = nextNode;
										path.Add(currentNode);
										Debug.Log("added " + currentNode.name + " in index " + path.IndexOf(currentNode));
										if (currentNode == targetNode)
										{
											Debug.Log("Correct path");
											return path;
										}
									}
								}
							}
							else
							{
								Debug.Log("Uncorrect path");
								return path;
							}
						}
					} while (test < 20);
				}
				catch
				{
					Debug.Log("Uncorrect path");
					return path;
				}
			}

			Debug.Log("what");
			//PrintList(path);
		}

		return path;
	}

	void PrintList(List<GameObject> list)
	{
		for(int i = 0; i < list.Count; i++)
		{
			Debug.Log(list[i].name);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		targetNode = other.gameObject;
		Debug.Log("Target node changed to: " + targetNode.name);
	}
}