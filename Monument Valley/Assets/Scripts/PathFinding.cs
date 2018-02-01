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

	public Node startNode, currentNode, nextNode, targetNode;
	int direction;

	private void Awake()
	{
		path = new List<Node>();
		allNodes = new List<GameObject>();
		upperTeleportNodes = new Node[3];
		lowerTeleportNodes = new Node[3];

		allNodes = GameObject.FindGameObjectsWithTag("WalkNode").ToList();
	}

	private void Start()
	{
		for (int i = 0; i < allNodes.Count; i++)
		{
			Debug.Log(allNodes[i].name);
		}
	}

	public List<Node> FindPath(Node node)
	{
		if (node.walkable)
		{
			direction = 0;
			clickedNode = node;
			startNode = clickedNode;
			currentNode = startNode;
			AddNodeToPath(currentNode);

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

						//Debug.Log("Here1 currentNode: " + currentNode);
						if (currentNode.neighbours.Length > startNode.neighbours.Length) startNode = currentNode;

						//Debug.Log("Here2 currentNode: " + currentNode);
						if (nextNode.walkable)
						{
							//Debug.Log("currentNode: " + currentNode + " next: " + nextNode + " target: " + targetNode);
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

							Debug.Log("Here3");
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

									Debug.Log("Here4");
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

	private void Teleport(Node firstNode, Node secondNode)
	{
		//allNodes 
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
	}
}