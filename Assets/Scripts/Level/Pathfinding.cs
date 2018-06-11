using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    public DungeonRoomGrid grid;

    public List<Node> FindPath(Vector2 startPos, Vector2 targetPos)
    {
        Node startNode = GridChecker.GetNodeFromWorldPosition(startPos);
        Node targetNode = GridChecker.GetNodeFromWorldPosition(targetPos);

        if (startNode == null || targetNode == null)
            return null;

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (Node neighbourNode in grid.GetNeighbourNodes(currentNode))
            {
                if (!neighbourNode.walkable || closedSet.Contains(neighbourNode))
                    continue;

                int newCostToNeighbour = currentNode.gCost + GetTargetDistance(currentNode, neighbourNode);
                if (newCostToNeighbour < neighbourNode.gCost || !openSet.Contains(neighbourNode))
                {
                    neighbourNode.gCost = newCostToNeighbour;
                    neighbourNode.hCost = GetTargetDistance(neighbourNode, targetNode);
                    neighbourNode.parent = currentNode;

                    if (!openSet.Contains(neighbourNode))
                        openSet.Add(neighbourNode);
                }
            }
        }

        return null;
    }

    private List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    public int GetTargetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);
        else
            return 14 * distX + 10 * (distY - distX);
    }
}
