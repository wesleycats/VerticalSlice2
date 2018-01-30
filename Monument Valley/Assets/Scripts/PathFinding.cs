using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinding : MonoBehaviour {

	public List<Node> path;
	public Node clickedNode;
	public bool clear;
	
	Node startNode, currentNode, nextNode, targetNode;
	int direction;

	private void Awake()
	{
		path = new List<Node>();
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

					if (currentNode.neighbours.Length > startNode.neighbours.Length) startNode = currentNode;

					if (nextNode.walkable)
					{
						currentNode = nextNode;
						AddNodeToPath(currentNode);
						
						if (currentNode == targetNode) break;
					}
					else
					{
						direction = 2;
						ChangeNextToNeighbour(currentNode);

						if (nextNode.walkable)
						{
							currentNode = nextNode;
							AddNodeToPath(currentNode);
							
							if (currentNode == targetNode)
							{
								break;
							}
							else
							{
								clear = false;
								if (currentNode.neighbours[direction]); // To break out this loop and continue in the catch loop
							}
						}
						else
						{
							Debug.LogWarning("Uncorrect path");
							path.Clear();
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
					path.Clear();
					currentNode = clickedNode;
					AddNodeToPath(currentNode);
					currentNode = startNode;
					AddNodeToPath(currentNode);
					ChangeNextToNeighbour(currentNode);

					if (nextNode.walkable)
					{
						currentNode = nextNode;
						AddNodeToPath(currentNode);
						
						clear = false;
						try
						{
							if (currentNode.neighbours[direction]); // To break out this loop and continue in the catch loop
						}
						catch
						{
							Debug.LogWarning("Uncorrect path");
							path.Clear();
							return path;
						}
					}
					else
					{
						Debug.LogWarning("Uncorrect path");
						path.Clear();
						return path;
					}
				}
				else
				{
					Debug.LogWarning("Uncorrect path");
					path.Clear();
					return path;
				}
				try
				{
					do
					{
						ChangeNextToNeighbour(currentNode);

						if (currentNode.neighbours.Length > startNode.neighbours.Length) startNode = currentNode;

						if (nextNode.walkable)
						{
							currentNode = nextNode;
							AddNodeToPath(currentNode);
							
							if (currentNode == targetNode) break;
						}
						else
						{
							direction = 2;
							ChangeNextToNeighbour(currentNode);

							if (nextNode.walkable)
							{
								currentNode = nextNode;
								AddNodeToPath(currentNode);

								if (currentNode == targetNode)
								{
									break;
								}
								else
								{
									direction = 1;
									ChangeNextToNeighbour(currentNode);

									if (nextNode.walkable)
									{
										currentNode = nextNode;
										AddNodeToPath(currentNode);

										if (currentNode == targetNode) break;
									}
								}
							}
							else
							{
								Debug.LogWarning("Uncorrect path");
								path.Clear();
								return path;
							}
						}
					} while (currentNode != targetNode);
				}
				catch
				{
					Debug.LogWarning("Uncorrect path");
					path.Clear();
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
	}

	private void ChangeCurrentTo(Node node)
	{
		currentNode = node;
	}

	private void ChangeNextToNeighbour(Node node)
	{
		nextNode = node.GetComponent<Node>().neighbours[direction].GetComponent<Node>();
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