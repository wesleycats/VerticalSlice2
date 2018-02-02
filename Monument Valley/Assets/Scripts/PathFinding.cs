using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinding : MonoBehaviour
{
	public List<Node> path;
	public List<GameObject> allNodes;
	public Node clickedNode;
	public Node[] upperTeleportNodes;
	public Node[] lowerTeleportNodes;
	public bool clear;
	public bool correct;
	public int floorLevel, nodeIndex;
	public float seconds;

	public Node startNode, currentNode, nextNode, targetNode;
	
	bool stay, teleport;
	int direction;

	private void Awake()
	{
		path = new List<Node>();
		//upperTeleportNodes = new Node[3];
		//lowerTeleportNodes = new Node[3];
		//allNodes = new List<GameObject>();
		//allNodes = GameObject.FindGameObjectsWithTag("WalkNode").ToList();
	}

	private void Start()
	{
		/*
		//allNodes.Sort();
		for (int i = 0; i < allNodes.Count; i++)
		{
			allNodes[i] = allNodes[i].name;
		}*/
	}

	public List<Node> FindPath(Node node)
	{
		teleport = true;
		if (node.walkable)
		{
			if (node == upperTeleportNodes[2])
			{
				stay = true;
				switch (floorLevel)
				{
					case 4:
						node = upperTeleportNodes[2];
						Debug.Log("node = " + node.name);
						break;

					case 3:
						node = lowerTeleportNodes[2];
						Debug.Log("node = " + node.name);
						break;
				}
			}
			else if (node == upperTeleportNodes[1])
			{
				stay = true;
				switch (floorLevel)
				{
					case 3:
						node = upperTeleportNodes[1];
						Debug.Log("node = " + node.name);
						break;

					case 2:
						node = lowerTeleportNodes[1];
						Debug.Log("node = " + node.name);
						break;
				}
			}
			else if (node == upperTeleportNodes[0])
			{
				stay = true;
				switch (floorLevel)
				{
					case 2:
						node = upperTeleportNodes[0];
						Debug.Log("node = " + node.name);
						break;

					case 1:
						node = lowerTeleportNodes[0];
						Debug.Log("node = " + node.name);
						break;
				}
			}

			direction = 0;
			clickedNode = node;
			startNode = clickedNode;
			currentNode = startNode;
			AddNodeToPath(currentNode);
			Debug.Log("added " + currentNode + " to index " + path.IndexOf(currentNode));

			try
			{
				do
				{
					ChangeNextToNeighbour(currentNode);

					//Debug.Log("here");
					if (currentNode.neighbours.Length > startNode.neighbours.Length) startNode = currentNode;

					if (nextNode.walkable)
					{
						//Debug.Log("current: " + currentNode + " next: " + nextNode);
						currentNode = nextNode;
						AddNodeToPath(currentNode);
						Debug.Log("added " + currentNode + " to index " + path.IndexOf(currentNode));

						if (currentNode == targetNode)
						{
							correct = true;
							break;

						}
					}
					else
					{
						direction = 2;
						ChangeNextToNeighbour(currentNode);

						Debug.Log("here");
						if (nextNode.walkable)
						{
							currentNode = nextNode;
							AddNodeToPath(currentNode);
							Debug.Log("added " + currentNode + " to index " + path.IndexOf(currentNode));

							if (currentNode == targetNode)
							{
								correct = true;
								break;
							}
							else
							{
								Debug.Log("here");
								clear = false;
								if (currentNode.neighbours[direction]) ; // To break out this loop and continue in the catch loop
							}
						}
						else
						{
							Debug.LogWarning("Uncorrect path");
							correct = false;
							ClearList(path);
							return path;
						}
					}
				} while (clear && currentNode.walkable && currentNode.neighbours[1]);
			}
			catch
			{
				direction = 1;
				if (clear)
				{
					//Debug.Log("current: " + currentNode + " clicked: " + clickedNode);
					ClearList(path);
					currentNode = clickedNode;
					AddNodeToPath(currentNode);
					//Debug.Log("start: " + startNode);
					//Debug.Log("added " + currentNode + " to index " + path.IndexOf(currentNode));
					currentNode = startNode;
					AddNodeToPath(currentNode);
					Debug.Log("added " + currentNode + " to index " + path.IndexOf(currentNode));

					ChangeNextToNeighbour(currentNode);

					if (nextNode.walkable)
					{
						currentNode = nextNode;
						AddNodeToPath(currentNode);
						Debug.Log("added " + currentNode + " to index " + path.IndexOf(currentNode));

						if (currentNode == clickedNode)
						{
							startNode = currentNode;
							ClearList(path);
							AddNodeToPath(currentNode);
							Debug.Log("added " + currentNode + " to index " + path.IndexOf(currentNode));
						}

						clear = false;
						try
						{
							if (currentNode.neighbours[direction]) ; // To break out this loop and continue in the catch loop
						}
						catch
						{
							Debug.LogWarning("Uncorrect path");
							correct = false;
							ClearList(path);
							return path;
						}
					}
					else
					{
						Debug.LogWarning("Uncorrect path");
						correct = false;
						ClearList(path);
						return path;
					}
				}
				else
				{
					Debug.LogWarning("Uncorrect path");
					correct = false;
					ClearList(path);
					return path;
				}
				try
				{
					do
					{
						ChangeNextToNeighbour(currentNode);

						Debug.Log("Here1 currentNode: " + currentNode);
						if (currentNode.neighbours.Length > startNode.neighbours.Length) startNode = currentNode;

						Debug.Log("Here2 currentNode: " + currentNode);
						if (nextNode.walkable)
						{
							Debug.Log("currentNode: " + currentNode + " next: " + nextNode + " target: " + targetNode);
							if (currentNode == targetNode)
							{
								correct = true;
								break;
							}
							else
							{
								currentNode = nextNode;
								AddNodeToPath(currentNode);
								Debug.Log("added " + currentNode + " to index " + path.IndexOf(currentNode));

								if (currentNode == clickedNode)
								{
									startNode = currentNode;
									ClearList(path);
									AddNodeToPath(currentNode);
									Debug.Log("added " + currentNode + " to index " + path.IndexOf(currentNode));
								}
							}

							if (currentNode == targetNode)
							{
								correct = true;
								break;
							}
						}
						else
						{
							direction = 2;
							ChangeNextToNeighbour(currentNode);

							//Debug.Log("Here3");
							if (nextNode.walkable)
							{
								currentNode = nextNode;
								AddNodeToPath(currentNode);
								Debug.Log("added " + currentNode + " to index " + path.IndexOf(currentNode));

								if (currentNode == targetNode)
								{
									correct = true;
									break;
								}
								else
								{
									direction = 1;
									ChangeNextToNeighbour(currentNode);

									//Debug.Log("Here4");
									if (nextNode.walkable)
									{
										currentNode = nextNode;
										AddNodeToPath(currentNode);
										Debug.Log("added " + currentNode + " to index " + path.IndexOf(currentNode));

										if (currentNode == targetNode)
										{
											correct = true;
											break;
										}
									}
								}
							}
							else
							{
								Debug.LogWarning("Uncorrect path");
								correct = false;
								ClearList(path);
								return path;
							}
						}
					} while (currentNode != targetNode);
				}
				catch
				{
					Debug.LogWarning("Uncorrect path");
					correct = false;
					ClearList(path);
					return path;
				}
			}
		}

		path.Reverse();
		PrintList(path);
		return path;
	}

	private void AddNodeToPath(Node node)
	{
		path.Add(node);
		//Debug.Log("added " + node + " to index " + path.IndexOf(node));
	}

	private void ChangeCurrentTo(Node node)
	{
		currentNode = node;
	}

	private void ChangeNextToNeighbour(Node node)
	{
		nextNode = node.GetComponent<Node>().neighbours[direction].GetComponent<Node>();
	}

	public int CheckFloor()
	{
		for (int i = 0; i < allNodes.Count; i++)
		{
			//Debug.Log(allNodes[i].name + " on index " + allNodes.IndexOf(allNodes[i]));
			if (targetNode.gameObject == allNodes[i])
			{
				nodeIndex = i;
			}
		}

		//Debug.Log(nodeIndex);

		if (nodeIndex >= 45)
		{
			floorLevel = 4;
		}
		else if (nodeIndex >= 32)
		{
			floorLevel = 3;
		}
		else if (nodeIndex >= 18)
		{
			floorLevel = 2;
		}
		else
		{
			floorLevel = 1;
		}

		//Debug.Log(floorLevel);
		return floorLevel;
	}

	private Node OneLessIndexNode()
	{
		Node node;
		node = null;
		for (int i = 0; i < allNodes.Count; i++)
		{
			//Debug.Log(allNodes[i].name + " on index " + allNodes.IndexOf(allNodes[i]));
			if (targetNode.gameObject == allNodes[i])
			{
				try	{ node = allNodes[i - 1].GetComponent<Node>(); }
				catch {	}
			}
		}
		
		return node;
	}

	private Node OneMoreIndexNode()
	{
		Node node;
		node = null;
		for (int i = 0; i < allNodes.Count; i++)
		{
			//Debug.Log(allNodes[i].name + " on index " + allNodes.IndexOf(allNodes[i]));
			if (targetNode.gameObject == allNodes[i])
			{
				try { node = allNodes[i + 1].GetComponent<Node>(); }
				catch { }
			}
		}

		return node;
	}

	public void Teleport(Node node)
	{
		if (teleport)
		{
			transform.position = node.transform.position;
			GetComponent<PlayerMovement>().pathIndex++;
			teleport = false;
		}
		Debug.Log(nextNode.name);
		/*if (transform.position == targetNode.transform.position)
		{
			transform.position = node.transform.position;
			GetComponent<PlayerMovement>().pathIndex++;
		}*/
	}

	private IEnumerator TeleportDelay(Node node)
	{
		yield return new WaitForSeconds(seconds);
		Teleport(node);
	}

	public void ClearList(List<Node> list)
	{
		list.Clear();
	}

	void PrintList(List<Node> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			Debug.Log(list[i].name);
		}
	}



	private void OnTriggerEnter(Collider other)
	{
		targetNode = other.GetComponent<Node>();
		Debug.Log("Target node changed to: " + targetNode.name);
		if (!stay)
		{
			for (int i = 0; i < upperTeleportNodes.Length; i++)
			{
				if (targetNode.transform.position == upperTeleportNodes[i].transform.position)
				{
					StartCoroutine(TeleportDelay(OneLessIndexNode()));
				}
			}

			for (int i = 0; i < lowerTeleportNodes.Length; i++)
			{
				if (targetNode.transform.position == lowerTeleportNodes[i].transform.position)
				{
					StartCoroutine(TeleportDelay(OneMoreIndexNode()));
				}
			}
		}
		CheckFloor(); 
	}
}